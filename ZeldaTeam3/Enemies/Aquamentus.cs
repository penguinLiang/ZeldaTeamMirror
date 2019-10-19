﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Aquamentus : EnemyAgent
    {
        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateAquamentusIdle();
        public override Rectangle Bounds => new Rectangle(Location, new Point(24, 32));
        private static readonly List<AgentState> ValidAgentStates = new List<AgentState>
        {
            AgentState.Ready,
            AgentState.Moving,
            AgentState.Halted
        };

        private const int ActionDelay = 16;
        private int _agentClock;

        private Direction _currentDirection;
        private AgentState _agentStatus;

        public Aquamentus(Point location)
        {
            Location = location;
            Health = 2;

            _agentStatus = AgentState.Ready;
        }

        public override void Spawn()
        {
            base.Spawn();
            _currentDirection = Direction.Down;
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

        private void UpdateAction()
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

        public override void Update()
        {
            base.Update();

            if (Alive && CanMove)
                ExecuteAction();
        }
    }
}
