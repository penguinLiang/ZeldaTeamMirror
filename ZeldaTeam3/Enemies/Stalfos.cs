﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Stalfos : EnemyAgent
    {
        private const int ActionDelay = 16;

        public override Rectangle Bounds => new Rectangle(Location.X, Location.Y, 16, 16);
        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateStalfos();
        private static readonly List<AgentState> ValidAgentStates = new List<AgentState>
        {
            AgentState.Ready,
            AgentState.Moving,
            AgentState.Halted
        };

        private int _agentClock;
        private Direction _currentDirection;
        private AgentState _agentStatus;

        public Stalfos(Point location)
        {
            Location = location;
            Health = 2;
        }

        public override void Spawn()
        {
            base.Spawn();
            _currentDirection = Direction.Down;
        }

        private void FlipDirection()
        {
            _currentDirection = DirectionUtility.Flip(_currentDirection);
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

        public void UpdateAction()
        {
            _agentStatus = AgentStateUtility.RandomFrom(ValidAgentStates);
            if (_agentStatus == AgentState.Moving)
            {
                _currentDirection = DirectionUtility.RandomDirection();
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
