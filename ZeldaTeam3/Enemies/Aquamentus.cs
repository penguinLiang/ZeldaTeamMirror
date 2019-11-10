using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Projectiles;
using Zelda.SoundEffects;

namespace Zelda.Enemies
{
    public class Aquamentus : EnemyAgent
    {
        private const int ActionDelay = 16;
        private Point _playerLocation;

        private ISprite _sprite;
        protected override ISprite Sprite => _sprite;
        public override Rectangle Bounds => Alive ? new Rectangle(Location, new Point(24, 32)) : Rectangle.Empty;

        private static readonly List<AgentState> ValidAgentStates = new List<AgentState>
        {
            AgentState.Ready,
            AgentState.Moving,
            AgentState.Halted,
            AgentState.Attacking
        };

        private readonly Point _origin;

        private int _agentClock;
        private Direction _currentDirection;
        private AgentState _agentStatus;

        public Aquamentus(Point location)
        {
            _origin  = location;
        }

        public override void Spawn()
        {
            base.Spawn();

            _sprite = EnemySpriteFactory.Instance.CreateAquamentusIdle();
            Location = _origin;
            Health = 6;
            _currentDirection = Direction.Down;
            _agentStatus = AgentState.Ready;
        }

        public override void TakeDamage(int damage)
        {
            if (Alive)
            {
                Health -= damage;
                Sprite?.PaletteShift();
                if (Health > 0)
                {
                    SoundEffectManager.Instance.PlayBossHurt();
                }
                else
                {
                    SoundEffectManager.Instance.PlayBossDead();
                }
            }
            else
            {
                Sprite?.Hide();
            }
        }

        private void ExecuteAction()
        {
            if (_agentClock > 0)
            {
                _agentClock--;
            }

            switch (_agentStatus)
            {
                case AgentState.Ready:
                    UpdateAction();
                    break;
                case AgentState.Halted:
                    if (_agentClock == 0)
                    {
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                case AgentState.Knocked:
                    if (_agentClock == 0)
                    {
                        FlipDirection();
                        _agentStatus = AgentState.Ready;
                    }
                    else
                    {
                        Move(_currentDirection);
                    }

                    break;
                case AgentState.Moving:
                    if (_agentClock == 0)
                    {
                        _agentStatus = AgentState.Ready;
                    }
                    else
                    {
                        Move(_currentDirection);
                    }

                    break;
                case AgentState.Attacking:
                    if (_agentClock == 0)
                    {
                        UseAttack();
                        _agentStatus = AgentState.Ready;
                        _sprite = EnemySpriteFactory.Instance.CreateAquamentusIdle();
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void UseAttack()
        {
            var fb0Location = new Point(Location.X, Location.Y - 4);
            var fb2Location = new Point(Location.X, Location.Y + 4);
            var velocityScalar = -1.5;

            Projectiles.Add(new Fireball(fb0Location, GenerateFireballVector(velocityScalar, -0.5), true));
            Projectiles.Add(new Fireball(Location, GenerateFireballVector(velocityScalar, 0), true));
            Projectiles.Add(new Fireball(fb2Location, GenerateFireballVector(velocityScalar, 0.5), true));
        }

        private Vector2 GenerateFireballVector(double xVelocity, double yVelocityOffset)
        {
            double xDiff = _playerLocation.X - Location.X;
            double yDiff = _playerLocation.Y - Location.Y;
            double magnitude = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);

            var normalizedY = yDiff / magnitude;

            return new Vector2((float) xVelocity, (float)(yVelocityOffset + normalizedY));
        }

        private void UpdateAction()
        {
            _agentStatus = AgentStateUtility.RandomFrom(ValidAgentStates);
            _agentClock = ActionDelay;
            var AttackScalar = 4;

            if (_agentStatus == AgentState.Moving)
            {
                _currentDirection = DirectionUtility.RandomVerticalDirection();
            }

            if (_agentStatus == AgentState.Attacking)
            {
                _agentClock *= AttackScalar;
                _sprite = EnemySpriteFactory.Instance.CreateAquamentusFiring();
            }
        }

        public override void Knockback()
        {
            _agentStatus = AgentState.Knocked;
            _agentClock = ActionDelay / 2;
            FlipDirection();
        }

        public override void Halt()
        {
            _agentStatus = AgentState.Halted;
            _agentClock = ActionDelay;
            FlipDirection();
            Move(_currentDirection);
        }

        public override void Stun()
        {
            throw new NotImplementedException();
        }

        private void FlipDirection()
        {
            _currentDirection = DirectionUtility.Flip(_currentDirection);
        }

        public override void Target(Point playerLocation)
        {
            _playerLocation = playerLocation;
        }

        public override void Update()
        {
            base.Update();
            if (Alive && CanMove)
                ExecuteAction();
        }

        public override ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }
    }
}
