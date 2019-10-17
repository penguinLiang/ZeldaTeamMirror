using System;

namespace Zelda.Player
{
    /*
     * Manages the state of Link's health and hurt status
     */
    internal class HealthStateMachine : IUpdatable
    {
        public bool Hurt { get; private set; }
        public bool Alive => Health > 0;

        // Health is countable as half hearts, so 6 is 3 full hearts
        public int MaxHealth { get; private set; } = 6;
        public int Health;

        private readonly FrameDelay _hurtResetDelay = new FrameDelay(60);

        public HealthStateMachine()
        {
            Health = MaxHealth;
            _hurtResetDelay.Pause();
        }

        public void TakeDamage()
        {
            if (Alive)
            {
                Health--;
                Hurt = true;
                _hurtResetDelay.Resume();
            }
            else
            {
                Hurt = false;
            }
        }

        public void FullHeal()
        {
            Health = MaxHealth;
        }

        public void Heal()
        {
            Health = Math.Min(Health + 2, MaxHealth);
        }

        public void AddHeart()
        {
            MaxHealth += 2;
        }

        public void Update()
        {
            _hurtResetDelay.Update();

            if (_hurtResetDelay.Delayed) return;
            _hurtResetDelay.Pause();
            Hurt = false;
        }
    }
}
