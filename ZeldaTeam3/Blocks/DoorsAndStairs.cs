using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Items;
using System.Collections.Generic;
using Zelda;

namespace Zelda.Blocks
{
    internal class DoorsAndStairs : ICollideable, IDrawable, IActivatable
    {
        private readonly ISprite _sprite = BlockSpriteFactory.Instance.CreateRightOpenDoor();
        private readonly Vector2 _drawLocation;
        private Rectangle _bounds;
        private BlockType _block;
        private LinkedList<BlockType> _allDoorsList;
        private LinkedList<BlockType> _allStairsList;

        public DoorsAndStairs(Point location, BlockType block)
        {
            _allDoorsList = new LinkedList<BlockType>();
            _allDoorsList.Add(BlockType.DoorUp);
            _allDoorsList.Add(BlockType.DoorDown);
            _allDoorsList.Add(BlockType.DoorRight);
            _allDoorsList.Add(BlockType.DoorLeft);
            _allDoorsList.Add(BlockType.DoorSpecialLeft2_1);
            _allDoorsList.Add(BlockType.DoorSpecialRight3_1);
            _allDoorsList.Add(BlockType.DoorSpecialUp1_1);

            _allStairsList = new LinkedList<BlockType>();
            _allStairsList.Add(BlockType.Stair1);
            _allStairsList.Add(BlockType.Stair2);
            _allStairsList.Add(BlockType.DungeonStair);
            _allStairsList.Add(BlockType.BasementStair);

            var x = location.X;
            var y = location.Y;
            if (_allDoorsList.Contains(block))
            {
                _bounds = new Rectangle(x, y, 32, 32);
            }
            if (_allStairsList.Contains(block))
            {
                _bounds = new Rectangle(x, y, 16, 16);
            }
            _drawLocation = new Vector2(x + 8, y + 8);
            _block = block;
            //Create the sprite from the block
        }

        public bool CollidesWith(Rectangle rect)
        {
            return _bounds.Intersects(rect);
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

        }
    }
}
