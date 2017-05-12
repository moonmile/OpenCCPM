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
    /// TicketDetail.xaml の相互作用ロジック
    /// </summary>
    public partial class TicketDetail : UserControl
    {
        public TicketDetail()
        {
            InitializeComponent();
        }

        public event EventHandler OnEdit;
        public event EventHandler OnNew;
        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditClicked(object sender, RoutedEventArgs e)
        {
            OnEdit?.Invoke(sender, e);
        }

        private void OnNewClicked(object sender, RoutedEventArgs e)
        {
            OnNew?.Invoke(sender, e);
        }
    }
}
