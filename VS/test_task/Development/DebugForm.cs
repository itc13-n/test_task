﻿using System;
using System.Collections.Generic;
using System.Configuration;
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

        private void button12_Click(object sender, EventArgs e)
        {
            string[] s = richTextBox1.Text.Split('/');
            
            List<Product> DBObjects = new List<Product>();
            Product o = new Product();

            switch (s[0].ToLower())
            {
                case "insert":
                    o.ID = 48;
                    o.Name = "TEST";
                    o.Price = 1.52m;
                    o.Subscribtion = false;
                    o.Period = null;
                    o.Owner = null;

                    DBObjects.Add(o);
                    DBOperator.InsertData(DBObjects);
                    break;

                case "delete":
                    DBObjects = DBOperator.GetObjects(new Product(), where: $"ID = {s[1]}");
                    if (DBObjects.Count > 0)
                    {
                        DBOperator.DeleteObject(DBObjects[0], $" ID = {DBObjects[0].ID} ");
                    }
                    break;

                case "get":
                    DBObjects = DBOperator.GetObjects(o, where: $"ID = {s[1]}");

                    listBox2.Items.Clear();
                    foreach (var item in DBObjects)
                    {
                        listBox2.Items.Add(DBObjects[0].ID + " " + DBObjects[0].Name + " " + DBObjects[0].Price + DBObjects[0].Subscribtion + " " + DBObjects[0].Period + " " + DBObjects[0].Owner.ToString());
                    }
                    break;

                case "update":
                    o.ID = 48;
                    o.Name = "TEST_NEW";
                    o.Price = 1.52m;
                    o.Subscribtion = false;
                    o.Period = null;
                    o.Owner = null;

                    MessageBox.Show(o.ID + " " + o.Name + " " + o.Price + o.Subscribtion + " " + o.Period + " " + o.Owner.ToString());

                    DBObjects.Add(o);
                    textBox1.Text = DBOperator.UpdateObject(o).ToString();
                    break;

                default:
                    break;
            }
            DBObjects.Clear();


            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Use default connection string?", "Which connection string to use?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                return;
            }
            DBOperator.ConnectionString = richTextBox1.SelectedText;
        }

    }
}
