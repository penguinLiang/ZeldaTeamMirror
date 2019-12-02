package main

import (
	"context"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"os"
	"strings"

	"github.com/jackc/pgx/v4/pgxpool"
)

const schema = `
CREATE TABLE IF NOT EXISTS Scores (
    initials VARCHAR(3) PRIMARY KEY,
    score INT
);
`

const (
	defaultPort = "8080"
	selectQuery = "SELECT initials, score FROM Scores ORDER BY score DESC LIMIT 25"
	insertQuery = "INSERT INTO Scores (initials, score) VALUES ($1, $2) ON CONFLICT (initials) DO UPDATE SET score = GREATEST(EXCLUDED.score, Scores.score)"
)

var db *pgxpool.Pool

func postScores(w http.ResponseWriter, req *http.Request) {
	var initials, score string
	if body, err := ioutil.ReadAll(req.Body); err == nil {
		s := strings.SplitN(string(body), ",", 2)
		initials, score = s[0], s[1]
	} else {
		http.Error(w, "Body Read Error", http.StatusInternalServerError)
	}

	_, err := db.Exec(context.Background(), insertQuery, initials, score)
	if err != nil {
		http.Error(w, "Insert Error\n"+err.Error(), http.StatusInternalServerError)
		return
	}
	listScores(w, req)
}

func listScores(w http.ResponseWriter, req *http.Request) {
	rows, _ := db.Query(context.Background(), selectQuery)

	for rows.Next() {
		var initials string
		var score int32
		err := rows.Scan(&initials, &score)
		if err != nil {
			http.Error(w, "Row Scan Error\n"+err.Error(), http.StatusInternalServerError)
			return
		}
		fmt.Fprintf(w, "%v,%v\n", initials, score)
	}
}

func urlHandler(w http.ResponseWriter, req *http.Request) {
	switch req.Method {
	case http.MethodGet:
		listScores(w, req)
	case http.MethodPost:
		postScores(w, req)
	default:
		w.Header().Add("Allow", "GET, POST")
		w.WriteHeader(http.StatusMethodNotAllowed)
	}
}

func main() {
	port := os.Getenv("PORT")
	if port == "" {
		port = defaultPort
	}

	poolConfig, err := pgxpool.ParseConfig(os.Getenv("DATABASE_URL"))
	if err != nil {
		log.Fatalf("Cannot parse DATABASE_URL: %v", err)
	}

	db, err = pgxpool.ConnectConfig(context.Background(), poolConfig)
	if err != nil {
		log.Fatal(err)
	}

	_, err = db.Exec(context.Background(), schema)
	if err != nil {
		log.Fatal(err)
		return
	}

	http.HandleFunc("/", urlHandler)
	log.Fatal(http.ListenAndServe(":"+port, nil))
}
