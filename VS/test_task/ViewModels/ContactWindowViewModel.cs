using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using test_task.Classes;
using test_task.DB;

namespace test_task.ViewModels
{
    class ContactWindowViewModel : Screen
    {
        #region fields
        private Contact _contact;
        private bool _isEditingEnabled;
        private Visibility _showSaveButton;
        private Visibility _showEditButton;
        #endregion

        #region properties
        public Contact Contact
        {
            get => _contact;
            set 
            {
                _contact = value;
                NotifyOfPropertyChange(() => Contact);
            }
        }
        public bool IsEditingEnabled
        {
            get => _isEditingEnabled;
            set 
            {
                _isEditingEnabled = value;
                NotifyOfPropertyChange(() => IsEditingEnabled);
            }
        }
        public Visibility ShowSaveButton
        {
            get => _showSaveButton;
            set
            {
                _showSaveButton = value;
                NotifyOfPropertyChange(() => ShowSaveButton);
            }
        }
        public Visibility ShowEditButton
        {
            get => _showEditButton;
            set 
            {
                _showEditButton = value;
                NotifyOfPropertyChange(() => ShowEditButton);
            }
        }
        #endregion

        #region ctors
        public ContactWindowViewModel()
        {
            Contact = DBOperator.GetContact(ShellViewModel.ChoosenObject as Client);
            IsEditingEnabled = false;
            ShowEditButton = Visibility.Visible;
            ShowSaveButton = Visibility.Hidden;
        }
        #endregion

        #region events
        public void CloseButtonClick()
        {
            ClientWindowViewModel.ActiveViewModel.ActivateItem(null);
        }

        public void EditButtonClick()
        {
            IsEditingEnabled = true;
            ShowEditButton = Visibility.Hidden;
            ShowSaveButton = Visibility.Visible;
        }

        public void SaveButtonClick()
        {

            Contact newContact = Contact;
            DBOperator.InsertContact(Contact);
            IsEditingEnabled = false;
            ShowEditButton = Visibility.Visible;
            ShowSaveButton = Visibility.Hidden;
        }
        #endregion
    }
}
