using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using test_task.Classes;

namespace test_task.ViewModels
{   //AddClientToManager
    class AddClientToManagerViewModel : Screen
    {
        #region fields
        private List<string> _clientsToAddNamesList;
        private int _clientsToAddLBSelectedIndex;
        private List<Client> _clients;
        #endregion

        #region properties
        public List<string> ClientsToAddNamesList
        {
            get => _clientsToAddNamesList;
            set
            {
                _clientsToAddNamesList = value;
                NotifyOfPropertyChange(() => ClientsToAddNamesList);
            }
        }
        public int ClientsToAddLBSelectedIndex
        {
            get => _clientsToAddLBSelectedIndex;
            set
            {
                _clientsToAddLBSelectedIndex = value;
                NotifyOfPropertyChange(() => ClientsToAddLBSelectedIndex);
            }
        }
        public List<Client> Clients
        { get => _clients; set => _clients = value; }
        #endregion

        #region ctors
        public AddClientToManagerViewModel()
        {
            ClientsToAddLBSelectedIndex = -1;

            FillLB();
        }
        #endregion

        #region events
        public void LBSelectionChanged()
        {
            if (ClientsToAddLBSelectedIndex > -1)
            {
                ManagerWindowViewModel.Client_IDAdd = Clients[ClientsToAddLBSelectedIndex].ID;
            }
        }
        #endregion

        #region functions
        public void FillLB()
        {
            Clients = DB.DBOperator.GetObjects(new Client());

            List<string> cStr = new List<string>();

            for (int i = 0; i < Clients.Count; i++)
            {
                cStr.Add(Clients[i].Name);
            }

            ClientsToAddNamesList = new List<string>(cStr);
        }
        #endregion
    }
}
