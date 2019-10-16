using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Blocks
{
    class DoorsAndStairs : ICollideable, IDrawable, IActivatable
    {
        private readonly ISprite _sprite = BlockSpriteFactory.Instance.CreateRightOpenDoor();
        private readonly Vector2 _drawLocation;
        public Rectangle Bounds { get; private set; }
        private BlockType _block;
        private BlockType[] _allDoorsList;
        private BlockType[] _allStairsList;

        public DoorsAndStairs(Point location, BlockType block)
        {
            string designation = "None";
            _allDoorsList = new BlockType[] { BlockType.DoorUp, BlockType.DoorDown, BlockType.DoorRight, BlockType.DoorLeft
                , BlockType.DoorSpecialLeft2_1, BlockType.DoorSpecialRight3_1, BlockType.DoorSpecialUp1_1};

            _allStairsList = new BlockType[] { BlockType.DungeonStair, BlockType.BasementStair};

            var x = location.X;
            var y = location.Y;

            foreach(BlockType door in _allDoorsList)
            {
                if(door == block)
                {
                    designation = "door";
                    break;
                }
            }
            foreach (BlockType stair in _allStairsList)
            {
                if (stair == block)
                {
                    designation = "stair";
                    break;
                }
            }
            if (designation == "door")
            {
                Bounds = new Rectangle(x, y, 32, 32);
            }
            if (designation == "stair")
            {
                Bounds = new Rectangle(x, y, 16, 16);
            }
            _drawLocation = new Vector2(x + 8, y + 8);
            _block = block;
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return new LinkKnockback(player);
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

        public void Activate()
        {
            return;
        }
    }
}
