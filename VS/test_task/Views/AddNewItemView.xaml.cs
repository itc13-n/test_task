using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace test_task.Views
{
    /// <summary>
    /// Interaction logic for AddNewItemView.xaml
    /// </summary>
    public partial class AddNewItemView : UserControl
    {
        public AddNewItemView()
        {
            InitializeComponent();
        }

        #region events
        private void CreatedObject_Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).Text = Regex.Replace((sender as TextBox).Text, "[^0-9.-]+", "");
        }
        #endregion


    }
}
