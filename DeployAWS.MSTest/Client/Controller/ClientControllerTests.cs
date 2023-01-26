using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeployAWS.MSTest.Client.Controller
{
    [TestClass]
    public class ClientControllerTests
    {
        [TestMethod]
        public void Test_String_FluentAssertions_ok()
        {
            string userName = "Fernando";
            userName.Should().StartWith("F").And.Contain("rnan").And.Be("Fernando");
        }
    }
}
