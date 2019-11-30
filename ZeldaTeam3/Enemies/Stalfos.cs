using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Stalfos : EnemyAgent
    {
        private const int ActionDelay = 16;

        private static readonly Random Rng = new Random();

        private static readonly Point Size = new Point(16, 16);
        public override Rectangle Bounds => new Rectangle(Location, Alive ? Size : Point.Zero);
        private ISprite _sprite;
        protected override ISprite Sprite => _sprite;
        private static readonly List<AgentState> ValidAgentStates = new List<AgentState>
        {
            AgentState.Ready,
            AgentState.Moving,
            AgentState.Halted
        };

        private readonly Point _origin;

        private int _agentClock;
        private Direction _currentDirection;
        private AgentState _agentStatus;
        private Point _playerLocation;

        public Stalfos(Point location)
        {
            _origin  = location;
        }

        public override void Spawn()
        {
            base.Spawn();

            _sprite = EnemySpriteFactory.Instance.CreateStalfos();
            Location = _origin;
            Health = 2;
            _agentStatus = AgentState.Ready;
            _currentDirection = Direction.Down;
        }

        private void FlipDirection()
        {
            _currentDirection = DirectionUtility.Flip(_currentDirection);
        }

        public override void Target(Point playerLocation)
        {
            _playerLocation = playerLocation;
        }

        public override void Stun()
        {
            _agentClock = 240;
            _agentStatus = AgentState.Stunned;
        }

        protected override void Knockback()
        {
            _agentStatus = AgentState.Knocked;
            _agentClock = ActionDelay;
            FlipDirection();
        }

        public override void Halt()
        {
            _agentStatus = AgentState.Halted;
            _agentClock = ActionDelay;
            FlipDirection();
            Move(_currentDirection,2);
        }

        public void UpdateAction()
        {
            _agentStatus = AgentStateUtility.RandomFrom(ValidAgentStates);
            if (_agentStatus == AgentState.Moving)
            {
                _currentDirection = Rng.Next(2)==0 
                    ? DirectionUtility.GetDirectionTowardsPoint(Location, _playerLocation)
                    : DirectionUtility.RandomDirection();
            }
            _agentClock = ActionDelay;
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
                        Move(_currentDirection, 2);
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
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void Update()
        {
            base.Update();
            if (Alive && CanMove)
                ExecuteAction();

            
        }
    }
}
