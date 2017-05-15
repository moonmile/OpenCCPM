using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;

namespace Openccpm.Test
{
    /// <summary>
    /// 認証のテスト
    /// </summary>
    [TestClass]
    public class TestAuth
    {
        /// <summary>
        /// 簡単なログインのみ
        /// </summary>
        [TestMethod]
        public void TestInit()
        {
            var cockie = new CookieContainer();
            var hc = new HttpClient(new HttpClientHandler { CookieContainer = cockie });
        }
    }
}
