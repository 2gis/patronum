using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patronum.WebService.Test
{
    [TestClass]
    public abstract class WebServiceTestCase
    {
        public virtual WebServiceUnderTest WebServiceUnderTest { get; set; }

        public string LastErrorText { get; set; }

        public TestContext TestContext { get; set; }


        [TestInitialize]
        public virtual void FunctionalTestInitialize()
        {
            WebServiceUnderTest = new WebServiceUnderTest();
        }
    }
}
