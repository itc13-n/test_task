using Caliburn.Micro;
using System.Collections.Generic;
using test_task.Classes;
using test_task.DB;

namespace test_task.ViewModels
{
    class ProductWindowViewModel : Screen
    {
        #region fields
        private BindableCollection<string> _clietnsNamesList;
        private List<Client> _clients;
        #endregion

        #region properties
        public List<Client> Clients
        {
            get => _clients; 
            
            private set
            {
                _clients = value;
                NotifyOfPropertyChange(() => Clients);
            }
        }
        public BindableCollection<string> ClientsNamesList
        {
            get => _clietnsNamesList; 
            private set
            {
                _clietnsNamesList = value;
                NotifyOfPropertyChange(() => ClientsNamesList);
            }
        }
        #endregion

        #region ctors
        public ProductWindowViewModel()
        {
            FillLB();
        }
        #endregion

        #region functions
        private void FillLB()
        {
            if ((ShellViewModel.ChoosenObject as Product) is null)
            {
                return;
            }

            Clients = DBOperator.GetClientsByProduct((ShellViewModel.ChoosenObject as Product).ID);

            List<string> cStr = new List<string>();

            for (int i = 0; i < Clients.Count; i++)
            {
                cStr.Add(Clients[i].Name);
            }

            if (cStr.Count < 1)
            {
                cStr.Add("Клиенты отсутствуют");
            }
            ClientsNamesList = new BindableCollection<string>(cStr);
        }
        #endregion
    }
}
