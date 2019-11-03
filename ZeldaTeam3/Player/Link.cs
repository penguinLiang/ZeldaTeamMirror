using Microsoft.Xna.Framework;

namespace Zelda.Player
{
    internal class Link : IPlayer
    {
        public readonly MovementStateMachine _movementStateMachine;
        private AliveSpriteStateMachine _aliveSpriteStateMachine;
        private DeadSpriteStateMachine _deadSpriteStateMachine;
        private HealthStateMachine _healthStateMachine;
        private SecondaryItemAgent _secondaryItemAgent ;

        // Prevents key queue from messing with movement after immediately teleporting
        private bool _teleportLock;
        private readonly FrameDelay _teleportLockDelay = new FrameDelay(15, true);

        public Inventory Inventory { get; } = new Inventory();
        public bool Alive => _healthStateMachine.Alive;
        public int Health => _healthStateMachine.Health;
        public int MaxHealth => _healthStateMachine.MaxHealth;
        public Point Location => _secondaryItemAgent.projectileLocation;
        public Direction Direction => _secondaryItemAgent._facing;

        public bool UsingPrimaryItem => _aliveSpriteStateMachine.UsingPrimaryItem;
        public bool UsingSecondaryItem => _secondaryItemAgent.UsingSecondaryItem;

        public ICollideable BodyCollision => new PlayerBodyCollision(_movementStateMachine);

        public ICollideable SwordCollision => new PlayerSwordCollision(_movementStateMachine);

        public Link(Point location)
        {
            _movementStateMachine = new MovementStateMachine(location);
            Spawn();
        }

        public void Move(Direction direction)
        {
            if (!Alive || _teleportLock || _aliveSpriteStateMachine.UsingItem) return;
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
            _healthStateMachine = new HealthStateMachine();
            _aliveSpriteStateMachine = new AliveSpriteStateMachine(_movementStateMachine.Facing);
            _deadSpriteStateMachine = new DeadSpriteStateMachine();
            _secondaryItemAgent = new SecondaryItemAgent();
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
            Inventory.AssignSecondaryItem(item);
            _secondaryItemAgent.AssignSecondaryItem(item);
        }

        public void Teleport(Point location, Direction facing)
        {
            _aliveSpriteStateMachine.Aim(facing);
            _movementStateMachine.Teleport(location, facing);
            _teleportLock = true;
            _teleportLockDelay.Resume();
        }

        public void Update()
        {
            if (!Alive)
            {
                _deadSpriteStateMachine.Update();
                return;
            }

            _teleportLockDelay.Update();
            if (!_teleportLockDelay.Delayed)
            {
                _teleportLock = false;
                _teleportLockDelay.Pause();
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

            // ReSharper disable once ConvertIfStatementToSwitchStatement (Makes less sense as switch)
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
                _deadSpriteStateMachine.Sprite?.Draw(_movementStateMachine.Location.ToVector2());
            }
        }
    }
}
