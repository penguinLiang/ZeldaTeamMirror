using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Projectiles;

namespace Zelda.Enemies
{
    public class Aquamentus : EnemyAgent
    {
        private const int ActionDelay = 16;
        private Point _playerLocation;

        private ISprite _sprite;
        protected override ISprite Sprite => _sprite;
        public override Rectangle Bounds => new Rectangle(Location, new Point(24, 32));

        private static readonly List<AgentState> ValidAgentStates = new List<AgentState>
        {
            AgentState.Ready,
            AgentState.Moving,
            AgentState.Halted,
            AgentState.Attacking
        };

        private Fireball[] _fireballs = new Fireball[3];

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
            Point fb0Location = new Point(Location.X, Location.Y - 4);
            Point fb2Location = new Point(Location.X, Location.Y + 4);
            var velocityScalar = -1.5;
            _fireballs[0] = new Fireball(fb0Location, GenerateFireballVector(velocityScalar, -0.5), true);
            _fireballs[1] = new Fireball(Location, GenerateFireballVector(velocityScalar, 0), true);
            _fireballs[2] = new Fireball(fb2Location, GenerateFireballVector(velocityScalar, 0.5), true);
        }

        private Vector2 GenerateFireballVector(double xVelocity, double yVelocityOffset)
        {
            double xDiff = _playerLocation.X - Location.X;
            double yDiff = _playerLocation.Y - Location.Y;
            double magnitude = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);

            //var normalizedX = xDiff / magnitude;
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

        public override void Update(Point playerLocation)
        {
            _playerLocation = playerLocation;
            base.Update(playerLocation);
            foreach (Fireball fb in _fireballs)
            {
                fb?.Update();
            }

            if (Alive && CanMove)
                ExecuteAction();
        }

        public override void Draw()
        {
            base.Draw();
            foreach (Fireball fb in _fireballs)
            {
                fb?.Draw();
            }
        }
    }
}
