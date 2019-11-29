using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;
using Zelda.ShaderEffects;
using Zelda.SoundEffects;

// ReSharper disable SwitchStatementMissingSomeCases (missing cases handled at run time)
namespace Zelda.Blocks
{
    internal class KeyBarrier : IBarricade
    {
        private BlockType _block;
        // protected override ISprite Sprite => _sprite;
        // protected override ICommand TransitionEffect { get; }
        private ISprite _sprite;
        private bool _unlocked {get; set;}
        public Rectangle Bounds { get; private set; }
        private Point _location;
        private KeyBarrierStateMachine _keyState { get; set; }
       public bool unlocked { get; set; }

        private static BlockType UnlockedType(BlockType block)
        {
            return BlockType.Sand;
        }

        public KeyBarrier(Point location, BlockType block)
        {
            _block = block;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            _location = location;
            _keyState = new KeyBarrierStateMachine();
            unlocked = false;
            Bounds = new Rectangle(_location, new Point(20, 20));
           //unlocked = (Check collisions for KeyBarrierCenter -> Unlocked = keybarrerCenter.unlock)

            if (_keyState._unlocked)
            {
                _block = BlockType.InvisibleBlock;
                Bounds = new Rectangle(_location, new Point(0, 0));
                _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            }
        }

        public void Reset()
        {
            _block = BlockType.KeyBarrier;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            Bounds = new Rectangle(_location, new Point(20, 20));
            _unlocked = false;
        }

        public void Unlock()
        {
            _unlocked = true;
            _block = BlockType.InvisibleBlock;
            Bounds = new Rectangle(_location,new Point(0, 0));
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(UnlockedType(_block)), true);
            //The center will play the sound effect
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            if (_unlocked) return new NoOp();
            //check player has key

            // ReSharper disable once InvertIf (cleaner as-is)
           else
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
