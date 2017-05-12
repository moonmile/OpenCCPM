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

namespace Openccpm.WPF.Views
{
    /// <summary>
    /// TicketEdit.xaml の相互作用ロジック
    /// </summary>
    public partial class TicketEdit : UserControl
    {
        public TicketEdit()
        {
            InitializeComponent();
        }

        public event EventHandler OnSave;
        public event EventHandler OnCancel;

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            OnSave?.Invoke(sender, e);
        }

        /// <summary>
        /// キャンセルをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            OnCancel?.Invoke(sender, e);
        }
    }
}
