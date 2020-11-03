using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using test_task.Classes;

namespace test_task.ViewModels
{
    class AddNewItemViewModel : Conductor<object>
    {
        #region static_members
        public static ShellViewModel CViewModel;
        #endregion

        #region fields
        private string _choosenType;
        private object _createdObject;
        private bool _m1;
        private bool _m2;
        private bool _m3;
        #endregion

        #region properties
        public string ChoosenType
        {
            get => _choosenType;
            set
            {
                _choosenType = value;
                NotifyOfPropertyChange(() => ChoosenType);
            }
        }
        public object CreatedObject
        {
            get => _createdObject;
            set 
            {
                _createdObject = value;
                NotifyOfPropertyChange(() => CreatedObject);
            }
        }
        public bool M1
        {
            get { return _m1; }
            set { _m1 = value; }
        }
        public bool M2
        {
            get { return _m2; }
            set { _m2 = value; }
        }
        public bool M3
        {
            get { return _m3; }
            set { _m3 = value; }
        }
        #endregion

        #region ctors
        public AddNewItemViewModel()
        {
            ChoosenType = ShellViewModel.ChoosenObject.GetType().Name;

            switch (ShellViewModel.ChoosenType)
            {
                case Manager _:
                    CreatedObject = new Manager();
                    break;
                case Client _:
                    CreatedObject = new Client();
                    break;
                case Product _:
                    CreatedObject = new Product();
                    break;
                default:
                    break;
            }

        }
        #endregion

        #region event
        public void SaveButtonClick()
        {
            if (typeof(Product) == CreatedObject.GetType())
            {
                if (M1)
                {
                    (CreatedObject as Product).Period = 1;
                }
                else if (M2)
                {
                    (CreatedObject as Product).Period = 3;
                }
                else
                {
                    (CreatedObject as Product).Period = 12;
                }
            }

            List<object> list = new List<object>();
            list.Add(CreatedObject);
            try
            {
                DB.DBOperator.InsertData(list);
            }

            catch(ArgumentNullException)
            {
                MessageBox.Show("Введите данные ещё раз!");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            CViewModel.EnableDataGrid();
            CViewModel.ActivateItem(null);
        }

        public void CancelButtonClick()
        {
            CViewModel.EnableDataGrid();
            ActivateItem(null);
        }
        #endregion

    }
}
