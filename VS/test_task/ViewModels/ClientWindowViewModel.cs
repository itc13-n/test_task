using Caliburn.Micro;
using System.Collections.Generic;
using System.Windows;
using test_task.Classes;
using test_task.DB;

namespace test_task.ViewModels
{
    class ClientWindowViewModel : Conductor<object>
    {
        #region static_members
        #region static_fields
        private static int _product_IDAdd;
        private static ClientWindowViewModel _activeViewModel;
        #endregion

        #region static_properties
        public static int Product_IDAdd
        {
            get => _product_IDAdd; set
            {
                if (value > -1)
                {
                    _product_IDAdd = value;
                    ActiveViewModel.ActivateItem(null);

                    Product p = new Product();
                    p.ID = _product_IDAdd;
                    DBOperator.Relation((ShellViewModel.ChoosenObject as Client).ID, p);
                    ActiveViewModel.FillLB();

                }
            }
        }
        public static ClientWindowViewModel ActiveViewModel
        { get => _activeViewModel; set => _activeViewModel = value; }
        #endregion
        #endregion

        #region fields
        private int _lBSelectedIndex;
        private Client _selectedClient;
        private Manager _selectedClientsManager;
        private BindableCollection<string> _productsNamesList;
        private List<Product> _products;
        private bool _isRemoveProductEnabled;
        #endregion

        #region properties
        public int LBSelectedIndex
        {
            get => _lBSelectedIndex;
            set 
            {
                _lBSelectedIndex = value;
                NotifyOfPropertyChange(() => LBSelectedIndex);
            }
        }
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                NotifyOfPropertyChange(() => _selectedClient);
            }
        }
        public Manager SelectedClientsManager
        {
            get => _selectedClientsManager;
            set => _selectedClientsManager = value;
        }
        public BindableCollection<string> ProductsNamesList
        {
            get => _productsNamesList;
            set
            {
                _productsNamesList = value;
                NotifyOfPropertyChange(() => ProductsNamesList);
            }
        }
        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        public bool IsRemoveProductEnabled
        {
            get => _isRemoveProductEnabled; set
            {
                _isRemoveProductEnabled = value;
                NotifyOfPropertyChange(() => IsRemoveProductEnabled);
            }
        }
        #endregion

        #region ctors
        public ClientWindowViewModel()
        {
            SelectedClient = ShellViewModel.ChoosenObject as Client;
            SelectedClientsManager = DBOperator.GetManagerByClient(SelectedClient.ID);

            FillLB();

            LBSelectedIndex = -1;
        }
        #endregion

        #region events
        public void BindProductButtonClick()
        {
            ActiveViewModel = this;
            ActivateItem(new AddProductToClientViewModel());
        }

        public void RemoveProductButtonClick()
        {
            int productIDToRemove = Products[LBSelectedIndex].ID;

            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show($"Вернуть товар: "
                + $"{Products[LBSelectedIndex].Name}" + "\n" + $"(возвращён клиентом {(ShellViewModel.ChoosenObject as Client).Name})?",
                "Подтвердите действие",
                System.Windows.Forms.MessageBoxButtons.YesNo);

            if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            Product p = new Product();
            p.ID = productIDToRemove;

            DBOperator.Relation((ShellViewModel.ChoosenObject as Client).ID, p, remove: true);
            FillLB();

        }

        public void LBSelectionChanged()
        {
            if (LBSelectedIndex > -1)
            {
                IsRemoveProductEnabled = true;
            }
            else
            {
                IsRemoveProductEnabled = false;
            }
        }

        public void ClientButtonClick()
        {
            ActiveViewModel = this;
            ActivateItem(new ContactWindowViewModel());
        }
        #endregion

        #region functions
        private void FillLB()
        {
            if ((ShellViewModel.ChoosenObject as Client) is null)
            {
                return;
            }

            Products = DBOperator.GetProductsByClient((ShellViewModel.ChoosenObject as Client).ID);

            List<string> cStr = new List<string>();


            for (int i = 0; i < Products.Count; i++)
            {
                cStr.Add(Products[i].Name);
            }

            ProductsNamesList = new BindableCollection<string>(cStr);
        }
        #endregion
    }
}
