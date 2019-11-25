using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Zelda.Dungeon
{
    public class DungeonManager : IDrawable
    {
        protected static readonly Point TileSize = new Point(16, 16);
        protected static readonly Point BasementAccessRoom = new Point(1, 0);
        protected static readonly Point BasementRoom = new Point(1, 1);

        public Scene Scene { get; private set; }
        public bool[][] EnabledRooms { get; protected set; }
        public bool[][] UnmappedRooms { get; protected set; }
        public bool[][] VisitedRooms { get; protected set; }
        public Point CurrentRoom { get; protected set; } = Point.Zero;
        public Action<Point, Point, Direction> Pan { private get; set; } = delegate { };
        protected ISprite Background;
        protected Scene[][] Scenes;
        protected IPlayer Player;
        protected Room[][] Rooms;
        protected BackgroundId[][] BackgroundIds;

        public virtual bool CurrentRoomMapped => !UnmappedRooms[CurrentRoom.Y][CurrentRoom.X];

        protected enum BackgroundId
        {
            Default = 0,
            Basement = 1,
            OldMan = 2,
            Dungeon = 3,
            Shop = 4
        }

        protected static ISprite GenerateBgSprite(BackgroundId backgroundId)
        {
            switch (backgroundId)
            {
                case BackgroundId.Default:
                    return BackgroundSpriteFactory.Instance.CreateDungeonBackground();
                case BackgroundId.Basement:
                    return BackgroundSpriteFactory.Instance.CreateBasementBackground();
                case BackgroundId.OldMan:
                    return BackgroundSpriteFactory.Instance.CreateOldManBackground();
                case BackgroundId.Dungeon:
                    return BackgroundSpriteFactory.Instance.CreateSurvivalDungeonBackground();
                case BackgroundId.Shop:
                    return BackgroundSpriteFactory.Instance.CreateSurvivalShopBackground();
                default:
                    return BackgroundSpriteFactory.Instance.CreateDungeonBackground();
            }
        }

        protected void SetBackground(BackgroundId backgroundId)
        {
            Background = GenerateBgSprite(backgroundId);
        }

        public virtual void LoadDungeonContent(ContentManager content)
        {
            var enabledRooms = content.Load<int[][]>("DungeonEnabledRooms");
            var enemies = content.Load<int[][]>("DungeonEnemies");
            var backgrounds = content.Load<int[][]>("DungeonRoomBackgrounds");
            var rows = enabledRooms.Length;
            Scenes = new Scene[rows][];
            Rooms = new Room[rows][];
            BackgroundIds = new BackgroundId[rows][];
            EnabledRooms = new bool[rows][];
            UnmappedRooms = new bool[rows][];
            VisitedRooms = new bool[rows][];

            for (var row = 0; row < rows; row++)
            {
                var cols = enabledRooms[row].Length;
                Scenes[row] = new Scene[cols];
                Rooms[row] = new Room[cols];
                BackgroundIds[row] = new BackgroundId[cols];
                EnabledRooms[row] = new bool[cols];
                UnmappedRooms[row] = new bool[cols];
                VisitedRooms[row] = new bool[cols];

                for (var col = 0; col < cols; col++)
                {
                    EnabledRooms[row][col] = enabledRooms[row][col] != 0;
                    UnmappedRooms[row][col] = enabledRooms[row][col] == 2;
                    if (enabledRooms[row][col] == 0) continue;

                    var enemyId = -1;
                    var backgroundId = BackgroundId.Default;

                    if (row < enemies.Length && col < enemies[row].Length)
                    {
                        enemyId = enemies[row][col];
                    }

                    if (row < backgrounds.Length && col < backgrounds[row].Length)
                    {
                        backgroundId = (BackgroundId) backgrounds[row][col];
                    }

                    var room = new Room(this,content.Load<int[][]>($"Rooms/{row}-{col}"), enemyId);
                    Rooms[row][col] = room;
                    BackgroundIds[row][col] = backgroundId;
                }
            }
        }

        public virtual void LoadScenes(IPlayer player)
        {
            Player = player;
            for (var row = 0; row < Scenes.Length; row++)
            {
                for (var col = 0; col < Scenes[row].Length; col++)
                {
                    if (!EnabledRooms[row][col]) continue;
                    Scenes[row][col] = new Scene(Rooms[row][col], player);
                }
            }
        }

        public virtual void ResetScenes()
        {
            for (var row = 0; row < Scenes.Length; row++)
            {
                for (var col = 0; col < Scenes[row].Length; col++)
                {
                    VisitedRooms[row][col] = false;
                    Scenes[row][col]?.ResetEnemies();
                }
            }
        }

        public void Transition(Direction roomDirection, bool unlock)
        {
            var newRoom = CurrentRoom;
            switch (roomDirection)
            {
                case Direction.Up:
                    newRoom.Y--;
                    break;
                case Direction.Down:
                    newRoom.Y++;
                    break;
                case Direction.Left:
                    newRoom.X--;
                    break;
                case Direction.Right:
                    newRoom.X++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (unlock)
            {
                Rooms[newRoom.Y][newRoom.X].Doors[DirectionUtility.Flip(roomDirection)]?.Unblock();
            }

            Scene?.DestroyProjectiles();
            Pan(CurrentRoom, newRoom, roomDirection);
        }

        public IDrawable BuildPanScene(int row, int column)
        {
            return new PanningScene(Rooms[row][column], GenerateBgSprite(BackgroundIds[row][column]));
        }

        public virtual void JumpToRoom(int row, int column, Direction facing = Direction.Up)
        {
            var oldRoom = CurrentRoom;
            VisitedRooms[row][column] = true;
            CurrentRoom = new Point(column, row);
            SetBackground(BackgroundIds[row][column]);
            if (oldRoom == BasementRoom && CurrentRoom == BasementAccessRoom)
            {
                Player?.Teleport(TileSize * new Point(6, 7), Direction.Down);
            }
            else if (CurrentRoom == BasementRoom)
            {
                Player?.Teleport(TileSize * new Point(3,2), Direction.Down);
            }
            else
            {
                Player?.Teleport(TeleportLocation.Calculate(facing), facing);
            }

            Scene?.DestroyProjectiles();
            Scene = Scenes[row][column];
            Scene.SpawnScene();
        }

        public void Update()
        {
            Scene?.Update();
        }

        public void Draw()
        {
            Background?.Draw(Vector2.Zero);
            Scene?.Draw();
        }
    }
}