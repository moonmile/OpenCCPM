using Openccpm.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Openccpm.WPF.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private List<Project> _Projects;
        public List<Project> Projects
        {
            get { return _Projects; }
            set
            {
                SetProperty(ref _Projects, value, "Projects");
            }
        }

        private Project _Project;
        public Project Project {
            get { return _Project; }
            set
            {
                SetProperty(ref _Project, value, "Project");
            }
        }


        private List<TicketView> _Tickets;
        public List<TicketView> Tickets
        {
            get { return _Tickets; }
            set
            {
                SetProperty(ref _Tickets, value, "Tickets");
            }
        }
        private TicketView _Ticket;
        public TicketView Ticket
        {
            get { return _Ticket; }
            set
            {
                SetProperty(ref _Ticket, value, "Ticket");
            }
        }
    }

    /// <summary>
    /// Observable object with INotifyPropertyChanged implemented
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        /// <param name="backingStore">Backing store.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="onChanged">On changed.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
