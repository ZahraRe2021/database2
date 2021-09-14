using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
         var  LoginForm = new LoginForm();
            LoginForm.ShowDialog();

           

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            DatabasePictures.savetodatabase();
            
        }
    }
}
