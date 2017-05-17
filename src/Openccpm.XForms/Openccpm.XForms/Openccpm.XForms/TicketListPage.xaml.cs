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
    public partial class TicketListPage : ContentPage
    {
        public TicketListPage()
        {
            InitializeComponent();
        }

        public TicketListPage( Project project )
        {
            InitializeComponent();
            vm = new TicketListViewModel();
            vm.Project = project;
            vm.Title = vm.Project.Name;
            vm.Items = new ObservableCollection<TicketView>();
            this.BindingContext = vm;
            service = MainPage.service;
            RefreshItemsFromTableAsync();
        }


        TicketDrivenService service;
        TicketListViewModel vm;

        /// <summary>
        /// テーブルの更新
        /// </summary>
        /// <returns></returns>
        private async Task RefreshItemsFromTableAsync()
        {
            var items = await service.Ticket.GetItemsAsync( vm.Project.Id );
            vm.Items.Clear();
            foreach (var it in items)
            {
                vm.Items.Add(it);
            }
            return;
        }

        /// <summary>
        /// 項目を選択したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as TicketView;
            if (item == null)
                return;
            await Navigation.PushAsync(new TicketDetailPage(item));
        }

    }
}
