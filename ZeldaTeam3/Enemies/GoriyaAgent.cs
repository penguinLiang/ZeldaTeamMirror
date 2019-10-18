using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    internal class GoriyaAgent
    {
        private ISprite _sprite;

        internal Projectiles.GoriyaBoomerang Boomerang;

        public bool Alive { get; private set; }
        private bool _isDying;
        private bool _isImmobile;
        private bool _updateSpriteFlag;

        public Point Location;

        private Direction _statusDirection;

        private int _health;
        private int _delayClock;
        private int _agentClock;
        private int _timeSinceBoomerangThrown;
        private const int BoomerangDuration = 40;
        private const int ActionDelay = 30;
        private const int Velocity = 1;
        private AgentState _agentStatus;
        private static Random rng = new Random();

        public GoriyaAgent(Point location)
        {
            _updateSpriteFlag = false;
            Location = location;
            _timeSinceBoomerangThrown = BoomerangDuration;
            Alive = false;
            _statusDirection = Direction.Down;
            _health = 0;
            _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceDown();
            _sprite.Hide();

            _agentStatus = AgentState.Ready;
        }

        public void Kill()
        {
            if (!Alive)
            {
                return;
            }
            _sprite.Hide();
            _sprite = EnemySpriteFactory.Instance.CreateDeathSparkle();
            _agentClock = 0;
            _agentStatus = AgentState.Ready;
            _delayClock = 32;
            _isDying = true;
            Alive = false;
        }

        public void UseAttack()
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

            Boomerang = new Projectiles.GoriyaBoomerang(Location + boomerangOffset, _statusDirection);
            _timeSinceBoomerangThrown = 0;
        }

        private void MoveDown()
        {
            Location.Y += Velocity;
        }

        private void MoveLeft()
        {
            Location.X -= Velocity;
        }

        private void MoveRight()
        {
            Location.X += Velocity;
        }

        private void MoveUp()
        {
            Location.Y -= Velocity;
        }

        public void Move(Direction direction)
        {
            if (_timeSinceBoomerangThrown <= BoomerangDuration || _isImmobile)
            {
                return;
            }

            UpdateDirection(direction);
            switch (direction)
            {
                case Direction.Up:
                    MoveUp();
                    return;
                case Direction.Left:
                    MoveLeft();
                    return;
                case Direction.Right:
                    MoveRight();
                    return;
                case Direction.Down:
                    MoveDown();
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        public void Spawn()
        {
            _sprite = EnemySpriteFactory.Instance.CreateSpawnExplosion();
            _isImmobile = true;
            Alive = true;
            _delayClock = 30;
            _health = 3;
            UpdateDirection(Direction.Down);
        }

        public void TakeDamage()
        {
            if (Alive)
            {
                _health--;
                _sprite.PaletteShift();
            }

            if (_health < 1)
            {
                Kill();
            }
        }

        public void Draw()
        {
            _sprite.Draw(Location.ToVector2());
            Boomerang?.Draw();
        }

        public void Update()
        {
            _timeSinceBoomerangThrown++;

            if (_delayClock > 0)
            {
                _delayClock--;
                if (_delayClock == 0)
                {
                    CheckFlags();
                }
            }
            else
            {
                if (Alive)
                    ExecuteAction();
            }
            
            if (_updateSpriteFlag)
            {
                UpdateSprite();
            }

            _sprite.Update();
            Boomerang?.Update();
        }
        public void Halt()
        {
            _agentStatus = AgentState.Halted;
            _agentClock = ActionDelay;
            FlipDirection();
            Move(_statusDirection);
        }

        public void ExecuteAction()
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
                case AgentState.Attacking: // Intentional case overflow. Reduces code redundancy.
                case AgentState.Halted:
                    if (_agentClock == 0)
                    {
                        _agentStatus = AgentState.Ready;
                    }
                    break;
                case AgentState.Knocked:
                    if (_agentClock != 0)
                    {
                        Move(_statusDirection);
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
                        Move(_statusDirection);
                    }
                    else
                    {
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void UpdateAction()
        {
            _agentStatus = (AgentState)(rng.Next(4));
            switch (_agentStatus)
            {
                case AgentState.Moving:
                    UpdateDirection((Direction)(rng.Next(4)));
                    _agentClock = ActionDelay;
                    break;
                case AgentState.Halted:
                    _agentClock = 2 * ActionDelay;
                    break;
                case AgentState.Attacking:
                    UseAttack();
                    _agentClock = BoomerangDuration;
                    break;
                default:
                    _agentClock = ActionDelay;
                    break;
            }
        }

        public void UpdateDirection(Direction direction)
        {
            _updateSpriteFlag = _statusDirection != direction;
            _statusDirection = direction;
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

            if (!Alive)
            {
                _sprite.Hide();
            }

            _updateSpriteFlag = false;
        }

        private void CheckFlags()
        {
            if (_isImmobile)
            {
                _updateSpriteFlag = true;
                _isImmobile = false;
            }

            if (_isDying)
            {
                _sprite = EnemySpriteFactory.Instance.CreateStalfos();
                _sprite.Hide();
                _isDying = false;
            }
        }

        private void FlipDirection()
        {
            switch (_statusDirection)
            {
                case Direction.Up:
                    UpdateDirection(Direction.Down);
                    break;
                case Direction.Down:
                    UpdateDirection(Direction.Up);
                    break;
                case Direction.Left:
                    UpdateDirection(Direction.Right);
                    break;
                case Direction.Right:
                    UpdateDirection(Direction.Left);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
