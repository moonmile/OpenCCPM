using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Openccpm.Lib.Services;
using System.Threading.Tasks;

namespace Openccpm.Test
{
    /// <summary>
    /// 認証のテスト
    /// </summary>
    [TestClass]
    public class TestAuth
    {

        UserService service;

        /// <summary>
        /// 簡単なログインのみ
        /// </summary>
        [TestMethod]
        public void TestInit()
        {
            var cockie = new CookieContainer();
            var hc = new HttpClient(new HttpClientHandler { CookieContainer = cockie });
        }

        /*
        // ユーザーの作成
        public async Task TestAddUser()
        {
            service = new UserService("http://localhost:5000");
            // レジストルする
            bool result = await service.AddUserAsync("test@mail.com", "password", "test user");
            // ログインする
            var user = await service.LogInAsync("test@mail.com", "password");
            // 自分自身を削除する
            await service.RemoveUserAsync("test@mail.com", "password");
            // ログインに失敗する
            var user = await service.LogInAsync("test@mail.com", "password");


        }
        */
        // ユーザーの削除
        // ユーザーをロールに追加
        // ユーザーをロールから外す
        // ユーザーをプロジェクトに追加
        // ユーザーをプロジェクトから外す

    }
}
