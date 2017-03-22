using System.Collections.Specialized;
using System.Web;
using RxLocal.Tests;
using NUnit.Framework;
using RxLocal.Core.Fakes;

namespace RxLocal.Core.Tests
{
    [TestFixture]
    public class WebHelperTests
    {
        private HttpContextBase _httpContext;
        private IWebHelper _webHelper;

        [Test]
        public void CanGetServerVariables()
        {
            var serverVariables = new NameValueCollection();
            serverVariables.Add("Key1", "Value1");
            serverVariables.Add("Key2", "Value2");

            _httpContext = new FakeHttpContext("~/", "GET", null, null, null, null, null, serverVariables);

            _webHelper = new WebHelper(_httpContext);
            _webHelper.ServerVariables("Key1").ShouldEqual("Value1");
            _webHelper.ServerVariables("Key2").ShouldEqual("Value2");
            _webHelper.ServerVariables("Key3").ShouldEqual("");
        }

        [Test]
        public void CanGetQueryString()
        {
            var queryStringParams = new NameValueCollection();
            queryStringParams.Add("Key1", "Value1");
            queryStringParams.Add("Key2", "Value2");

            _httpContext = new FakeHttpContext("~/", "GET", null, null, queryStringParams, null, null, null);

            _webHelper = new WebHelper(_httpContext);
            _webHelper.QueryString<string>("Key1").ShouldEqual("Value1");
            _webHelper.QueryString<string>("Key2").ShouldEqual("Value2");
            _webHelper.QueryString<string>("Key3").ShouldEqual(null);
        }

        [Test]
        public void CanRemoveQueryString()
        {
            _httpContext = new FakeHttpContext("~/", "GET", null, null, null, null, null, null);
            _webHelper = new WebHelper(_httpContext);
            //first param (?)
            _webHelper.RemoveQueryString("http://www.example.com/?param1=value1&param2=value2", "param1")
                .ShouldEqual("http://www.example.com/?param2=value2");
            //second param (&)
            _webHelper.RemoveQueryString("http://www.example.com/?param1=value1&param2=value2", "param2")
                .ShouldEqual("http://www.example.com/?param1=value1");
            //non-existing param
            _webHelper.RemoveQueryString("http://www.example.com/?param1=value1&param2=value2", "param3")
                .ShouldEqual("http://www.example.com/?param1=value1&param2=value2");
        }

        [Test]
        public void CanRemoveQueryString_ShouldReturnLowerCasedResult()
        {
            _httpContext = new FakeHttpContext("~/", "GET", null, null, null, null, null, null);
            _webHelper = new WebHelper(_httpContext);
            _webHelper.RemoveQueryString("htTp://www.eXAmple.com/?param1=value1&parAm2=value2", "paRAm1")
                .ShouldEqual("http://www.example.com/?param2=value2");
        }

        [Test]
        public void CanRemoveQueryString_ShouldIgnoreInputParameterCase()
        {
            _httpContext = new FakeHttpContext("~/", "GET", null, null, null, null, null, null);
            _webHelper = new WebHelper(_httpContext);
            _webHelper.RemoveQueryString("http://www.example.com/?param1=value1&parAm2=value2", "paRAm1")
                .ShouldEqual("http://www.example.com/?param2=value2");
        }

        [Test]
        public void CanModifyQueryString()
        {
            _httpContext = new FakeHttpContext("~/", "GET", null, null, null, null, null, null);
            _webHelper = new WebHelper(_httpContext);
            //first param (?)
            _webHelper.ModifyQueryString("http://www.example.com/?param1=value1&param2=value2", "param1=value3", null)
                .ShouldEqual("http://www.example.com/?param1=value3&param2=value2");
            //second param (&)
            _webHelper.ModifyQueryString("http://www.example.com/?param1=value1&param2=value2", "param2=value3", null)
                .ShouldEqual("http://www.example.com/?param1=value1&param2=value3");
            //non-existing param
            _webHelper.ModifyQueryString("http://www.example.com/?param1=value1&param2=value2", "param3=value3", null)
                .ShouldEqual("http://www.example.com/?param1=value1&param2=value2&param3=value3");
        }

        [Test]
        public void CanModifyQueryStringWithAnchor()
        {
            _httpContext = new FakeHttpContext("~/", "GET", null, null, null, null, null, null);
            _webHelper = new WebHelper(_httpContext);
            _webHelper.ModifyQueryString("http://www.example.com/?param1=value1&param2=value2", "param1=value3", "Test")
                .ShouldEqual("http://www.example.com/?param1=value3&param2=value2#test");
        }

        [Test]
        public void CanModifyQueryString_NewAnchorShouldRemovePreviousOne()
        {
            _httpContext = new FakeHttpContext("~/", "GET", null, null, null, null, null, null);
            _webHelper = new WebHelper(_httpContext);
            _webHelper.ModifyQueryString("http://www.example.com/?param1=value1&param2=value2#test1", "param1=value3", "Test2")
                .ShouldEqual("http://www.example.com/?param1=value3&param2=value2#test2");
        }
    }
}
