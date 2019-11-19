using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Zelda.Survival
{
    public class SurvivalManager : IDrawable
    {
        private static readonly Point TileSize = new Point(16, 16);

        public SurvivalScene Scene { get; private set; }
        private ISprite _background;
        private IPlayer _player;
        private SurvivalRoom _shopRoom;
        private SurvivalRoom _dungeonRoom;
        private SurvivalScene _shopScene;
        private SurvivalScene _dungeonScene;
        private BackgroundId _dungeonBackground;
        private BackgroundId _shopBackground;

        //public bool CurrentRoomMapped => //!UnmappedRooms[CurrentRoom.Y][CurrentRoom.X];

        private enum BackgroundId
        {
            Dungeon = 0,
            Shop = 1
        }

        private static ISprite Background(BackgroundId backgroundId)
        {
            /*switch (backgroundId)
            {
                case BackgroundId.Default:
                    return BackgroundSpriteFactory.Instance.CreateDungeonBackground();
                default:
                    return BackgroundSpriteFactory.Instance.CreateDungeonBackground();
            }*/
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