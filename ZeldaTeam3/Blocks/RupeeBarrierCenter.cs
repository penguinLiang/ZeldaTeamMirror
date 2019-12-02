using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.ShaderEffects;

// ReSharper disable SwitchStatementMissingSomeCases (missing cases handled at run time)
namespace Zelda.Blocks
{
    internal class RupeeBarrierCenter : IBarricade
    {
        private BlockType _block;
        private ISprite _sprite;
        public bool Unlocked { get; set; }
        public Rectangle Bounds { get; private set; }
        private Point _location;
        private readonly int _price;

        private static BlockType UnlockedType()
        {
            return BlockType.InvisibleBlock;
        }

        public RupeeBarrierCenter(Point location, BlockType block, int price = 1)
        {
            _price = price;
            _block = block;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            _location = location;
            Unlocked = false;
            Bounds = new Rectangle(location, new Point(32, 32));
        }

        public void Reset()
        {
            _block = BlockType.RupeeBarrierCenter;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            Bounds = new Rectangle(_location, new Point(32, 32));
            Unlocked = false;
        }

        public void Unlock()
        {
            Unlocked = true;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(UnlockedType()), true);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            if (!Unlocked && player.BodyCollision.CollidesWith(Bounds) && player.Inventory.TryRemoveRupee(_price))
            {
                Unlock();
                return new NoOp();
            }

            if (!Unlocked)
                return new MoveableHalt(player);
            return new NoOp();
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }


        public ICommand EnemyEffect(IEnemy enemy)
        {
            if (Unlocked)
            {
                return new NoOp();
            }

            return new MoveableHalt(enemy);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            if (Unlocked)
            {
                return new NoOp();
            }

            return new MoveableHalt(projectile);
        }

        public void Update()
        {
            _sprite?.Update();
        }

        public void Draw()
        {
            _sprite?.Draw(_location.ToVector2());
        }
    }

}
