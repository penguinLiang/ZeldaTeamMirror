using Microsoft.Xna.Framework;

namespace Zelda.Player
{
    public class Link : IPlayer
    {
        private readonly MovementStateMachine _movementStateMachine;
        private readonly SpriteStateMachine _spriteStateMachine;
        private readonly HealthStateMachine _healthStateMachine = new HealthStateMachine();
        private readonly SecondaryItemAgent _secondaryItemAgent = new SecondaryItemAgent();

        public Inventory Inventory { get; } = new Inventory();
        public bool Alive => _healthStateMachine.Alive;

        public Link(Point location)
        {
            _movementStateMachine = new MovementStateMachine(location);
            _spriteStateMachine = new SpriteStateMachine(_movementStateMachine.Facing);
        }

        public void Move(Direction direction)
        {
            if (_spriteStateMachine.UsingItem) return;
            _movementStateMachine.Move(direction);
            _spriteStateMachine.Aim(_movementStateMachine.Facing);
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

        
        public void Heal()
        {
            _healthStateMachine.Heal();
        }

        public void FullHeal()
        {
            _healthStateMachine.FullHeal();
        }

        public void Spawn()
        {
            _healthStateMachine.Spawn();
        }

        public void TakeDamage()
        {
            if(_healthStateMachine.Hurt) return;
            _healthStateMachine.TakeDamage();
            if(!_healthStateMachine.Alive){
                Kill();
            }
        }

        public void Kill()
        {     
                _spriteStateMachine.Dying = true;
               if(_spriteStateMachine.Dying){
                 _spriteStateMachine.Kill();
                }

        public void Stun()
        {
            _movementStateMachine.Halt();
            _spriteStateMachine.Sprite.PaletteShift();
        }

        public void UsePrimaryItem()
        {
            if (_spriteStateMachine.UsingItem) return;
            _spriteStateMachine.UsePrimaryItem();
        }

        public void UseSecondaryItem()
        {
            if (_spriteStateMachine.UsingItem) return;
            _spriteStateMachine.UseSecondaryItem();
            _secondaryItemAgent.UseSecondaryItem(_movementStateMachine.Facing, _movementStateMachine.Location);
        }

        public void AssignSecondaryItem(Items.Secondary item)
        {
            _spriteStateMachine.AssignSecondaryItem(item);
            _secondaryItemAgent.AssignSecondaryItem(item);
        }

        public void Update()
        {
            if (!_spriteStateMachine.UsingItem && _movementStateMachine.Idling)
            {
                _spriteStateMachine.Sprite.PauseAnimation();
            }
            else
            {
                _spriteStateMachine.Sprite.PlayAnimation();
            }

            if (_healthStateMachine.Hurt)
            {
                _spriteStateMachine.Sprite.PaletteShift();
            }

            _spriteStateMachine.Update();
            _secondaryItemAgent.Update();
            if (!_spriteStateMachine.UsingItem) _movementStateMachine.Update();
            _healthStateMachine.Update();

            if(!_healthStateMachine.Alive&&_spriteStateMachine.Dying){
                Kill();
            }
        }

        // When using a primary item, the sprite is offset from the origin of the bounding box for the Left and Up directions by 16 pixels,
        // this was chosen in contrast to making every sprite 32x32 and calculating the origin of the bounding box for all directions
        private Vector2 AdjustedDrawLocation()
        {
            var drawLocation = _movementStateMachine.Location.ToVector2();
            if (!_spriteStateMachine.UsingPrimaryItem) return drawLocation;

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
            _spriteStateMachine.Sprite.Draw(AdjustedDrawLocation());
        }
    }
}
