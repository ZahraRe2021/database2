using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace database2
{
    public partial class profile : Form
    {
        public profile()
        {
            InitializeComponent();
        }

        private void profile_Load(object sender, EventArgs e)
        {
            //string base64EncodedImage = null;
        }

        private void SelectedProductsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
           Form Form2 = new Form2();
            openChildForm( Form2);
           // UserSelecedItems.
           // SelectedProductsList.DataSource = Form2.userItems;
        }


        private void btnSearch_Click_2(object sender, EventArgs e)
        {
            string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";

            using (IDbConnection con = new SqlConnection(sql))
            {
                List<UserSelecedItems> userItems = new List<UserSelecedItems>();
                
               // MessageBox.Show("f");
               userItems =  con.Query<UserSelecedItems>("select * From UserItem", new DynamicParameters()).ToList();
                SelectedProductsList.DataSource = userItems;
                SelectedProductsList.DisplayMember = userItems[0].Item;
            }
        }
       
    }
}
