using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Zelda.Dungeon;
// ReSharper disable ForCanBeConvertedToForeach

namespace Zelda.Survival
{
    public class SurvivalManager : IDrawable, IDungeonManager
    {
        private static readonly Point TileSize = new Point(16, 16);

        public IScene Scene { get; protected set; }
        private ISprite _background;
        private SurvivalScene[][] _scenes;
        private IPlayer _player;
        private SurvivalRoom[][] _rooms ;
        private WaveManager _waveManager;
        public Point CurrentRoom { get; protected set; } = Point.Zero;

        private static ISprite Background(bool inShop)
        {
            return inShop ?
                BackgroundSpriteFactory.Instance.CreateSurvivalShopBackground() : BackgroundSpriteFactory.Instance.CreateSurvivalDungeonBackground();
        }

        private void SetBackground(bool inShop)
        {
            _background = Background(inShop);
        }

        public void LoadDungeonContent(ContentManager content)
        {
            _rooms = new [] {
                new []
                {
                    new SurvivalRoom(this, content.Load<int[][]>($"Shop/ShopTiles"))
                },
                new []
                {
                    new SurvivalRoom(this, content.Load<int[][]>($"Rooms/Survival-Dungeon"))
                }
            };

            var rows = _rooms.Length;
            _scenes = new SurvivalScene[rows][];
            for (var row = 0; row < rows; row++)
            {
                var cols = _rooms[row].Length;
                _scenes[row] = new SurvivalScene[cols];
            }
            _waveManager = new WaveManager(this, _rooms[0][0], content.Load<string[][]>("SurvivalWaves"));

            /* TODO: Implement */
        }

        public void LoadScenes(IPlayer player)
        {
            _player = player;
            for (var row = 0; row < _scenes.Length; row++)
            {
                for (var col = 0; col < _scenes[row].Length; col++)
                {
                    _scenes[row][col] = new SurvivalScene(_rooms[row][col], player);
                }
            }
        }

        public void ResetScenes()
        {
            // NO-OP: Not necessary for now
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
            JumpToRoom(newRoom.Y, newRoom.X);
        }

        public void JumpToRoom(int row, int column, Direction facing = Direction.Up)
        {
            CurrentRoom = new Point(column, row);
            var inShop = row == 0;
            SetBackground(inShop);
            //_waveManager.InShop = inShop;
            if (inShop)
            {
                _player?.Teleport(TileSize * new Point(27, 60) + new Point(8, 0), Direction.Up);
            }
            else
            {
                _player?.Teleport(TileSize * new Point(23, 2) + new Point(8, 0), Direction.Down);
            }

            Scene?.DestroyProjectiles();
            Scene = _scenes[row][column];
            Scene.SpawnScene();
        }

        public void Update()
        {
            //_waveManager.Update();
            Scene?.Update();
        }

        public void Draw()
        {
            _background?.Draw(Vector2.Zero);
            Scene?.Draw();
        }
    }
}