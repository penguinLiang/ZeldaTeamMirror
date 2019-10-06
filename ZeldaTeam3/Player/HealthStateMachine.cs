﻿namespace Zelda.Player
{
    internal class HealthStateMachine : ISpawnable
    {
        // 60/60 frames = ~1s
        private const int DeathFrameDelay = 60;
        // 120/60 frames = ~2s
        private const int RespawnFrameDelay = 120;
        private const int HurtResetDelay = 120;

        public bool Alive { get; private set; } = true;
        public bool Visible { get; private set; } = true;
        public bool Hurt { get; private set; }
        public int _health {get; private set; } = 3;

        private int _dyingFramesDelayed;
        private int _respawnFramesDelayed;
        private int _hurtFramesDelayed;
        //Link starts with 3 hearts. Float because some enemies hit for .5 of a heart
        //potential get/set?

        //With Enemies, if they call damage to link, should be able to pass in the damage value?

        

        private int DyingFramesDelayed
        {
            get => _dyingFramesDelayed;
            set
            {
                if (Alive) return;
                _dyingFramesDelayed = value;

                if (_dyingFramesDelayed != DeathFrameDelay) return;
                Visible = false;
                _dyingFramesDelayed = 0;
            }
        }

        private int RespawnFramesDelayed
        {
            get => _respawnFramesDelayed;
            set
            {
                if (Alive) return;
                _respawnFramesDelayed = value;

                if (_respawnFramesDelayed != RespawnFrameDelay) return;
                Alive = true;
                Visible = true;
                _respawnFramesDelayed = 0;
            }
        }

        private int HurtFramesDelayed
        {
            get => _hurtFramesDelayed;
            set
            {
                if (!Hurt) return;
                _hurtFramesDelayed = value;

                if (_hurtFramesDelayed != HurtResetDelay) return;
                Hurt = false;
                _hurtFramesDelayed = 0;
            }
        }

        public void Spawn()
        {
            Alive = true;
            Visible = true;
            _health = 3;
        }

        public void TakeDamage()
        {
            Hurt = true;
            //TODO: Add in HEALTH checks
            _health--;
            //Health = Health - Enemy Attack Value
            
            System.Diagnostics.Debug.WriteLine("HEALTH: " + _health);
            if(_health<1){
                Kill();
                }                         
            
        }

        public void Kill()
        {
            Hurt = false;
            Alive = false;
            //play death animation?
            System.Diagnostics.Debug.WriteLine("\n Oh no! ");
             Visible = false;
                _dyingFramesDelayed = 0;
            _hurtFramesDelayed = 0;

            
        }

        public void Update()
        {
            DyingFramesDelayed++;
            RespawnFramesDelayed++;
            HurtFramesDelayed++;
        }
    }
}
