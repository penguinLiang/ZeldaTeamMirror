using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;

namespace Zelda.Blocks
{
    internal class LeftRightDoor : ICollideable, IDrawable, IActivatable
    {
        private readonly ISprite _sprite;
        public Rectangle Bounds { get; }
        private DungeonManager _dungeonManager;
        private readonly Vector2 _drawLocation;
        private BlockType _block;
        private bool _lockedOrBlocked = false;

        private Rectangle _leftWallOne {get;}
        private Rectangle _leftWallTwo {get;}
        private Rectangle _leftTransitionSpace {get;}
        private Rectangle _rightWallOne {get;}
        private Rectangle _rightWallTwo {get;}
        private Rectangle _rightTransitionSpace {get;}

        public LeftRightDoor(DungeonManager dungeon, Point location, BlockType block)
        {
            Bounds = new Rectangle(location.X, location.Y, 32, 48);

            _leftWallOne = new Rectangle(location.X + 16, location.Y, 16, 16);
            _leftWallTwo = new Rectangle(location.X + 16, location.Y + 32, 16, 16);
            _leftTransitionSpace = new Rectangle(location.X, location.Y + 16, 16, 16);

            _rightWallOne = new Rectangle(location.X, location.Y, 16, 16);
            _rightWallTwo = new Rectangle(location.X, location.Y + 32, 16, 16);
            _rightTransitionSpace = new Rectangle(location.X + 16, location.Y + 16, 16, 16);

            _sprite = BlockTypeSprite.Sprite(block);
            _drawLocation = new Vector2(location.X, location.Y + 8);
            _dungeonManager = dungeon;
            _block = block;
            if((_block == BlockType.BombableWallLeft) || (_block == BlockType.BombableWallRight)
                || (_block == BlockType.DoorLockedLeft) || (_block == BlockType.DoorLockedRight)) 
                {
                    _lockedOrBlocked = true;
                }
        }


        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            if(_block == BlockType.DoorRight && _lockedOrBlocked == false) 
            {
                if(player.BodyCollision.CollidesWith(_rightWallOne) || player.BodyCollision.CollidesWith(_rightWallTwo))
                {
                    return new MoveableHalt(player);
                }
                if(player.BodyCollision.CollidesWith(_rightTransitionSpace))
                {
                    return new Transition(_dungeonManager, Direction.Right);
                }
                return new NoOp();
            }
            if(_block == BlockType.DoorLeft && _lockedOrBlocked == false) 
            {
                if(player.BodyCollision.CollidesWith(_leftWallOne) || player.BodyCollision.CollidesWith(_leftWallTwo))
                {
                    return new MoveableHalt(player);
                }
                if(player.BodyCollision.CollidesWith(_leftTransitionSpace))
                {
                    return new Transition(_dungeonManager, Direction.Left);
                }
                return new NoOp();
            }
            return new MoveableHalt(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new MoveableHalt(enemy);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return new MoveableHalt(projectile);
        }

        public void Update()
        {
            _sprite?.Update();
        }

        public void Draw()
        {
            _sprite?.Draw(_drawLocation);
        }

        public void Activate()
        {
            // NO-OP
        }
    }
}
