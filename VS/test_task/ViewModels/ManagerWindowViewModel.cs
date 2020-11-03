using Caliburn.Micro;
using System.Collections.Generic;
using test_task.Classes;
using test_task.DB;

namespace test_task.ViewModels
{
    public class ManagerWindowViewModel : Conductor<object>
    {

        #region static_members
        #region static_fields
        private static int _client_IDAdd;
        private static ManagerWindowViewModel _activeViewModel;
        #endregion

        #region static_properties
        public static int Client_IDAdd
        {
            get => _client_IDAdd; set
            {
                if (value > -1)
                {
                    _client_IDAdd = value;
                    ActiveViewModel.ActivateItem(null);

                    DBOperator.Relation(_client_IDAdd, ShellViewModel.ChoosenObject);
                    ActiveViewModel.FillLB();

                }
            }
        }
        public static ManagerWindowViewModel ActiveViewModel
        { get => _activeViewModel; set => _activeViewModel = value; }
        #endregion
        #endregion

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
            get => clients;
            set => clients = value;
        }
        #endregion

        #region ctors
        public ManagerWindowViewModel()
        {
            SelectedManager = ShellViewModel.ChoosenObject as Manager;
            FillLB();
            LBSelectedIndex = -1;
        }
        #endregion

        #region events
        public void BindClientButtonClick()
        {
            ActiveViewModel = this;
            ActivateItem(new AddClientToManagerViewModel());
        }

        public void RemoveClientButtonClick()
        {
            int clientIDToRemove = Clients[LBSelectedIndex].ID;

            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show($"Отвязать клиента "
                + $"{clients[LBSelectedIndex].Name}" + "\n" + $"от менеджера {(ShellViewModel.ChoosenObject as Manager).Name}?",
                "Подтвердите действие",
                System.Windows.Forms.MessageBoxButtons.YesNo);

            if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            DBOperator.Relation(clientIDToRemove,SelectedManager, true);
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
