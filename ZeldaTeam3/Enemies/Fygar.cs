using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Projectiles;

namespace Zelda.Enemies
{
    public class Fygar : EnemyAgent
    {
        private const int ActionDelay = 16;

        private static readonly Random Rng = new Random();

        private static readonly Point Size = new Point(13, 13);
        public override Rectangle Bounds => new Rectangle(Location, Alive ? Size : Point.Zero);
        private ISprite _sprite;
        protected override ISprite Sprite => _sprite;
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
        private Point _playerLocation;
        private Direction _facing;

        public Fygar(Point location)
        {
            _origin  = location;
        }

        public override void Spawn()
        {
            base.Spawn();

            _sprite = EnemySpriteFactory.Instance.CreateFygarFaceLeft();
            _facing = Direction.Left;
            Location = _origin;
            Health = 2;
            _agentStatus = AgentState.Ready;
            _currentDirection = Direction.Down;
        }

        protected override void Move(Direction direction, int speed = 1)
        {
            base.Move(direction, speed);
            if (direction == Direction.Right)
            {
                _sprite = EnemySpriteFactory.Instance.CreateFygarFaceRight();
                _facing = Direction.Right;
            }
            else if(direction == Direction.Left)
            {
                _sprite = EnemySpriteFactory.Instance.CreateFygarFaceLeft();
                _facing = Direction.Left;
            }
        }

        protected override void Knockback()
        {
            _agentStatus = AgentState.Knocked;
            _agentClock = ActionDelay;
            _currentDirection = DirectionUtility.Flip(_currentDirection);
        }

        public override void Halt()
        {
            _agentStatus = AgentState.Halted;
            _agentClock = ActionDelay;
            _currentDirection = DirectionUtility.Flip(_currentDirection);
            base.Move(_currentDirection, 2);
        }

        public override void Stun()
        {
            _agentClock = 240;
            _agentStatus = AgentState.Stunned;
        }

        public bool IsFacingPlayer()
        {
            var xDiff = _playerLocation.X - Location.X;

            return xDiff > 0 ? _facing == Direction.Right : _facing == Direction.Left;
        }

        public void UpdateAction()
        {
            _agentClock = ActionDelay;
            _agentStatus = AgentStateUtility.RandomFrom(ValidAgentStates);

            if (_agentStatus == AgentState.Moving)
            {

                _currentDirection = Rng.Next(2) == 0
                    ? DirectionUtility.RandomDirection()
                    : DirectionUtility.GetDirectionTowardsPoint(Location, _playerLocation);
            }
            
            if (_agentStatus == AgentState.Attacking)
            {
                _agentClock *= 4;
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
                case AgentState.Stunned:
                case AgentState.Halted:
                    if (_agentClock == 0)
                    {
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                case AgentState.Knocked:
                    if (_agentClock == 0)
                    {
                        _agentStatus = AgentState.Ready;
                    }
                    else
                    {
                        base.Move(_currentDirection, 2);
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
                    if (!IsWithinCircularBounds(_playerLocation, Location, 192))
                    {
                        _agentStatus = AgentState.Ready;
                        return;
                    }

                    if (_agentClock == 0)
                    {
                        if (IsFacingPlayer())
                        {
                            UseAttack();
                        }
                        
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        private void UseAttack()
        {
            const double velocityScalar = 1.5;

            Projectiles.Add(new Fireball(Location, GenerateFireballVector(velocityScalar, 0), true));
        }

        private Vector2 GenerateFireballVector(double xVelocity, double yVelocityOffset)
        {
            double xDiff = _playerLocation.X - Location.X;
            double yDiff = _playerLocation.Y - Location.Y;
            var magnitude = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);

            var normalizedY = yDiff / magnitude;

            return new Vector2((float)xVelocity * Math.Sign(xDiff), (float)(yVelocityOffset + normalizedY));
        }

        public override void Target(Point playerLocation)
        {
            _playerLocation = playerLocation;
        }

        public override void Update()
        {
            if (Alive && CanMove)
                ExecuteAction();

            base.Update();
        }

        public override ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }
    }
}
