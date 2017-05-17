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
    public partial class ProjectListPage : ContentPage
    {
        public ProjectListPage()
        {
            InitializeComponent();
            vm = new ProjectListViewModel();
            vm.Items = new ObservableCollection<Project>();
            this.BindingContext = vm;
            service = MainPage.service;

            RefreshItemsFromTableAsync();
        }

        ProjectListViewModel vm;

        /// <summary>
        /// テーブルの更新
        /// </summary>
        /// <returns></returns>
        private async Task RefreshItemsFromTableAsync()
        {
            var items = await service.Project.GetItemsAsync();
            vm.Items.Clear();
            foreach ( var it in items )
            {
                vm.Items.Add(it);
            }
            return;
        }
        public TicketDrivenService service;

        /// <summary>
        /// 項目を選択したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Project;
            if (item == null)
                return;
            await Navigation.PushAsync(new TicketListPage(item));
        }


        /// <summary>
        /// 項目を選択したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnItemSelectedProject(object sender, SelectedItemChangedEventArgs args)
        {
            await Navigation.PushAsync(new ProjectDetailPage());
        }
    }
}
