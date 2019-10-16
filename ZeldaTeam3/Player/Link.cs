using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Enemies;

namespace Zelda.Player
{
    internal class Link : IPlayer
    {
        private readonly MovementStateMachine _movementStateMachine;
        private readonly AliveSpriteStateMachine _aliveSpriteStateMachine;
        private readonly DeadSpriteStateMachine _deadSpriteStateMachine = new DeadSpriteStateMachine();
        private readonly HealthStateMachine _healthStateMachine = new HealthStateMachine();
        private readonly SecondaryItemAgent _secondaryItemAgent = new SecondaryItemAgent();

        public Inventory Inventory { get; } = new Inventory();
        public bool Alive => _healthStateMachine.Alive;

        // Link only collides with the bottom half of his sprite, hence the offset by 8 in the y and the height only being 8
        public Rectangle Bounds => new Rectangle(_movementStateMachine.Location.X, _movementStateMachine.Location.Y + 8, 16, 8);

        public Link(Point location)
        {
            _movementStateMachine = new MovementStateMachine(location);
            _aliveSpriteStateMachine = new AliveSpriteStateMachine(_movementStateMachine.Facing);
        }

        public void Move(Direction direction)
        {
            if (!Alive || _aliveSpriteStateMachine.UsingItem) return;
            _movementStateMachine.Move(direction);
            _aliveSpriteStateMachine.Aim(direction);
            _deadSpriteStateMachine.Aim(direction);
        }

        public void Knockback()
        {
            _movementStateMachine.Knockback();
        }

        public void Halt()
        {
            _movementStateMachine.Halt();
        }
        
        public void Heal()
        {
            _healthStateMachine.Heal();
        }

        public void FullHeal()
        {
            _healthStateMachine.FullHeal();
        }

        public void AddHeart()
        {
            _healthStateMachine.AddHeart();
        }

        public void Spawn()
        {
            // NO-OP: No appearance animation
        }

        public void TakeDamage()
        {
            if(_healthStateMachine.Hurt) return;
            Halt();
            _healthStateMachine.TakeDamage();
            _movementStateMachine.Knockback();
            _aliveSpriteStateMachine.Sprite.PaletteShift();
        }

        public void Stun()
        {
            _movementStateMachine.Halt();
            _aliveSpriteStateMachine.Sprite.PaletteShift();
        }

        public void UsePrimaryItem()
        {
            if (_aliveSpriteStateMachine.UsingItem) return;
            _aliveSpriteStateMachine.UsePrimaryItem(Inventory.SwordLevel);
        }

        public void UseSecondaryItem()
        {
            if (_aliveSpriteStateMachine.UsingItem) return;
            _aliveSpriteStateMachine.UseSecondaryItem();
            _secondaryItemAgent.UseSecondaryItem(_movementStateMachine.Facing, _movementStateMachine.Location);
        }

        public void AssignSecondaryItem(Items.Secondary item)
        {
            _secondaryItemAgent.AssignSecondaryItem(item);
        }

        public void Update()
        {
            if (!Alive)
            {
                _deadSpriteStateMachine.Update();
                return;
            }

            if (!_aliveSpriteStateMachine.UsingItem && _movementStateMachine.Idling)
            {
                _aliveSpriteStateMachine.Sprite.PauseAnimation();
            }
            else
            {
                _aliveSpriteStateMachine.Sprite.PlayAnimation();
            }

            _aliveSpriteStateMachine.Update();
            _secondaryItemAgent.Update();

            if (!_aliveSpriteStateMachine.UsingItem) _movementStateMachine.Update();
            _healthStateMachine.Update();
        }

        // When using a primary item, the sprite is offset from the origin of the bounding box for the Left and Up directions by 16 pixels,
        // this was chosen in contrast to making every sprite 32x32 and calculating the origin of the bounding box for all directions
        private Vector2 AdjustedDrawLocation()
        {
            var drawLocation = _movementStateMachine.Location.ToVector2();
            if (!_aliveSpriteStateMachine.UsingPrimaryItem) return drawLocation;

            if (_movementStateMachine.Facing == Direction.Left)
            {
                drawLocation.X -= 16;
            }
            else if (_movementStateMachine.Facing == Direction.Up)
            {
                drawLocation.Y -= 16;
            }

            return drawLocation;
        }

        public void Draw()
        {
            _secondaryItemAgent.Draw();
            if (Alive)
            {
                _aliveSpriteStateMachine.Sprite.Draw(AdjustedDrawLocation());
            }
            else
            {
                _deadSpriteStateMachine.Sprite.Draw(_movementStateMachine.Location.ToVector2());
            }
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return NoOp.Instance;
        }

        public ICommand ProjectileEffect(IHaltable projectile)
        {
            return new MoveableHalt(projectile);
        }
    }
}
