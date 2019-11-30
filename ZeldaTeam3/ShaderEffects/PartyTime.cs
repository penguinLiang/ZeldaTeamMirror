using Microsoft.Xna.Framework.Graphics;

namespace Zelda.ShaderEffects
{
    internal class PartyTime : IUpdatable
    {
        private const float BaseVelocity = 0.005f;
        private const float CycleMax = 0.35f;
        private const float CycleMin = -0.34f;
        private float _cycle = CycleMin;
        private float _cycleVelocity = BaseVelocity;

        public static Effect ShaderEffect { set; private get; }

        public PartyTime()
        {
            ShaderEffect.Parameters["InSaturationOffset"].SetValue(CycleMin);
        }

        public void Apply()
        {
            ShaderEffect.Techniques[0].Passes[0].Apply();
        }

        public void Update()
        {
            if (_cycleVelocity > 0.0f && _cycle >= CycleMax)
            {
                _cycleVelocity = -BaseVelocity;
            }
            else if (_cycleVelocity < 0.0f && _cycle <= CycleMin)
            {
                _cycleVelocity = BaseVelocity;
            }

            _cycle += _cycleVelocity;
            ShaderEffect.Parameters["InSaturationOffset"].SetValue(_cycle);
        }
    }
}
