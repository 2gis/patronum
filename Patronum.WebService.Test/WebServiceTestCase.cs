using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patronum.WebService.Test
{
    [TestClass]
    public abstract class WebServiceTestCase
    {
        public virtual WebServiceUnderTest WebService { get; set; }

        public string LastErrorText { get; set; }

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public virtual void FunctionalTestInitialize()
        {
            WebService = new WebServiceUnderTest();
        }
    }
}
