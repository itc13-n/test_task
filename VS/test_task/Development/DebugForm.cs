using System;
using System.Collections.Generic;
using System.Windows.Forms;
using test_task.Classes;
using test_task.DB;

namespace test_task.Development
{
    public partial class DebugForm : Form
    {
        List<string> list;

        public DebugForm()
        {
            InitializeComponent();
            DBOperator.Initialize();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            List<string> returnedCollection;

            if (richTextBox1.SelectedText.Length > 0)
            {
                returnedCollection = DBOperator.DirectQueryR(richTextBox1.SelectedText);
            }
            else
            {
                returnedCollection = DBOperator.DirectQueryR(richTextBox1.Text);
            }

            if (!(returnedCollection is null))
            {
                foreach (string item in returnedCollection)
                {
                    listBox2.Items.Add(item);
                }
            }

             
        }

        private void button1_Click(object sender, EventArgs e)
        {                   
            List <Manager> managers = DBOperator.GetObjects(new Manager());
            
            listBox1.Items.Clear();

            foreach (var item in managers)
            {
                listBox1.Items.Add(item.Name);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                if (richTextBox1.SelectedText.Length > 0)
                {
                    DBOperator.ClearTable(richTextBox1.SelectedText);
                }
                else
                {
                    DBOperator.ClearTable(richTextBox1.Text);
                }
                
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Client> clients = DBOperator.GetObjects(new Client());

            listBox1.Items.Clear();

            foreach (var item in clients)
            {
                listBox1.Items.Add(item.Name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Product> products = DBOperator.GetObjects(new Product());

            listBox1.Items.Clear();

            foreach (var item in products)
            {
                listBox1.Items.Add(item.Name);
            }
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string s = richTextBox1.Text;
            richTextBox1.Clear();

            list = new List<string>();

            foreach (string item in s.Split())
            {
                list.Add("('" + item + "', ");
            }
            //for (int i = 0; i < list.Count; i++)
            //{
            //    richTextBox1.Text += "\n" + list[i];
            //}



            //if (richTextBox1.SelectedText.Length > 0)
            //{
            //    textBox1.Text = DBOperator.DirectQuery(richTextBox1.SelectedText).ToString(); 
            //}
            //else
            //{
            //    textBox1.Text = DBOperator.DirectQuery(richTextBox1.Text).ToString();
            //}

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string s = richTextBox1.Text;
            string[] sArr = s.Split();
            richTextBox1.Clear();


            for (int i = 0; i < list.Count - 1; i++)
            {
                list[i] = list[i] + sArr[i] + "), ";
                richTextBox1.Text += "\n" + list[i];
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string s = richTextBox1.Text;
            string[] sArr = s.Split();
            richTextBox1.Clear();



            for (int i = 0; i < list.Count - 1; i++)
            {
                list[i] = list[i] + sArr[i] + ", ";
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {

            if (richTextBox1.SelectedText.Length > 0)
            {
                textBox1.Text = DBOperator.DirectQuery(richTextBox1.SelectedText).ToString();
            }
            else
            {
                textBox1.Text = DBOperator.DirectQuery(richTextBox1.Text).ToString();
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Manager m = new Manager();
            m.Name = richTextBox1.Text;
            List<Manager> managers = new List<Manager>();
            managers.Add(m);
            DBOperator.InsertData<Manager>(managers);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string[] s = richTextBox1.Text.Split();
            List<Manager> managers = DBOperator.GetObjects<Manager>(new Manager(), where: $"ID = {s[0]}");
            if (managers.Count >0)
            {

                DBOperator.DeleteObject<Manager>(managers[0], $" ID = {managers[0].ID} ");
            }
            
        }
    }
}
