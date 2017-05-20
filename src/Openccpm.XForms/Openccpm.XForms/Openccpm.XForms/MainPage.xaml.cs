using Openccpm.Lib.Models;
using Openccpm.Lib.Services;
using Openccpm.XForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Openccpm.XForms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            vm = new MainPageViewModel();
            this.BindingContext = vm;

            service = new TicketDrivenService(_url);
            vm.Login = new LoginParameter() { LoginId = "tomoaki@mail.com", Password =  "masuda" };
        }

        MainPageViewModel vm;
        public static TicketDrivenService service;
        // string _url = "http://openccpm.azurewebsites.net";
        string _url = "http://localhost:5000";


        /// <summary>
        /// ログインボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Login_Clicked(object sender, EventArgs e)
        {
            // ログインする
            var result = await service.LogInAsync(vm.Login.LoginId, vm.Login.Password);
            if (result == false)
                return;

            // ログインに成功したらプロジェクト一覧へ
            await Navigation.PushAsync(new ProjectListPage());
        }
    }
}
