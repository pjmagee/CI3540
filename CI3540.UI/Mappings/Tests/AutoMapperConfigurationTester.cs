using AutoMapper;
using CI3540.UI.App_Start;
using NUnit.Framework;

namespace CI3540.UI.Mappings.Tests
{
    [TestFixture]
    public class AutoMapperConfigurationTester
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            AutoMapperConfig.RegisterConfig();
        }

        [Test]
        public void ShouldMapEverything()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}



