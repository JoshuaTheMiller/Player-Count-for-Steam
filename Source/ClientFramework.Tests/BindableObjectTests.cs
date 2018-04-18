using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trfc.ClientFramework;

namespace ClientFramework.Tests
{
    [TestClass]
    public class BindableObjectTests
    {
        [TestMethod]
        public void SetFieldCanSetNullField()
        {
            var bindableObject = new BindableObject();
            string nullField = null;

            bindableObject.SetField(ref nullField, "", "someName");

            //Success
        }
    }
}
