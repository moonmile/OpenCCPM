using Openccpm.Lib.Models;
using Openccpm.Lib.Services;
using Openccpm.XForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Openccpm.XForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicketDetailPage : ContentPage
    {
        public TicketDetailPage()
        {
            InitializeComponent();
        }

        public TicketDetailPage(TicketView ticket)
        {
            InitializeComponent();
            vm = new TicketDetailViewModel();
            vm.Title = ticket.TaskNo;
            vm.Ticket = ticket;
            this.BindingContext = vm;
            service = MainPage.service;
            RefreshItemsFromTableAsync();
        }


        TicketDrivenService service;
        TicketDetailViewModel vm;

        /// <summary>
        /// テーブルの更新
        /// </summary>
        /// <returns></returns>
        private async Task RefreshItemsFromTableAsync()
        {
            // チケットの詳細を再取得する
            var id = vm.Ticket.Id;
            var item = await service.GetTicketAsync(id);
            vm.Ticket = item;
            return;
        }
    }
}
