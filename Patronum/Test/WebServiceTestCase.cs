
namespace Patronum.Test
{
    public abstract class WebServiceTestCase
    {
        public virtual WebServiceUnderTest WebService { get; set; }

        public string LastErrorText { get; set; }

        public virtual void FunctionalTestInitialize()
        {
            WebService = new WebServiceUnderTest();
        }
    }
}
