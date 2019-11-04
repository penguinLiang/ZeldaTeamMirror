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

        private void SetBackground(BackgroundId backgroundId)
        {
            switch (backgroundId)
            {
                case BackgroundId.Default:
                    _background = BackgroundSpriteFactory.Instance.CreateDungeonBackground();
                    break;
                case BackgroundId.Basement:
                    _background = BackgroundSpriteFactory.Instance.CreateBasementBackground();
                    break;
                case BackgroundId.OldMan:
                    _background = BackgroundSpriteFactory.Instance.CreateOldManBackground();
                    break;
                default:
                    _background = BackgroundSpriteFactory.Instance.CreateDungeonBackground();
                    break;
            }
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
                    _scenes[row][col] = new Scene(row, col, _rooms[row][col], player);
                }
            }
        }

        public void ResetScenes()
        {
            for (var row = 0; row < _scenes.Length; row++)
            {
                for (var col = 0; col < _scenes[row].Length; col++)
                {
                    _scenes[row][col]?.Reset();
                    VisitedRooms[row][col] = false;
                }
            }
        }

        public void Transition(Direction roomDirection)
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
            JumpToRoom(newRoom.Y, newRoom.X, roomDirection);
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

            Scene = _scenes[row][column];
            Scene.SpawnEnemies();
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