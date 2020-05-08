using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace pictureWords
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );
        main mm;
        public Form1(Image img,string word,int Duration,main m)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 14, 14));
            pictureBox1.Image = img;
            label1.Text = word;
            timer1.Enabled = true;
            timer1.Interval = Duration * 1000;
            mm = m;
            this.TopMost = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
     
        }




       
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isMouseEneter)
            {
                timer1.Enabled = false;
                timer1.Interval = 5000;
                timer1.Enabled = true;
            }
            else
            {
                this.Close();
            }
           
      
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mm.forms.Remove(this);
            for (int x = 0; x < mm.forms.Count;x++ )
            {
                mm.forms[x].Location = new Point(mm.left, (x * 120)+10);
            }
 
        }
        bool isMouseEneter = false;
            
        private void elContainer1_MouseEnter(object sender, EventArgs e)
        {
            isMouseEneter = true;
        }

        private void elContainer1_MouseLeave(object sender, EventArgs e)
        {
            isMouseEneter = false;
  
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 

        private void elContainer1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                timer1.Enabled = false;
               
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            timer1.Interval = 3000;
            timer1.Enabled = true;
        }
    }
}
