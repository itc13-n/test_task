using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using test_task.Development;

namespace test_task.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string inputString;

        public string InputField
        {
            get 
            { 
                return inputString; 
            }
            set 
            {
                inputString = value;
                NotifyOfPropertyChange(() => InputField);
            }
        }

        public void AddObject()
        {
            MessageBox.Show("Add to DB function called!");

        }

        public void ShowDebug()
        {
            var df = new DebugForm();
            df.Show();
        }
    }
}
