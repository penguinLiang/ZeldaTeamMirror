using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Zelda.Dungeon;
// ReSharper disable ForCanBeConvertedToForeach

namespace Zelda.Survival
{
    public class SurvivalManager : IDungeonManager
    {
        private static readonly Point TileSize = new Point(16, 16);

        public IScene Scene { get; protected set; }
        private ISprite _background;
        private SurvivalScene[][] _scenes;
        private IPlayer _player;
        private SurvivalRoom[][] _rooms;
        private WaveManager _waveManager;
        public Point CurrentRoom { get; protected set; } = Point.Zero;
        public int CurrentWave => _waveManager.CurrentWave;

        private bool InShop => CurrentRoom.Y == 0;
        public bool PartyHard => _waveManager.CurrentWaveType == WaveType.Party && !InShop;

        private void SetBackground()
        {
            _background = InShop ?
                BackgroundSpriteFactory.Instance.CreateSurvivalShopBackground() : BackgroundSpriteFactory.Instance.CreateSurvivalDungeonBackground();
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

            _waveManager = new WaveManager(content.Load<string[][]>("SurvivalWaves"));
        }

        public void LoadScenes(IPlayer player)
        {
            _player = player;
            for (var row = 0; row < _scenes.Length; row++)
            {
                for (var col = 0; col < _scenes[row].Length; col++)
                {
                    _scenes[row][col] = new SurvivalScene(_rooms[row][col], _waveManager, player);
                }
            }
        }

        public void ResetScenes()
        {
            _waveManager.Reset();
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
            SetBackground();
            if (InShop)
            {
                _player?.Teleport(TileSize * new Point(27, 60) + new Point(8, 0), Direction.Up);
                _waveManager.AdvanceWave();
            }
            else
            {
                _player?.Teleport(TileSize * new Point(23, 2) + new Point(8, 0), Direction.Down);
                _waveManager.StartEnemyWave();
            }

            Scene?.DestroyProjectiles();
            Scene = _scenes[row][column];
            Scene.SpawnScene();
        }

        public void Update()
        {
            Scene?.Update();
            _waveManager.Update();

            if (InShop) return;

            _waveManager.TrackSpawnsNearPlayer(_rooms[1][0].SpawnTiles, _player.Location);

            if (!_waveManager.WaveComplete || _player.UsingPrimaryItem) return;

            _waveManager.ClearWave();
            _waveManager.AdvanceWave();
            if (_waveManager.CurrentWaveType == WaveType.Shop)
            {
                JumpToRoom(0, 0);
            }
            else
            {
                _waveManager.StartEnemyWave();
            }
        }

        public void Draw()
        {
            _background?.Draw(Vector2.Zero);
            Scene?.Draw();
        }
    }
}