using Openccpm.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openccpm.WPF.ViewModels
{
    public class TicketViewModel : ObservableObject
    {
        private TicketView _Ticket;
        public TicketView Ticket
        {
            get { return _Ticket; }
            set
            {
                SetProperty(ref _Ticket, value, nameof(Ticket));
            }
        }

        private List<Tracker> _Trackers;
        public List<Tracker> Trackers
        {
            get { return _Trackers; }
            set
            {
                SetProperty(ref _Trackers, value, nameof(Trackers));
            }
        }

        private List<Status> _Statuses;
        public List<Status> Statuses
        {
            get { return _Statuses; }
            set
            {
                SetProperty(ref _Statuses, value,  nameof(Statuses));
            }
        }

        private List<Priority> _Priorities;
        public List<Priority> Priorities
        {
            get { return _Priorities; }
            set
            {
                SetProperty(ref _Priorities, value, nameof(Properties));
            }
        }

        private List<User> _Users;
        public List<User> Users
        {
            get { return _Users; }
            set
            {
                SetProperty(ref _Users, value, nameof(Users));
            }
        }
    }
}
