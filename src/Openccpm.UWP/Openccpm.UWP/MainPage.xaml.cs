using Openccpm.UWP.Controllers;
using Openccpm.Web.Models;
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

        TaskItemService taskItems;
        TaskItem _item;


        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            taskItems = new TaskItemService("http://localhost:5000");
        }


        /// <summary>
        /// リストを取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetItemsClick(object sender, RoutedEventArgs e)
        {
            var lst = await taskItems.GetItems();
            foreach ( var it in lst )
            {
                Debug.WriteLine(it.Id + " " + it.Title);
            }
            if ( lst.Count > 0 )
            {
                textId.Text = lst[0].Id;
            }
        }

        /// <summary>
        /// 指定IDで取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetItemClick(object sender, RoutedEventArgs e)
        {
            var id = textId.Text;
            var item = await taskItems.GetItem(id);
            Debug.WriteLine(item);
            textNo.Text = item.TaskNo;
            textTitle.Text = item.Title;
            textDesc.Text = item.Desc;
            _item = item;
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddClick(object sender, RoutedEventArgs e)
        {
            var item = new TaskItem();
            item.TaskNo = textNo.Text;
            item.Title = textTitle.Text;
            item.Desc = textDesc.Text;
            item = await taskItems.AddItem(item);
            textId.Text = item.Id;
            _item = item;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            var item = _item;
            item.Id = textId.Text;
            item.TaskNo = textNo.Text;
            item.Title = textTitle.Text;
            item.Desc = textDesc.Text;
            taskItems.UpdateItem(item);
        }
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var id  = textId.Text;
            taskItems.DeleteItem(id);
        }


        /// <summary>
        /// タスクIDを指定して開始/終了のリストを取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetStartEndClick(object sender, RoutedEventArgs e)
        {
            var id = textId.Text;
            var lst = await taskItems.GetStartEnd( id );
            foreach (var it in lst)
            {
                Debug.WriteLine(it.Id + " " + it.StartAt + " " + it.EndAt );
            }
        }

        /// <summary>
        /// タスクIDを指定して開始/終了のリストを追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddStartEndClick(object sender, RoutedEventArgs e)
        {
            var id = textId.Text;
            var isplan = chkPlan.IsChecked.Value;
            var start = DateTime.Parse(textStartTime.Text);
            var end = DateTime.Parse(textEndTime.Text);
            var item = await taskItems.AddStartEnd(id, start, end, isplan );
        }
    }
}
