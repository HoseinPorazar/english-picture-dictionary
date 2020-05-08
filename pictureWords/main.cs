using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace pictureWords
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        OleDbConnection con;
        private NotifyIcon m_notifyicon;
        private ContextMenu m_menu;  
        private void main_Load(object sender, EventArgs e)
        {

            con = new OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=db.mdb;Jet OLEDB:Database Password=h@#%^ein;");

            
            con.Open();
            left = SystemInformation.VirtualScreen.Width - this.Width - 150;

            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0,
                new MenuItem("Show", new System.EventHandler(Show_Click)));
            m_menu.MenuItems.Add(1,
                new MenuItem("Hide", new System.EventHandler(Hide_Click)));
            m_menu.MenuItems.Add(2,
                new MenuItem("Exit", new System.EventHandler(Exit_Click)));
            m_notifyicon = new NotifyIcon();
            m_notifyicon.Text = "learn english";
            m_notifyicon.Visible = true;
            m_notifyicon.Icon = new Icon(Application.StartupPath+"\\20.ico"); 
            m_notifyicon.ContextMenu = m_menu;
            m_notifyicon.DoubleClick += new EventHandler(m_notifyicon_DoubleClick);
            cmbCat.SelectedIndex = 0;
        }

        void m_notifyicon_DoubleClick(object sender, EventArgs e)
        {
            
            this.TopMost = true;
            this.Show();
            this.TopMost = false;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            m_notifyicon.Dispose();
            Environment.Exit(0);
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Show_Click(object sender, EventArgs e)
        {
            this.Show();
        }
     
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (cmbCat.SelectedItem == null)
            {
                MessageBox.Show("please select a category!", "error");
                return;
            }
  
            fillDt();
            timer1.Enabled = true;
            timer1.Interval = (int)(numInterval.Value) * 1000;
            btnpouse.Text = "pouse";
            btnStart.Enabled = false;
           
         }
        DataTable dt = new DataTable();
        void fillDt()
        {
            dt.Clear();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM " + cmbCat.Text, con);
            
            da.Fill(dt);
            index = 0;
        }
        int index = 0;
        public List<Form1> forms = new List<Form1>();
        public int left = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (forms.Count <= 5)
            {
                if (!(index < dt.Rows.Count))
                { index = 0; }
                    byte[] buffer = (byte[])dt.Rows[index]["image"];
                    string word = dt.Rows[index]["word"].ToString();
                    MemoryStream ms = new MemoryStream(buffer);
                    Form1 m = new Form1(Image.FromStream(ms), word, (int)(numericUpDown1.Value), this);
                    m.Location = new Point(left, (forms.Count * 120) + 10);
                    m.Show();
                    forms.Add(m);
            
                   index++;
                   
                
            }
         
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {

                fillDt();
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btnStart.Enabled = true;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                timer1.Interval = (int)(numInterval.Value) * 1000;
                timer1.Enabled = true;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (btnpouse.Text.Equals("pouse"))
            {
                btnpouse.Text = "resume";
                if (timer1.Enabled)
                timer1.Enabled = false;
            }
            else if (btnpouse.Text.Equals("resume"))
            {
                btnpouse.Text = "pouse";
                if (index == 0)
                {
                    button1_Click_1(this, new EventArgs());

                }
                else
                {
                    if (!timer1.Enabled)
                        timer1.Enabled = true;
                }
                  
            }
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_notifyicon.Dispose();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
         //OleDbConnection con;
         //   con = new OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=db.mdb;");
         //   con.Open();
         //   string[] restrictions = new string[4];
         //   restrictions[3] = "Table";
         //   DataTable userTables = null;
         //   userTables = con.GetSchema("Tables", restrictions);
         //   for (int i = 0; i < userTables.Rows.Count; i++)
         //   {

         //       //richTextBox1.AppendText(userTables.Rows[i][2].ToString()+"\n");
         //   }