using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.ShaderEffects;

// ReSharper disable SwitchStatementMissingSomeCases (missing cases handled at run time)
namespace Zelda.Blocks
{
    internal class RupeeBarrier : IBarricade
    {
        private BlockType _block;
        private ISprite _sprite;
        public Rectangle Bounds { get; private set; }
        private Point _location;
        public bool Unlocked { get; set; }

        private static BlockType UnlockedType()
        {
            return BlockType.InvisibleBlock;
        }

        public RupeeBarrier(Point location, BlockType block)
        {
            _block = block;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            _location = location;
            Bounds = new Rectangle(_location, new Point(20, 20));

            if (!Unlocked) return;
            _block = BlockType.InvisibleBlock;
            Bounds = new Rectangle(_location, new Point(0, 0));
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
        }

        public void Reset()
        {
            _block = BlockType.RupeeBarrier;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            Bounds = new Rectangle(_location, new Point(20, 20));
            Unlocked = false;
        }

        public void Unlock()
        {
            Unlocked = true;
            _block = BlockType.InvisibleBlock;
            Bounds = new Rectangle(_location, new Point(0, 0));
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(UnlockedType()), true);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            if (Unlocked) return new NoOp();

            return new MoveableHalt(player);
        }
        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
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
            _sprite?.Draw(_location.ToVector2());
        }
    }

}
