using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Zelda.Dungeon
{
    public class DungeonManager : IDrawable
    {
        private static readonly Point TileSize = new Point(16, 16);
        private static readonly Point BasementAccessRoom = new Point(1, 0);
        private static readonly Point BasementRoom = new Point(1, 1);

        public Scene Scene { get; private set; }
        public bool[][] EnabledRooms { get; private set; }
        public bool[][] UnmappedRooms { get; private set; }
        public bool[][] VisitedRooms { get; private set; }
        public Point CurrentRoom { get; private set; } = Point.Zero;
        public Action<Point, Point, Direction> Pan { private get; set; } = delegate { };
        private ISprite _background;
        private Scene[][] _scenes;
        private IPlayer _player;
        private Room[][] _rooms;
        private BackgroundId[][] _backgroundIds;

        public bool CurrentRoomMapped => !UnmappedRooms[CurrentRoom.Y][CurrentRoom.X];

        private enum BackgroundId
        {
            Default = 0,
            Basement = 1,
            OldMan = 2
        }

        private static ISprite Background(BackgroundId backgroundId)
        {
            switch (backgroundId)
            {
                case BackgroundId.Default:
                    return BackgroundSpriteFactory.Instance.CreateDungeonBackground();
                case BackgroundId.Basement:
                    return BackgroundSpriteFactory.Instance.CreateBasementBackground();
                case BackgroundId.OldMan:
                    return BackgroundSpriteFactory.Instance.CreateOldManBackground();
                default:
                    return BackgroundSpriteFactory.Instance.CreateDungeonBackground();
            }
        }

        private void SetBackground(BackgroundId backgroundId)
        {
            _background = Background(backgroundId);
        }

        public void LoadDungeonContent(ContentManager content)
        {
            var enabledRooms = content.Load<int[][]>("DungeonEnabledRooms");
            var enemies = content.Load<int[][]>("DungeonEnemies");
            var backgrounds = content.Load<int[][]>("DungeonRoomBackgrounds");
            var rows = enabledRooms.Length;
            _scenes = new Scene[rows][];
            _rooms = new Room[rows][];
            _backgroundIds = new BackgroundId[rows][];
            EnabledRooms = new bool[rows][];
            UnmappedRooms = new bool[rows][];
            VisitedRooms = new bool[rows][];

            for (var row = 0; row < rows; row++)
            {
                var cols = enabledRooms[row].Length;
                _scenes[row] = new Scene[cols];
                _rooms[row] = new Room[cols];
                _backgroundIds[row] = new BackgroundId[cols];
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
                    _rooms[row][col] = room;
                    _backgroundIds[row][col] = backgroundId;
                }
            }
        }

        public void LoadScenes(IPlayer player)
        {
            _player = player;
            for (var row = 0; row < _scenes.Length; row++)
            {
                for (var col = 0; col < _scenes[row].Length; col++)
                {
                    if (!EnabledRooms[row][col]) continue;
                    _scenes[row][col] = new Scene(_rooms[row][col], player);
                }
            }
        }

        public void ResetScenes()
        {
            for (var row = 0; row < _scenes.Length; row++)
            {
                for (var col = 0; col < _scenes[row].Length; col++)
                {
                    VisitedRooms[row][col] = false;
                    _scenes[row][col]?.ResetEnemies();
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
                _rooms[newRoom.Y][newRoom.X].Doors[DirectionUtility.Flip(roomDirection)]?.Unblock();
            }

            Scene?.DestroyProjectiles();
            Pan(CurrentRoom, newRoom, roomDirection);
        }

        public IDrawable BuildPanScene(int row, int column)
        {
            return new PanningScene(_rooms[row][column], Background(_backgroundIds[row][column]));
        }

        public void JumpToRoom(int row, int column, Direction facing = Direction.Up)
        {
            var oldRoom = CurrentRoom;
            VisitedRooms[row][column] = true;
            CurrentRoom = new Point(column, row);
            SetBackground(_backgroundIds[row][column]);
            if (oldRoom == BasementRoom && CurrentRoom == BasementAccessRoom)
            {
                _player?.Teleport(TileSize * new Point(6, 7), Direction.Down);
            }
            else if (CurrentRoom == BasementRoom)
            {
                _player?.Teleport(TileSize * new Point(3,2), Direction.Down);
            }
            else
            {
                _player?.Teleport(TeleportLocation.Calculate(facing), facing);
            }

            Scene?.DestroyProjectiles();
            Scene = _scenes[row][column];
            Scene.SpawnScene();
        }

        public void Update()
        {
            Scene?.Update();
        }

        public void Draw()
        {
            _background?.Draw(Vector2.Zero);
            Scene?.Draw();
        }
    }
}