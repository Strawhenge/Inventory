using Strawhenge.Inventory.Effects;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.EffectTests
{
    public class EffectsTests
    {
        const int InitialHealth = 10;
        const int HealthIncrease = 2;

        readonly HealthData _data;
        readonly Health _health;
        readonly IEffectFactory _factory;

        public EffectsTests(ITestOutputHelper testOutputHelper)
        {
            var logger = new TestOutputLogger(testOutputHelper);

            _data = new HealthData() { IncreaseAmount = HealthIncrease };
            _health = new Health() { Amount = InitialHealth };

            _factory = new EffectFactory(
                new AbstractFactoryFake(
                    new HealthFactory(_health)),
                logger);
        }

        [Fact]
        public void Effect_should_apply()
        {
            var effect = CreateEffect();

            effect.Apply();

            VerifyEffectIsApplied();
        }

        [Fact]
        public void Effect_should_revert()
        {
            var effect = CreateEffect();

            effect.Apply();
            effect.Revert();

            VerifyEffectIsNotApplied();
        }

        [Fact]
        public void Default_effect_should_return_when_corresponding_factory_not_implemented()
        {
            var effect = _factory.Create(
                EffectData.From(new NotImplementedEffectData()));

            Assert.IsType<NullEffect>(effect);
        }

        Effect CreateEffect() => _factory.Create(EffectData.From(_data));

        void VerifyEffectIsApplied() => Assert.Equal(InitialHealth + HealthIncrease, _health.Amount);

        void VerifyEffectIsNotApplied() => Assert.Equal(InitialHealth, _health.Amount);
    }
}