using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
// ReSharper disable SwitchStatementMissingSomeCases

namespace Zelda.Enemies
{
    public class WallMaster : EnemyAgent
    {
        private const int ActionDelay = 16;
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
        private int _actionCount;

        public WallMaster(Point location)
        {
            _origin = location;
        }

        public override void Target(Point playerLocation)
        {
            _playerLocation = playerLocation;
        }

        public override void Spawn()
        {
            base.Spawn();
            _sprite = EnemySpriteFactory.Instance.CreateWallMaster();
            Health = 2;
            Location = _origin;
            _currentDirection = Direction.Down;
        }

        private void FlipDirection()
        {
            _currentDirection = DirectionUtility.Flip(_currentDirection);
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
            _agentClock = 240;
            _agentStatus = AgentState.Stunned;
        }

        protected override void Knockback()
        {
            // NO-OP: Unknockable
        }

        public void UpdateAction()
        {
            if (_actionCount == 0)
            {
                _agentStatus = AgentStateUtility.RandomFrom(ValidAgentStates);
                switch (_agentStatus)
                {
                    case AgentState.Moving:
                        _actionCount = 4;
                        break;
                    case AgentState.Halted:
                    case AgentState.Knocked:
                    case AgentState.Ready:
                        _actionCount = 1;
                        break;
                }
                
            }
            _actionCount--;

            if (_agentStatus == AgentState.Moving)
            {
                _currentDirection = IsWithinCircularBounds(_playerLocation, Location, 192)
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
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void Update()
        {
            if (Alive && CanMove)
                ExecuteAction();

            base.Update();
        }
    }
}
