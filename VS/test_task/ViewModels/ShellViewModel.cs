using Caliburn.Micro;
using test_task.Development;
using test_task.DB;
using test_task.Views;
using test_task.Classes;
using System.Collections.Generic;
using System.Windows;
using System;

namespace test_task.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        #region static_members
        #region static_fields
        private static object choosenObject = null;
        private static object choosenType = null;
        #endregion

        #region static_properties
        public static object ChoosenObject { get => choosenObject; set => choosenObject = value; }
        public static object ChoosenType { get => choosenType; set => choosenType = value; }
        #endregion
        #endregion

        #region fields
        private string _selectedMainAction;
        private BindableCollection<string> _mainActions;
        private object _dataOut;
        private int _dataOutSelectedIndex;
        private object _dataOutSelectedItem;
        private bool _isDataGridEnabled = true;
        private bool _isAddButtonEnabled = false;
        private bool _isRemoveButtonEnabled = false;
        #endregion

        #region properties
        public string SelectedMainAction
        {
            get => _selectedMainAction;
            set
            {
                _selectedMainAction = value;
                //choosenType = _selectedMainAction;
                NotifyOfPropertyChange(() => SelectedMainAction);
            }
        }
        public BindableCollection<string> MainActions
        {
            get { return _mainActions; }
            private set 
            {
                _mainActions = value;
                NotifyOfPropertyChange(() => MainActions);
            }
        }
        public object DataOut
        {
            get { return _dataOut; }
            set 
            {
                _dataOut = value;
                NotifyOfPropertyChange(() => DataOut);
            }
        }
        public int DataOutSelectedIndex
        {
            get { return _dataOutSelectedIndex; }
            set 
            { 
                _dataOutSelectedIndex = value;
                NotifyOfPropertyChange(() => DataOutSelectedIndex);
            }
        }
        public object DataOutSelectedItem
        {
            get => _dataOutSelectedItem;
            set 
            {
                _dataOutSelectedItem = value;
                NotifyOfPropertyChange(() => DataOutSelectedItem);
            }
        }
        public bool IsDataGridEnabled
        {
            get => _isDataGridEnabled;
            set 
            {
                _isDataGridEnabled = value;
                NotifyOfPropertyChange(() => IsDataGridEnabled);
            }
        }
        public bool IsAddButtonEnabled
        {
            get => _isAddButtonEnabled;
            set 
            {
                _isAddButtonEnabled = value;
                NotifyOfPropertyChange(() => IsAddButtonEnabled);
            }
        }
        public bool IsRemoveButtonEnabled
        {
            get => _isRemoveButtonEnabled;
            set 
            {
                _isRemoveButtonEnabled = value;
                NotifyOfPropertyChange(() => IsRemoveButtonEnabled);
            }
        }
        #endregion

        #region ctors
        public ShellViewModel()
        {
            //////////////////////////
            DBOperator.Initialize();
            //////////////////////////
            DisplayName = "Задание";
            MainActions = new BindableCollection<string>(ShellViewActionsLists.mainActions);
            //////////////////////////
            ShellView.CViewModel = this;
            AddNewItemViewModel.CViewModel = this;
            //////////////////////////
        }
        #endregion

        #region enents
        public void MainSelectionChanged()
        {
            IsRemoveButtonEnabled = false;
            IsAddButtonEnabled = false;
            ActivateItem(null);
            if (SelectedMainAction == ShellViewActionsLists.mainActions[0])
            {
                choosenType = new Manager();
                FillDG();
                //ActivateItem(new ClientByManagerViewModel());
                //ContextItemActions = new BindableCollection<string>(ShellViewActionsLists.contextActionsManager);
            }
            else if (SelectedMainAction == ShellViewActionsLists.mainActions[1])
            {
                choosenType = new Client();
                FillDG();
                //ActivateItem(null);
                //ContextItemActions = new BindableCollection<string>(ShellViewActionsLists.contextActionsClient);
            }
            else if (SelectedMainAction == ShellViewActionsLists.mainActions[2])
            {
                choosenType = new Product();
                FillDG();
                //ActivateItem(null);
                //ContextItemActions = new BindableCollection<string>(ShellViewActionsLists.contextActionsProduct);
            }
        }

        public void DataGridSelectionChanged()
        {
            List<object> list = DataOut as List<object>;
            ActivateItem(null);

            if (!(list is null) & (list.Count > 0) & (DataOutSelectedIndex > -1) & (DataOutSelectedIndex < (DataOut as List<object>).Count))
            {
                IsAddButtonEnabled = true;
                IsRemoveButtonEnabled = true;
                switch (choosenType)
                {
                    case Manager _:
                        choosenObject = new Manager(DataOutSelectedItem as Manager);
                        ActivateItem(new ManagerWindowViewModel());
                        break;

                    case Client _:
                        choosenObject = new Client(DataOutSelectedItem as Client);
                        ActivateItem(new ClientWindowViewModel());
                        break;

                    case Product _:
                        choosenObject = new Product(DataOutSelectedItem as Product);
                        ActivateItem(new ProductWindowViewModel());
                        break;

                    default:
                        break;
                }
            }

        }

        public void DataOutRowEditEnding()
        {
            switch (choosenType)
            {
                case Manager m:
                    DBOperator.UpdateObject((DataOut as List<object>)[DataOutSelectedIndex] as Manager);
                    break;
                case Client c:
                    DBOperator.UpdateObject((DataOut as List<object>)[DataOutSelectedIndex] as Client);
                    break;
                case Product p:
                    DBOperator.UpdateObject((DataOut as List<object>)[DataOutSelectedIndex] as Product);
                    break;
                default:
                    break;
            }
            DataOutSelectedIndex = -1;
            DataOutSelectedItem = null;
        }

        public void AddButtonClick()
        {
            IsDataGridEnabled = false;
            ActivateItem(new AddNewItemViewModel());

        }

        public void RemoveButtonClick()
        {
            if (DataOutSelectedIndex < 0 
                || DataOutSelectedItem is null 
                || choosenObject is null 
                || choosenObject.GetType() != choosenObject.GetType()
                || choosenObject.GetType() != DataOutSelectedItem.GetType())
            {
                return;
            }
            DBOperator.DeleteObject(choosenObject);
            FillDG();

        }
        #endregion

        #region functions
        public void EnableDataGrid()
        {
            IsDataGridEnabled = true;
            FillDG();
        }

        private void FillDG()
        {
            DataOut = DBOperator.GetObjects(choosenType);
            IsDataGridEnabled = true;
        }
        #endregion

        #region debug&dev
        public void ChangeConnectionString()
        {
            ShowDebug();
        }

        public void ShowDebug()
        {
            var df = new DebugForm();
            df.Show();
        }
        #endregion

        #region garbage
        //public delegate void SomeHandler();
        //private event SomeHandler _notify;

        //public event SomeHandler Notify
        //{
        //    add
        //    {
        //        _notify += value;
        //    }
        //    remove
        //    {
        //        _notify -= value;
        //    }
        //}


        //public void SomeFunc()
        //{
        //    MessageBox.Show("!");
        //}

        #endregion
    }
}
