using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;
using Zelda.ShaderEffects;
using Zelda.SoundEffects;

// ReSharper disable SwitchStatementMissingSomeCases (missing cases handled at run time)
namespace Zelda.Blocks
{
    internal class KeyBarrierCenter : IBarricade
    {
        private BlockType _block;
        // protected override ISprite Sprite => _sprite;
        // protected override ICommand TransitionEffect { get; }
        private ISprite _sprite;
        public bool unlocked { get; set; }
        public Rectangle Bounds { get; private set; }
        private Point _location;
       // private KeyBarrierStateMachine _keyState {get; set;}

        private static BlockType UnlockedType(BlockType block)
        {
            return BlockType.InvisibleBlock;
        }

        public KeyBarrierCenter(Point location, BlockType block)
        {
            _block = block;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            _location = location;
            unlocked = false;
            Bounds = new Rectangle(location, new Point(32, 32));
    }

    public void Reset()
        {
            _block = BlockType.KeyBarrierCenter;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(_block), true);
            Bounds = new Rectangle(_location, new Point(32, 32));
            unlocked = false;
        }

        public void Unlock()
        {
            unlocked = true;
           // SoundEffectManager.Instance.PlayDoorUnlock();

            //Collision remains
            //take out the other blocks near you
            //no more collision, block replaced by sand?
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(UnlockedType(_block)), true);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            if (!unlocked &&(player.BodyCollision.CollidesWith(Bounds) && player.Inventory.TryRemoveKey()))
            {
                // SoundEffectManager.Instance.PlayDoorUnlock();
                Unlock();
                return new NoOp();
                //if link is unlocking the block, it shouldn't halt him
            }
           else if (!unlocked)
                return new MoveableHalt(player);
            else
                return new NoOp();
        }
        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }


        public ICommand EnemyEffect(IEnemy enemy)
        {
            if (unlocked)
            {
                return new NoOp();
            }else
            return new MoveableHalt(enemy);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            if (unlocked)
            {
                return new NoOp();
            }
            else
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
