using System;
using Zelda.Music;
using Zelda.SoundEffects;

namespace Zelda.Player
{
    /*
     * Manages the state of Link's health and hurt status
     */
    internal class HealthStateMachine : IUpdatable
    {
        private const int HealthBeepTimerReset = 20;
        private const int InvulnerabilityTime = 670;
        private const int PaletteShiftDuration = 100;

        public bool Hurt { get; private set; }
        public bool Invulnerable { get; private set; }
        public bool InvulnerabilityFading => InvulnerabilityTime - _invulnerabilityTimer < PaletteShiftDuration;
        public bool Alive => Health > 0;

        // Health is countable as half hearts, so 6 is 3 full hearts
        public int MaxHealth { get; private set; } = 6;
        public int Health;

        private readonly FrameDelay _hurtResetDelay = new FrameDelay(40, true);
        private int _healthBeepTimer;
        private int _invulnerabilityTimer;

        public HealthStateMachine()
        {
            Health = MaxHealth;
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

        public void MakeInvulnerable()
        {
            Invulnerable = true;
            _invulnerabilityTimer = 0;
            MusicManager.Instance.PlayStarMusic();
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
            
            if (Invulnerable && ++_invulnerabilityTimer >= InvulnerabilityTime)
            {
                Invulnerable = false;
                MusicManager.Instance.PlayLabryinthMusic();
            }

            _hurtResetDelay.Update();

            if (_hurtResetDelay.Delayed) return;
            _hurtResetDelay.Pause();
            Hurt = false;
        }
    }
}
