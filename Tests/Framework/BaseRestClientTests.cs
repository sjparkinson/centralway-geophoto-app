using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.ApiClients;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class BaseRestClientTests : BaseRestClient
    {
        private BaseRestClientTests testClient;

        [TestInitialize]
        public void TestSetup()
        {
            this.testClient = new BaseRestClientTests();

            this.testClient.baseUrl = "http://example.com";
        }

        [TestMethod]
        public void GetRequestParameters_Test()
        {
            var testParameter = new RequestParameter("test", "parameter");

            testClient.AddRequestParameter(testParameter);

            var parameters = testClient.GetRequestParameters();

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(testParameter, parameters.First());

            testClient.AddRequestParameter(testParameter);

            Assert.AreEqual(2, parameters.Count);

            parameters.ForEach(p => Assert.AreEqual(testParameter, p));
        }

        [TestMethod]
        public void AddRequestParameter_KeyValueStrings_Test()
        {
            var testParameter = new RequestParameter("test", "parameter");

            testClient.AddRequestParameter(testParameter.Key, testParameter.Value);

            var parameters = testClient.GetRequestParameters();

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(testParameter, parameters.First());
        }

        [TestMethod]
        public void AddRequestParameter_RequestParameter_Test()
        {
            var testParameter = new RequestParameter("test", "parameter");

            testClient.AddRequestParameter(testParameter);

            var parameters = testClient.GetRequestParameters();

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(testParameter, parameters.First());
        }

        [TestMethod]
        public void GetUrlWithParameters_NoParameters_Test()
        {
            var testParameter = new RequestParameter("test", "parameter");

            testClient.AddRequestParameter(testParameter);
            testClient.AddRequestParameter(testParameter);

            string expectedUrl = testClient.baseUrl + "?test=parameter&test=parameter";

            Assert.AreEqual(expectedUrl, testClient.GetUrlWithParameters());
        }

        [TestMethod]
        public void GetUrlWithParameters_StringParameter_Test()
        {
            var testParameter = new RequestParameter("test", "parameter");

            testClient.AddRequestParameter(testParameter);

            string expectedUrl = testClient.baseUrl + "?test=parameter&test=parameter";

            Assert.AreEqual(expectedUrl, testClient.GetUrlWithParameters("test", "parameter"));
        }

        [TestMethod]
        public void GetUrlWithParameters_RequestParameters_Test()
        {
            var testParameter = new RequestParameter("test", "parameter");

            testClient.AddRequestParameter(testParameter);

            string expectedUrl = testClient.baseUrl + "?test=parameter&test=parameter";

            var parameters = new List<RequestParameter> { testParameter };

            Assert.AreEqual(expectedUrl, testClient.GetUrlWithParameters(parameters));
        }
    }
}
