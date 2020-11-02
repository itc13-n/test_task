using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_task.Classes;

namespace test_task.ViewModels
{
    class AddProductToClientViewModel : Screen
    {
        #region fields
        private List<string> _productsToAddNamesList;
        private int _productsToAddLBSelectedIndex;
        private List<Product> _products;
        #endregion

        #region properties
        public List<string> ProductsToAddNamesList
        {
            get => _productsToAddNamesList;
            set
            {
                _productsToAddNamesList = value;
                NotifyOfPropertyChange(() => ProductsToAddNamesList);
            }
        }
        public int ProductsToAddLBSelectedIndex
        {
            get => _productsToAddLBSelectedIndex;
            set
            {
                _productsToAddLBSelectedIndex = value;
                NotifyOfPropertyChange(() => ProductsToAddLBSelectedIndex);
            }
        }
        public List<Product> Products
        { get => _products; set => _products = value; }
        #endregion

        #region ctors
        public AddProductToClientViewModel()
        {
            ProductsToAddLBSelectedIndex = -1;

            FillLB();
        }
        #endregion

        #region events
        public void LBSelectionChanged()
        {
            if (ProductsToAddLBSelectedIndex > -1)
            {
                ClientWindowViewModel.Product_IDAdd = Products[ProductsToAddLBSelectedIndex].ID;
            }
        }
        #endregion

        #region functions
        public void FillLB()
        {
            Products = DB.DBOperator.GetObjects(new Product());

            List<string> cStr = new List<string>();

            for (int i = 0; i < Products.Count; i++)
            {
                cStr.Add(Products[i].Name);
            }

            ProductsToAddNamesList = new List<string>(cStr);
        }
        #endregion
    }
}
