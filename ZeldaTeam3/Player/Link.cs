using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Items;
using Zelda.SoundEffects;

namespace Zelda.Player
{
    internal class Link : IPlayer
    {
        private readonly MovementStateMachine _movementStateMachine;
        private AliveSpriteStateMachine _aliveSpriteStateMachine;
        private DeadSpriteStateMachine _deadSpriteStateMachine;
        private HealthStateMachine _healthStateMachine;
        private PlayerProjectileAgent _playerProjectileAgent;

        // Prevents key queue from messing with movement after immediately teleporting
        private bool _teleportLock;
        private readonly FrameDelay _teleportLockDelay = new FrameDelay(15, true);

        private bool _partyHard;
        
        public Inventory Inventory { get; } = new Inventory();
        public bool Alive => _healthStateMachine.Alive;
        public bool Won { get; private set; }
        public int Health => _healthStateMachine.Health;
        public int MaxHealth => _healthStateMachine.MaxHealth;
        public Point Location => _movementStateMachine.Location;
        public Direction Direction => _movementStateMachine.Facing;
        public List<IProjectile> Projectiles => _playerProjectileAgent.Projectiles;

        public bool UsingPrimaryItem => _aliveSpriteStateMachine.UsingPrimaryItem;
        public bool UsingSecondaryItem => _playerProjectileAgent.UsingSecondaryItem;

        public ICollideable BodyCollision => new PlayerBodyCollision(this, _partyHard || _healthStateMachine.Invulnerable);

        public ICollideable SwordCollision => new PlayerSwordCollision(_movementStateMachine, Inventory.SwordLevel);

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
            _playerProjectileAgent = new PlayerProjectileAgent(this);
        }

        public void TakeDamage(int damage)
        {
            if(_partyHard || _healthStateMachine.Hurt || _healthStateMachine.Invulnerable) return;
            Halt();
            _healthStateMachine.TakeDamage(damage);
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
            SoundEffectManager.Instance.PlaySwordSlash();

            if (Health != MaxHealth) return;
            SoundEffectManager.Instance.PlaySwordShoot();
            _playerProjectileAgent.FireSwordBeam(_movementStateMachine.Facing, _movementStateMachine.Location,
                Inventory.SwordLevel);
        }

        public void UseSecondaryItem()
        {
            if (_aliveSpriteStateMachine.UsingItem) return;
            _playerProjectileAgent.UseSecondaryItem(_movementStateMachine.Facing, _movementStateMachine.Location);
            if (UsingSecondaryItem) _aliveSpriteStateMachine.UseSecondaryItem();
        }

        public void AssignSecondaryItem(Secondary item)
        {
            Inventory.AssignSecondaryItem(item);
            _playerProjectileAgent.AssignSecondaryItem(item);
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

            if (_healthStateMachine.Hurt)
            {
                _aliveSpriteStateMachine.Sprite.PaletteShift();
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
            _playerProjectileAgent.Update();

            if (!_aliveSpriteStateMachine.UsingItem) _movementStateMachine.Update();
            _healthStateMachine.Update();
        }

        public void TouchTriforce()
        {
            Won = true;
        }

        public void MakeInvulnerable()
        {
            _healthStateMachine.MakeInvulnerable();
            _aliveSpriteStateMachine.Sprite.PaletteShift();
        }

        public void PartyHard()
        {
            _partyHard = true;
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
            _playerProjectileAgent.Draw();
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
