using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_task.Classes;
using test_task.DB;

namespace test_task.ViewModels
{
    class ContactWindowViewModel : Screen
    {
        #region properties
        private Contact _contact;

        public Contact Contact
        {
            get => _contact;
            set 
            {
                _contact = value;
                NotifyOfPropertyChange(() => Contact);
            }
        }

        #endregion

        #region ctors
        public ContactWindowViewModel()
        {
            Contact = DBOperator.GetContact(ShellViewModel.ChoosenObject as Client);
        }
        #endregion

        #region events
        public void CloseButtonClick()
        {
            ClientWindowViewModel.ActiveViewModel.ActivateItem(null);
        }
        #endregion
    }
}
