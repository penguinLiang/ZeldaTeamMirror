
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Blocks
{
    internal class Barrier : ICollideable, IDrawable, IUpdatable
    {
        private ISprite _sprite;
        private readonly Vector2 _drawLocation;
        private Rectangle _bounds;
        //        private readonly ISprite _sprite = ItemSpriteFactory.Instance.CreateArrow();


        private BlockType _block;
        private int _locationx;
        private int _locationy;

        public Barrier(Point location, BlockType block)

        {
            _locationx = location.X;
           _locationy = location.Y;
            _bounds = new Rectangle(_locationx, _locationy, 32, 32);
            _drawLocation = new Vector2(_locationx + 8, _locationy + 8);

            _block = block;

            GetSpriteAndBounds();

        }

        private void GetSpriteAndBounds()
        {
            //Use the blocktype to determine the sprite and the bounds for the blocks
            switch (_block)
            {
                case BlockType.WallLeft:
                    _sprite = BlockSpriteFactory.Instance.CreateLeftWall();
                    break;
                case BlockType.WallBottom:
                    _sprite = BlockSpriteFactory.Instance.CreateBottomWall();
                    break;
                case BlockType.WallRight:
                    _sprite = BlockSpriteFactory.Instance.CreateRightWall();
                    break;
                case BlockType.WallTop:
                    _sprite = BlockSpriteFactory.Instance.CreateTopWall();
                    break;
                case BlockType.BombableWallBottom:
                    _sprite = BlockSpriteFactory.Instance.CreateBottomWallHole();
                    break;
                case BlockType.BombableWallLeft:
                    _sprite = BlockSpriteFactory.Instance.CreateLeftWallHole();
                    break;
                case BlockType.BombableWallRight:
                    _sprite = BlockSpriteFactory.Instance.CreateRightWallHole();
                    break;
                case BlockType.BombableWallTop:
                    _sprite = BlockSpriteFactory.Instance.CreateTopWallHole();
                    break;
                case BlockType.DoorLockedDown:
                    _sprite = BlockSpriteFactory.Instance.CreateBottomLockedDoor();
                    break;
                case BlockType.DoorLockedLeft:
                    _sprite = BlockSpriteFactory.Instance.CreateLeftLockedDoor();
                    break;
                case BlockType.DoorLockedRight:
                    _sprite = BlockSpriteFactory.Instance.CreateRightLockedDoor();
                    break;
                case BlockType.DoorLockedUp:
                    _sprite = BlockSpriteFactory.Instance.CreateTopLockedDoor();
                    break;
                case BlockType.DragonStatue:
                    _sprite = BlockSpriteFactory.Instance.CreateStatue2();
                    break;
                case BlockType.FishStatue:
                    _sprite = BlockSpriteFactory.Instance.CreateStatue1();
                    _bounds = new Rectangle(_locationx, _locationy, 16, 16);
                    break;
                case BlockType.Fire:
                    _sprite = BlockSpriteFactory.Instance.CreateFire();
                    _bounds = new Rectangle(_locationx, _locationy, 16, 16);
                    break;
                case BlockType.ImmovableBlock:
                    _sprite = BlockSpriteFactory.Instance.CreateSolidBlock();
                    _bounds = new Rectangle(_locationx, _locationy, 16, 16);
                    break;
                case BlockType.InvisibleBlock:
                    _sprite = BlockSpriteFactory.Instance.CreateBlackTile();
                    _bounds = new Rectangle(_locationx, _locationy, 16, 16);
                    break;
                case BlockType.Water:
                    _sprite = BlockSpriteFactory.Instance.CreateWater();
                    _bounds = new Rectangle(_locationx, _locationy, 16, 16);
                    break;
            }
        }

        public bool CollidesWith(Rectangle rect)
        {
            return _bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return new MoveableHalt(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new MoveableHalt(enemy);
        }

        public ICommand ProjectileEffect(IHaltable projectile)
        {
            return new MoveableHalt(projectile);
        }

        public void Update()
        {
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_drawLocation);
        }
    }
}
