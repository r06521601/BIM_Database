using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Autodesk.Revit.Creation;
using System.Globalization;

namespace BIM_Database
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        ExternalEvent externalevent;
        ExternalEvent externalevent_ood;
        Handler handler;
        OOD ood;

        public static string connection_information = "server=localhost;user id=roy;password=;database=final;SslMode=none";
        public string item = "";
        public string custom_query = "";
        MySqlConnection mySqlConnection = new MySqlConnection(connection_information);

        public UIDocument uidoc;

        public Form1(UIDocument _uidoc)
        {
            InitializeComponent();
            uidoc = _uidoc;
            
            handler = new Handler();
            ood = new OOD();
            ood.timespend = new double();
            ood.number = new int();
            handler.datagridview_ids = new List<string>();
            externalevent = ExternalEvent.Create(handler);

            externalevent_ood = ExternalEvent.Create(ood);
        }


        private void button1_Click(object sender, EventArgs e)
        {

            mySqlConnection.Open();
            string query = "select * from " + item;
            custom_query = query;
            textBox1.Text = custom_query;
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySqlConnection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            mySqlConnection.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            item = comboBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Query_Time: ";
            mySqlConnection.Open();
            DateTime time = DateTime.Now;

            MySqlDataAdapter adapter = null;
            try
            {
                DataTable dataTable = new DataTable();



                for (int i = 0; i < 100; i++)
                {
                    dataTable = new DataTable();
                    adapter = new MySqlDataAdapter(custom_query, mySqlConnection);
                    adapter.Fill(dataTable);
                }
                DateTime time2 = DateTime.Now;

                label1.Text += (time2 - time).TotalMilliseconds;
                label1.Text += " ms; num: " + dataTable.Rows.Count; ;

                dataGridView1.DataSource = dataTable;
            }
            catch { }
            mySqlConnection.Close();



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            custom_query = textBox1.Text;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



            if (e.ColumnIndex == 0)
            {


                handler.datagridview_ids.Clear();

                for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
                {
                    handler.datagridview_ids.Add(dataGridView1.SelectedCells[i].Value.ToString());
                }

            }
            externalevent.Raise();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

            mySqlConnection.Open();
            string query = "show tables";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySqlConnection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                comboBox1.Items.Add(row[0].ToString());
            }
            mySqlConnection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Text = "OOD_Query_Time: ";
            externalevent_ood.Raise();
            label4.Text += ood.timespend;
            label4.Text += " ms; num: "+ood.number;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mySqlConnection.Open();
            string query = "select sum(範圍) from 樓板 where 範圍 < "+textBox2.Text;
            string one_floor_area_query = "select sum(範圍) from 樓板 join(多個樓層)  on 樓板.樓層 = 多個樓層.ID where 多個樓層.名稱 = '1F'";
            double sum_area = new double();
            double sum_one_floor_area = new double();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySqlConnection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                sum_area = double.Parse(row[0].ToString());
            }
            MySqlDataAdapter adapter_2 = new MySqlDataAdapter(one_floor_area_query, mySqlConnection);

            DataTable dataTable_2 = new DataTable();
            adapter_2.Fill(dataTable_2);
            foreach (DataRow row in dataTable_2.Rows)
            {
                sum_one_floor_area = double.Parse(row[0].ToString());
            }
            label5.Text = "容積率: ";
            label6.Text = "建蔽率: ";

            label5.Text += (sum_area/(double.Parse(textBox2.Text))*100).ToString("0.00")+"%";
            label6.Text += (sum_one_floor_area / (double.Parse(textBox2.Text)) * 100).ToString("0.00") + "%";
            mySqlConnection.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
