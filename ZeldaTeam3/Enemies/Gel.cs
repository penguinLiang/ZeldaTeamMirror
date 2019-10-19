using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Zelda.Enemies
{
    public class Gel : EnemyAgent
    {
        private ISprite _sprite;
        protected override ISprite Sprite => _sprite;
        public override Rectangle Bounds => new Rectangle(Location.X + 4, Location.Y + 7, 8, 9);
        private static readonly List<AgentState> ValidAgentStates = new List<AgentState>
        {
            AgentState.Ready,
            AgentState.Moving,
            AgentState.Halted
        };

        private readonly Point _origin;

        private Direction _currentDirection;
        private AgentState _agentStatus;

        private const int ActionDelay = 16;
        private int _agentClock;

        public Gel(Point location)
        {
            _origin = location;
            _agentStatus = AgentState.Ready;
        }

        public override void Spawn()
        {
            base.Spawn();

            _sprite = EnemySpriteFactory.Instance.CreateGel();
            Location = _origin;
            _currentDirection = Direction.Down;
        }

        private void FlipDirection()
        {
            _currentDirection = DirectionUtility.Flip(_currentDirection);
        }

        public void ExecuteAction()
        {
            if (_agentClock > 0)
            {
                _agentClock--;
            }

            switch (_agentStatus)
            {
                case AgentState.Ready: //determine next action
                    UpdateAction();
                    break;
                case AgentState.Halted:
                    if (_agentClock == 0)
                    {
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                case AgentState.Knocked:
                    if (_agentClock != 0)
                    {
                        Move(_currentDirection);
                    }
                    else
                    {
                        FlipDirection();
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                case AgentState.Moving:
                    if (_agentClock != 0)
                    {
                        Move(_currentDirection);
                    }
                    else
                    {
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                case AgentState.Attacking:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void UpdateAction()
        {
            _agentStatus = AgentStateUtility.RandomFrom(ValidAgentStates);
            if (_agentStatus == AgentState.Moving)
            {
                _currentDirection = DirectionUtility.RandomDirection();
            }
            _agentClock = ActionDelay;
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
            _agentClock = ActionDelay*3;
            FlipDirection();
            Move(_currentDirection);
        }

        public override void Stun()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            base.Update();

            if (Alive && CanMove)
                ExecuteAction();
        }
    }
}