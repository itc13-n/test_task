using Caliburn.Micro;
using test_task.Development;
using test_task.DB;
using test_task.Views;
using test_task.Classes;
using System.Collections.Generic;
using System.Windows;

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
        private BindableCollection<string> _changeItemActions = new BindableCollection<string>();
        private BindableCollection<string> _contextItemAction = new BindableCollection<string>();
        private object _dataOut;
        private int _dataOutSelectedIndex;
        private bool _writeToDB = false;
        private bool _canUseContextItem;
        private object _dataOutSelectedItem;
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
        public object DataOutSelectedItem
        {
            get => _dataOutSelectedItem;
            set 
            {
                _dataOutSelectedItem = value;
                NotifyOfPropertyChange(() => DataOutSelectedItem);
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
