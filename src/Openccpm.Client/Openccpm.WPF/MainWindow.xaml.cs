using Openccpm.UWP.Controllers;
using Openccpm.Web.Models;
using Openccpm.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Openccpm.WPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }
#if DEBUG
        string _url = "http://localhost:5000";
#else
        string _url = "http://openccpm.azurewebsites.net";
#endif
        TicketDrivenService service;
        MainViewModel viewModel;
        TicketViewModel viewModelTicket;

        /// <summary>
        /// アプリケーションロード時の初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            service = new TicketDrivenService(_url);
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
            // プロジェクトのリストを取得
            viewModel.Projects = await service.Project.GetItemsAsync();


            ticketDetail.OnEdit += (s, ee) => {
                ticketDetail.Visibility = Visibility.Hidden;
                ticketEdit.Visibility = Visibility.Visible;
            };
            ticketEdit.OnCancel += (s, ee) =>
            {
                ticketDetail.Visibility = Visibility.Visible;
                ticketEdit.Visibility = Visibility.Hidden;
            };
            ticketEdit.OnSave += TicketEdit_OnSave;
            ticketDetail.OnNew += TicketDetail_OnNew;
        }

        /// <summary>
        /// プロジェクトの選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnProjectSelectionChanged(object sender, RoutedEventArgs e)
        {
            viewModel.Tickets = await service.GetTicketsAsync(viewModel.Project.ProjectNo);

            // TicketDetail と TicketView にバインドし直し
            viewModelTicket = new TicketViewModel();
            await service.Initalize(viewModel.Project.ProjectNo);
            viewModelTicket.Trackers = service.ListTracker;
            viewModelTicket.Statuses = service.ListStatus;
            viewModelTicket.Priorities = service.ListPriority;
            viewModelTicket.Users = service.ListAssignTo;
            this.ticketDetail.DataContext = viewModelTicket;
            this.ticketEdit.DataContext = viewModelTicket;
        }

        /// <summary>
        /// チケットの選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnTicketSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel.Ticket != null)
            {
                // 詳細情報を再取得
                viewModelTicket.Ticket = viewModel.Ticket = await service.GetTicketAsync(viewModel.Ticket.Id);
            }
        }

        /// <summary>
        /// チケットの保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TicketEdit_OnSave(object sender, EventArgs e)
        {
            if (viewModelTicket.Ticket.Id == null)
            {
                await service.AddTicketAsync(viewModelTicket.Ticket);
            }
            else
            {
                await service.UpdateTicketAsync(viewModelTicket.Ticket);
            }
            ticketDetail.Visibility = Visibility.Visible;
            ticketEdit.Visibility = Visibility.Hidden;
            // リストを更新する
            lvTickets.IsEnabled = true;
            viewModel.Tickets = await service.GetTicketsAsync(viewModel.Project.ProjectNo);

        }

        /// <summary>
        /// チケットを新規作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TicketDetail_OnNew(object sender, EventArgs e)
        {
            var ticket = new TicketView();
            ticket.Project = viewModel.Project;
            ticket.ProjectId = viewModel.Project.Id;
            ticket.Project_ProjectNo = viewModel.Project.ProjectNo;
            viewModelTicket.Ticket = ticket;

            ticketDetail.Visibility = Visibility.Hidden;
            ticketEdit.Visibility = Visibility.Visible;
        }
    }
}
