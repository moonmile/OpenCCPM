﻿using System;
using System.Collections.Generic;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Openccpm.UWP.Views
{
    public sealed partial class TicketEdit : UserControl
    {
        public TicketEdit()
        {
            this.InitializeComponent();
        }

        public event EventHandler OnSave;
        public event EventHandler OnCancel;

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            OnSave?.Invoke(sender, new EventArgs());
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            OnCancel?.Invoke(sender, new EventArgs());
        }
    }
}
