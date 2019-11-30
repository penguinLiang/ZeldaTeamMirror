using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;
using Zelda.ShaderEffects;
using Zelda.SoundEffects;

// ReSharper disable SwitchStatementMissingSomeCases (missing cases handled at run time)
namespace Zelda.Blocks
{
    internal class KeyBarrier : ICollideable, IDrawable
    {
        private readonly BlockType _block;
       // protected override ISprite Sprite => _sprite;
       // protected override ICommand TransitionEffect { get; }
        private ISprite _sprite;
        private bool _unlocked;
        public Rectangle Bounds { get; }
        private Point _location;


        private static BlockType UnlockedType(BlockType block)
        {
            return BlockType.Sand;
        }

        public KeyBarrier(ShopManager shop, Point location, BlockType block)
        {
            _block = block;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            _location = location;
        }

        public void Reset()
        {
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            _unlocked = false;
        }

        public void Unblock()
        {
            _unlocked = true;
            //take out the other blocks near you
            //no more collision, block replaced by sand?
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(UnlockedType(_block)), true);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            if (_unlocked) return new NoOp();
            //check player has key

            // ReSharper disable once InvertIf (cleaner as-is)
           // if (player.BodyCollision.CollidesWith(LocationOffset(NoOpArea)) && player.Inventory.TryRemoveKey())
           // {
             //   SoundEffectManager.Instance.PlayDoorUnlock();
              //  Unblock();
           // }

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
