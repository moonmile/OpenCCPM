﻿using Openccpm.UWP.Controllers;
using Openccpm.Web.Models;
using Openccpm.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace Openccpm.UWP
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        string _url = "http://openccpm.azurewebsites.net";
        TicketDrivenService service;
        MainViewModel viewModel;
        TicketViewModel viewModelTicket;

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            service = new TicketDrivenService(_url);
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
            // プロジェクトのリストを取得
            viewModel.Projects = await service.Project.GetItemsAsync();


            ticketDetail.OnEdit += (s, ee) => {
                ticketDetail.Visibility = Visibility.Collapsed;
                ticketEdit.Visibility = Visibility.Visible;
            };
            ticketEdit.OnCancel += (s, ee) =>
            {
                ticketDetail.Visibility = Visibility.Visible;
                ticketEdit.Visibility = Visibility.Collapsed;
            };
            ticketEdit.OnSave += TicketEdit_OnSave;
            ticketDetail.OnNew += TicketDetail_OnNew;

        }
        /// <summary>
        /// プロジェクトの選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnProjectSelectionChanged(object sender, SelectionChangedEventArgs e)
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
            ticketEdit.Visibility = Visibility.Collapsed;
            // リストを更新する
            lvTickets.IsEnabled = true;
            int index = lvTickets.SelectedIndex;
            viewModel.Tickets = await service.GetTicketsAsync(viewModel.Project.ProjectNo);
            // カーソルを戻す
            lvTickets.SelectedIndex = index;

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

            ticketDetail.Visibility = Visibility.Collapsed;
            ticketEdit.Visibility = Visibility.Visible;
        }
    }
}
