using System.Diagnostics.Eventing.Reader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Zelda.Dungeon;

namespace Zelda.Survival
{
    public class SurvivalManager : DungeonManager
    {
        public override bool CurrentRoomMapped { get; } = true;
        WaveManager _waveManager;

        public override void LoadDungeonContent(ContentManager content)
        {
            BackgroundIds = new[]
            {
                new[] {BackgroundId.Shop},
                new[] {BackgroundId.Dungeon},
            };

            Rooms = new [] {
                new Room[]
                {
                    new SurvivalRoom(this, content.Load<int[][]>($"Shop/ShopTiles"))
                },
                new Room[]
                {
                    new SurvivalRoom(this, content.Load<int[][]>($"Rooms/Survival-Dungeon"))
                }
            };

            var rows = Rooms.Length;
            Scenes = new Scene[rows][];
            EnabledRooms = new bool[rows][];
            UnmappedRooms = new bool[rows][];
            VisitedRooms = new bool[rows][];
            for (var row = 0; row < rows; row++)
            {
                var cols = Rooms[row].Length;
                Scenes[row] = new Scene[cols];
                EnabledRooms[row] = new bool[cols];
                UnmappedRooms[row] = new bool[cols];
                VisitedRooms[row] = new bool[cols];

                for (var col = 0; col < cols; col++)
                {
                    EnabledRooms[row][col] = true;
                    UnmappedRooms[row][col] = true;
                }
            }
            _waveManager = new WaveManager((SurvivalRoom)Rooms[0][0], content.Load<string[][]>("SurvivalWaves"));

            /* TODO: Implement */
        }

        public override void LoadScenes(IPlayer player)
        {
            base.LoadScenes(player);
        }

        public override void ResetScenes()
        {
            //No Op
        }

        public override void JumpToRoom(int row, int column, Direction facing = Direction.Up)
        {
            VisitedRooms[row][column] = true;
            CurrentRoom = new Point(column, row);
            SetBackground(BackgroundIds[row][column]);
            if (row == 1)
            {
                Player?.Teleport(TileSize * new Point(23, 2) + new Point(8, 0), Direction.Down);
            }
            else
            {
                Player?.Teleport(TileSize * new Point(27, 60) + new Point(8, 0), Direction.Up);
            }

            Scene?.DestroyProjectiles();
            Scene = Scenes[row][column];
            Scene.SpawnScene();
        }

        public override void Update() 
        {
            base.Update();
            _waveManager.Update();
        }
    }
}