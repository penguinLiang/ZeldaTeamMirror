using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    internal class Goriya : EnemyAgent
    {
        private const int BoomerangDuration = 40;
        private const int ActionDelay = 30;

        private static readonly Point Size = new Point(16, 16);
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

        private bool _updateSpriteFlag;
        private Direction _statusDirection;

        private int _agentClock;
        private int _timeSinceBoomerangThrown;
        private AgentState _agentStatus;

        public Goriya(Point location)
        {
            _origin = location;
        }

        public override void Spawn()
        {
            base.Spawn();
            _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceDown();
            Location = _origin;
            Health = 3;

            _agentStatus = AgentState.Ready;
            _timeSinceBoomerangThrown = BoomerangDuration;
            _statusDirection = Direction.Down;
            _updateSpriteFlag = false;

            UpdateDirection(Direction.Down);
        }

        private void UpdateDirection(Direction direction)
        {
            _updateSpriteFlag = _statusDirection != direction;
            _statusDirection = direction;
        }
        private void FlipDirection()
        {
            UpdateDirection(DirectionUtility.Flip(_statusDirection));
        }

        private void UseAttack()
        {
            var boomerangOffset = new Point(4, 4);
            switch (_statusDirection)
            {
                case Direction.Up:
                    boomerangOffset.Y -= 16;
                    break;
                case Direction.Down:
                    boomerangOffset.Y += 16;
                    break;
                case Direction.Left:
                    boomerangOffset.X -= 16;
                    break;
                case Direction.Right:
                    boomerangOffset.X += 16;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Projectiles.Add(new Projectiles.GoriyaBoomerang(Location + boomerangOffset, _statusDirection));
            _timeSinceBoomerangThrown = 0;
        }

        protected override void Move(Direction direction)
        {
            if (_timeSinceBoomerangThrown <= BoomerangDuration || !CanMove) return;

            UpdateDirection(direction);
            base.Move(direction);
        }

        public override void Stun()
        {
            throw new NotImplementedException();
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
            Move(_statusDirection);
        }

        public void ExecuteAction()
        {
            if (_agentClock > 0) _agentClock--;

            switch (_agentStatus)
            {
                case AgentState.Ready:
                    _updateSpriteFlag = true;
                    UpdateAction();
                    break;
                case AgentState.Attacking:
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
                        Move(_statusDirection);
                    }
                    break;
                case AgentState.Moving:
                    if (_agentClock == 0)
                    {
                        _agentStatus = AgentState.Ready;
                    }
                    else
                    {
                        Move(_statusDirection);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void UpdateAction()
        {
            _agentStatus = AgentStateUtility.RandomFrom(ValidAgentStates);
            switch (_agentStatus)
            {
                case AgentState.Moving:
                    UpdateDirection(DirectionUtility.RandomDirection());
                    _agentClock = ActionDelay;
                    break;
                case AgentState.Halted:
                    _agentClock = 2 * ActionDelay;
                    break;
                case AgentState.Attacking:
                    UseAttack();
                    _agentClock = BoomerangDuration;
                    break;
                case AgentState.Ready:
                    break;
                case AgentState.Knocked:
                    break;
                default:
                    _agentClock = ActionDelay;
                    break;
            }
        }

        private void UpdateSprite()
        {
            switch (_statusDirection)
            {
                case Direction.Down:
                    _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceDown();
                    break;
                case Direction.Left:
                    _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceLeft();
                    break;
                case Direction.Up:
                    _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceUp();
                    break;
                case Direction.Right:
                    _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _updateSpriteFlag = false;
        }

        public override void Update()
        {
            _timeSinceBoomerangThrown++;

            if (Alive && CanMove) ExecuteAction();

            if (_updateSpriteFlag) UpdateSprite();

            base.Update();
        }
    }
}
