using System.Windows;
using System.Windows.Controls;
using test_task.ViewModels;

namespace test_task.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public static object CViewModel;

        public ShellView()
        {
            InitializeComponent();
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem != null)
            {
                (sender as DataGrid).RowEditEnding -= DataGrid_RowEditEnding;
                (sender as DataGrid).CommitEdit();
                (sender as DataGrid).Items.Refresh();
                (sender as DataGrid).RowEditEnding += DataGrid_RowEditEnding;
                (CViewModel as ShellViewModel).DataOutRowEditEnding();
            }
            else return;

        }
    }
}
