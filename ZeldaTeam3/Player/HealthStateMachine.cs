using System;
using Zelda.SoundEffects;

namespace Zelda.Player
{
    /*
     * Manages the state of Link's health and hurt status
     */
    internal class HealthStateMachine : IUpdatable
    {
        private const int HealthBeepTimerReset = 20;

        public bool Hurt { get; private set; }
        public bool Alive => Health > 0;

        // Health is countable as half hearts, so 6 is 3 full hearts
        public int MaxHealth { get; private set; } = 6;
        public int Health;

        private readonly FrameDelay _hurtResetDelay = new FrameDelay(60);
        private int _healthBeepTimer;

        public HealthStateMachine()
        {
            Health = MaxHealth;
            _hurtResetDelay.Pause();
        }

        public void TakeDamage(int damage)
        {
            if (Alive)
            {
                Health -= damage;
                Hurt = true;
                _hurtResetDelay.Resume();
                if (Health > 0)
                    SoundEffectManager.Instance.PlayLinkHurt();
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
            Heal();
        }

        public void Update()
        {
            if (Health > 2)
            {
                _healthBeepTimer = HealthBeepTimerReset;
            }
            else if (++_healthBeepTimer >= HealthBeepTimerReset)
            {
                _healthBeepTimer = 0;
                SoundEffectManager.Instance.PlayLowHealth();
            }

            _hurtResetDelay.Update();

            if (_hurtResetDelay.Delayed) return;
            _hurtResetDelay.Pause();
            Hurt = false;
        }
    }
}
