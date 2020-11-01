using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using test_task.Classes;
using test_task.DB;

namespace test_task.ViewModels
{
    public class ClientByManagerViewModel : Caliburn.Micro.Screen
    {
        #region fields
        private BindableCollection<string> _clientsLis;
        private Manager _selectedManager;
        private bool _isRemoveClientEnabled;
        private int _lBSelectedIndex;
        private List<Client> clients;
        #endregion

        #region properties
        public BindableCollection<string> ClientsNamesList
        {
            get => _clientsLis;
            set
            {
                _clientsLis = value;
                NotifyOfPropertyChange(() => ClientsNamesList);
            }
        }
        public Manager SelectedManager
        {
            get { return _selectedManager; }
            set 
            {
                _selectedManager = value;
                NotifyOfPropertyChange(() => SelectedManager);
            }
        }
        public bool IsRemoveClientEnabled
        {
            get { return _isRemoveClientEnabled; }
            set 
            {
                _isRemoveClientEnabled = value;
                NotifyOfPropertyChange(() => IsRemoveClientEnabled);
            }
        }
        public int LBSelectedIndex
        {
            get { return _lBSelectedIndex; }
            set
            {
                _lBSelectedIndex = value;
                NotifyOfPropertyChange(() => LBSelectedIndex);
            }
        }
        public List<Client> Clients
        {
            get { return clients; }
            set { clients = value; }
        }
        #endregion

        #region ctors
        public ClientByManagerViewModel()
        {
            SelectedManager = ShellViewModel.ChoosenObject as Manager;
            FillLB();
            LBSelectedIndex = -1;
        }
        #endregion

        #region events
        public void BindClientButtonClick()
        {

        }

        public void RemoveClientButtonClick()
        {
            int clientIDToRemove = Clients[LBSelectedIndex].ID;

            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show($"Отвязать клиента " +
                                                                            $"{clients[LBSelectedIndex].Name}"
                                                                             + "\n"
                                                                             + $"+ от менеджера {(ShellViewModel.ChoosenObject as Manager).Name}?",
                                                                             "Подтвердите удаление",
                                                                             System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            DBOperator.Relation(clientIDToRemove, new Manager(), true);
            FillLB();

            
        }

        public void LBSelectionChanged()
        {
            if (LBSelectedIndex > -1)
            {
                IsRemoveClientEnabled = true;
            }
            else
            {
                IsRemoveClientEnabled = false;
            }
        }
        #endregion

        #region functions
        private void FillLB()
        {
            if ((ShellViewModel.ChoosenObject as Manager) is null)
            {
                return;
            }

            clients = DBOperator.GetClientsByManager(((ShellViewModel.ChoosenObject as Manager).ID));
            List<string> cStr = new List<string>();


            for (int i = 0; i < clients.Count; i++)
            { 
                cStr.Add(clients[i].Name);
            }

            ClientsNamesList = new BindableCollection<string>(cStr);
        }
        #endregion
    }
}
