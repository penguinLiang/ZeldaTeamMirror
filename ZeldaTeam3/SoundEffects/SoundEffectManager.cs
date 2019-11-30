using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Zelda.SoundEffects
{
    internal class SoundEffectManager
    {
        public static SoundEffectManager Instance { get; } = new SoundEffectManager();

        private SoundEffect _playSwordSlash;
        private SoundEffect _playSwordShoot;
        private SoundEffect _playDeflect;
        private SoundEffect _playArrowBoomerangShoot;
        private SoundEffect _playBombDrop;
        private SoundEffect _playBombExplode;
        private SoundEffect _playEnemyHit;
        private SoundEffect _playEnemyDie;
        private SoundEffect _playLinkHurt;
        private SoundEffect _playLowHealth;
        //For looping and stopping the sound
        private SoundEffect _playPickupItem;
        //Also used for Fairy sound effect
        private SoundEffect _playPickupNewItem;
        private SoundEffect _playPickupDoppedHeartKey;
        private SoundEffect _playPickupRupee;
        private SoundEffect _playKeyAppear;
        private SoundEffect _playDoorUnlock;
        private SoundEffect _playBossDead;
        private SoundEffect _playBossHurt;
        private SoundEffect _playPuzzleSolved;
        private SoundEffect _playLaserBeam;

        public void LoadAllSounds(ContentManager content)
        {
            _playArrowBoomerangShoot = content.Load<SoundEffect>("Sounds/LOZ_Arrow_Boomerang");
            _playBombDrop = content.Load<SoundEffect>("Sounds/LOZ_Bomb_Drop");
            _playBombExplode = content.Load<SoundEffect>("Sounds/LOZ_Bomb_Blow");
            _playBossHurt = content.Load<SoundEffect>("Sounds/LOZFDS_Boss_Hit");
            _playBossDead = content.Load<SoundEffect>("Sounds/LOZFDS_Boss_Scream1_Distant");
            _playDeflect = content.Load<SoundEffect>("Sounds/LOZ_Shield");
            _playDoorUnlock = content.Load<SoundEffect>("Sounds/LOZ_Door_Unlock");
            _playEnemyDie = content.Load<SoundEffect>("Sounds/LOZ_Enemy_Die");
            _playEnemyHit = content.Load<SoundEffect>("Sounds/LOZ_Enemy_Hit");
            _playKeyAppear = content.Load<SoundEffect>("Sounds/LOZ_Key_Appear");
            _playLaserBeam = content.Load<SoundEffect>("Sounds/LaserBeam");
            _playLinkHurt = content.Load<SoundEffect>("Sounds/LOZ_Link_Hurt");
            _playLowHealth = content.Load<SoundEffect>("Sounds/LOZ_LowHealth");
            _playPickupDoppedHeartKey = content.Load<SoundEffect>("Sounds/LOZ_Get_Heart");
            _playPickupItem = content.Load<SoundEffect>("Sounds/LOZ_Get_Item");
            _playPickupRupee = content.Load<SoundEffect>("Sounds/LOZ_Get_Rupee");
            _playPickupNewItem = content.Load<SoundEffect>("Sounds/LOZ_Fanfare");
            _playPuzzleSolved = content.Load<SoundEffect>("Sounds/LOZ_Secret");
            _playSwordShoot = content.Load<SoundEffect>("Sounds/LOZ_Sword_Shoot");
            _playSwordSlash = content.Load<SoundEffect>("Sounds/LOZ_Sword_Slash");
        }

        public void PlayArrowShoot()
        {
            _playArrowBoomerangShoot.CreateInstance().Play();
        }

        public SoundEffectInstance PlayBoomerang()
        {
            var arrowBoomerangInstance = _playArrowBoomerangShoot.CreateInstance();
            arrowBoomerangInstance.IsLooped = true;
            arrowBoomerangInstance.Play();
            return arrowBoomerangInstance;
        }

        public void PlayBombDrop()
        {
            _playBombDrop.CreateInstance().Play();
        }

        public void PlayBombExplode()
        {
            _playBombExplode.CreateInstance().Play();
        }

        public void PlayBossHurt()
        {
            _playBossHurt.CreateInstance().Play();
        }

        public void PlayBossDead()
        {
            _playBossDead.CreateInstance().Play();
        }

        public void PlayDeflect()
        {
            _playDeflect.CreateInstance().Play();
        }

        public void PlayDoorUnlock()
        {
            _playDoorUnlock.CreateInstance().Play();
        }

        public void PlayEnemyDie()
        {
            _playEnemyDie.CreateInstance().Play();
        }

        public void PlayEnemyHit()
        {
            _playEnemyHit.CreateInstance().Play();
        }

        public void PlayKeyAppear()
        {
            _playKeyAppear.CreateInstance().Play();
        }

        public void PlayLaserBeam()
        {
            _playLaserBeam.CreateInstance().Play();
        }

        public void PlayLinkHurt()
        {
            _playLinkHurt.CreateInstance().Play();
        }

        public void PlayLowHealth()
        {
            _playLowHealth.CreateInstance().Play();
        }

        public void PlayPickupDroppedHeartKey()
        {
            _playPickupDoppedHeartKey.CreateInstance().Play();
        }

        public void PlayPickupItem()
        {
            _playPickupItem.CreateInstance().Play();
        }

        public void PlayPickupRupee()
        {
            _playPickupRupee.CreateInstance().Play();
        }

        public void PlayPickupNewItem()
        {
            _playPickupNewItem.CreateInstance().Play();
        }

        public void PlayPuzzleSolved()
        {
            _playPuzzleSolved.CreateInstance().Play();
        }

        public void PlaySwordShoot()
        {
            _playSwordShoot.CreateInstance().Play();
        }

        public void PlaySwordSlash()
        {
            _playSwordSlash.CreateInstance().Play();
        }
    }
}


