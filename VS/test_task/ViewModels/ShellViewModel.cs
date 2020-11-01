using Caliburn.Micro;
using test_task.Development;
using test_task.DB;
using test_task.Views;
using test_task.Classes;
using System.Collections.Generic;

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
        private string _selectedContextItem;
        private string _selectedChangeItem;
        private BindableCollection<string> _mainActions;
        private BindableCollection<string> _changeItemActions = new BindableCollection<string>();
        private BindableCollection<string> _contextItemAction = new BindableCollection<string>();
        private object _dataOut;
        private int _dataOutSelectedIndex;
        private bool _writeToDB = false;
        private bool _canUseContextItem;
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
        public string SelectedContextItem
        {
            get => _selectedContextItem;
            set
            {
                _selectedContextItem = value;
                NotifyOfPropertyChange(() => SelectedContextItem);
            }
        }
        public string SelectedChangeItem
        {
            get => _selectedChangeItem;
            set
            {
                _selectedChangeItem = value;
                NotifyOfPropertyChange(() => SelectedChangeItem);
            }
        }
        public BindableCollection<string> ContextItemActions
        {
            get { return _contextItemAction; }
            set 
            {
                _contextItemAction = value;
                NotifyOfPropertyChange(() => ContextItemActions);
            }
        }
        public BindableCollection<string> ChangeItemActions
        {
            get { return _changeItemActions; }
            set 
            {
                _changeItemActions = value;
                NotifyOfPropertyChange(() => ChangeItemActions);
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
                
                if (_writeToDB)
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
                    FillDG();
                }
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
        public bool CanUseContextItem
        {
            get { return _canUseContextItem; }
            set
            {
                _canUseContextItem = value;
                NotifyOfPropertyChange(() => CanUseContextItem);
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
            ChangeItemActions = new BindableCollection<string>(ShellViewActionsLists.changeItemActions);
            //////////////////////////
            
            //////////////////////////
        }
        #endregion

        #region enents
        public void MainSelectionChanged()
        {
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
         
        //public void ContextSelectionChanged()
        //{
        //    if (SelectedContextItem is null)
        //    {
        //        return;
        //    }
        //    if (SelectedMainAction == ShellViewActionsLists.mainActions[0]) //managers
        //    {
        //        if (SelectedContextItem == ShellViewActionsLists.contextActionsManager[0]) //bind client
        //        {
        //            ///////////////////////////////////////////////////
        //            ActivateItem(null);                              //
        //            //throw new NotImplementedException("Bind Client");//
        //            ///////////////////////////////////////////////////
        //        }
        //        else if (SelectedContextItem == ShellViewActionsLists.contextActionsManager[1]) //show clients list
        //        {
        //            ActivateItem(new ClientByManagerViewModel());

        //        }
        //    }//managers

        //    else if (SelectedMainAction == ShellViewActionsLists.mainActions[1])//clients
        //    {
        //        if (SelectedContextItem == ShellViewActionsLists.contextActionsClient[0]) //assign Manager
        //        {
                    
        //            //////////////////////////////////////////////////////
        //            ActivateItem(null);                                 //
        //            //throw new NotImplementedException("assign Manager");//
        //            //////////////////////////////////////////////////////
        //        }//assign Manager
        //        else if (SelectedContextItem == ShellViewActionsLists.contextActionsClient[1]) //View contact
        //        {
        //            ////////////////////////////////////////////////////
        //            ActivateItem(null);                               //
        //            //throw new NotImplementedException("View contact");//
        //            ////////////////////////////////////////////////////
        //        }//View contact
        //        else if (SelectedContextItem == ShellViewActionsLists.contextActionsClient[2]) //product list
        //        {
        //            ////////////////////////////////////////////////////
        //            ActivateItem(null);                               //
        //           //throw new NotImplementedException("product list");//
        //            ////////////////////////////////////////////////////
        //        }//product list
        //    }//clients

        //    else if (SelectedMainAction == ShellViewActionsLists.mainActions[2])//products
        //    {
        //        if (SelectedContextItem == ShellViewActionsLists.contextActionsProduct[0])//give to client
        //        {
        //            //////////////////////////////////////////////////////
        //            ActivateItem(null);                                 //
        //            //throw new NotImplementedException("give to client");//
        //            //////////////////////////////////////////////////////
        //        }//give to client
        //        else if (SelectedContextItem == ShellViewActionsLists.contextActionsProduct[1])//clients List
        //        {
        //            ////////////////////////////////////////////////////
        //            ActivateItem(null);                               //
        //            //throw new NotImplementedException("clients List");//
        //            ////////////////////////////////////////////////////
        //        }//clients List
        //    }//products
        //    SelectedContextItem = null;
        //}

        public void DataGridSelectionChanged()
        {
            List<object> list = DataOut as List<object>;
            ActivateItem(null);

            if (!(list is null) & (list.Count > 0) & (DataOutSelectedIndex > -1))
            {
                CanUseContextItem = true;
                switch (choosenType)
                {
                    case Manager _:
                        choosenObject = list[DataOutSelectedIndex] as Manager;
                        ActivateItem(new ClientByManagerViewModel());
                        break;

                    case Client _:
                        choosenObject = list[DataOutSelectedIndex] as Client;

                        break;

                    case Product _:
                        choosenObject = list[DataOutSelectedIndex] as Product;

                        break;

                    default:
                        break;
                }                
            }
        }
        #endregion

        #region functions
        private void FillDG()
        {
            DataOut = DBOperator.GetObjects(choosenType);
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

        #region Garbage
        public void SMTH()
        {
            //MessageBox.Show(((DataOut as List<object>)[Index] as Manager).Name);
        }



        #endregion
    }
}
