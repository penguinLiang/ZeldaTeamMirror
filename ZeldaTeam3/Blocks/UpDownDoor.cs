using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;
using Zelda.Blocks;

namespace Zelda.Blocks
{
    internal class UpDownDoor : ICollideable, IDrawable, IActivatable
    {
        private readonly ISprite _sprite;
        public Rectangle Bounds { get; }
        private DungeonManager _dungeonManager;
        private BlockType _block;
        private readonly Vector2 _drawLocation;
        private bool _lockedOrBlocked = false;

        private Rectangle _upWallOne {get;}
        private Rectangle _upWallTwo {get;}
        private Rectangle _upTransitionSpace {get;}
        private Rectangle _downWallOne {get;}
        private Rectangle _downWallTwo {get;}
        private Rectangle _downTransitionSpace {get;}

        public UpDownDoor(DungeonManager dungeon, Point location, BlockType block)
        {
            Bounds = new Rectangle(location, new Point(32, 32));

            _upWallOne = new Rectangle(location.X, location.Y + 16, 8, 16);
            _upWallTwo = new Rectangle(location.X + 24, location.Y + 16, 8, 16);
            _upTransitionSpace = new Rectangle(location.X + 8, location.Y, 16, 16);

            _downWallOne = new Rectangle(location.X, location.Y, 8, 16);
            _downWallTwo = new Rectangle(location.X + 24, location.Y, 8, 16);
            _downTransitionSpace = new Rectangle(location.X + 8, location.Y + 16, 16, 16);

            _drawLocation = location.ToVector2();
            _sprite = BlockTypeSprite.Sprite(block);
            _dungeonManager = dungeon;
            _block = block;
            if((_block == BlockType.BombableWallTop) || (_block == BlockType.BombableWallBottom)
                || (_block == BlockType.DoorLockedUp) || (_block == BlockType.DoorLockedDown)) 
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
            if(_block == BlockType.DoorUp && _lockedOrBlocked == false) 
            {
                if(player.BodyCollision.CollidesWith(_upWallOne) || player.BodyCollision.CollidesWith(_upWallTwo))
                {
                    return new MoveableHalt(player);
                }
                if(player.BodyCollision.CollidesWith(_upTransitionSpace))
                {
                    return new Transition(_dungeonManager, Direction.Up);
                }
                return new NoOp();
            }
            if(_block == BlockType.DoorDown && _lockedOrBlocked == false) 
            {
                if(player.BodyCollision.CollidesWith(_downWallOne) || player.BodyCollision.CollidesWith(_downWallTwo))
                {
                    return new MoveableHalt(player);
                }
                if(player.BodyCollision.CollidesWith(_downTransitionSpace))
                {
                    return new Transition(_dungeonManager, Direction.Down);
                }
                return new NoOp();
            }
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
