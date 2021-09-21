using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database2
{
    public partial class LoginForm : Form
    {
        public static string profileName ="";

        public LoginForm()
        {
            InitializeComponent();
            
        }
        SqlConnection con = new SqlConnection();
        SqlCommand comm;
        string sql;
        private void LoginForm_Load(object sender, EventArgs e)
        {
            

            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";
            con.Open();

            //DataSet ds = new DataSet();
            //sAdap.Fill(ds, "Info");
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btnCreat_Click(object sender, EventArgs e)
        {
            // string sql = "select * from LoginInfo where UserName= '" + tbxUser.Text + "'";
            //SqlDataAdapter sAdap = new SqlDataAdapter(sql, con);
            //SqlCommand comm = new SqlCommand(sql, con);
            //SqlDataReader reader = comm.ExecuteReader();
            // if (!reader.Read())
            if (!string.IsNullOrWhiteSpace(tbxUser.Text) && !string.IsNullOrWhiteSpace(tbxPass.Text))
            {
                sql = "select * from LoginInfo2 where UserName= '" + tbxUser.Text + "'";
                comm = new SqlCommand(sql, con);
                SqlDataReader reader = comm.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();


                    var output = con.Query<LoginData>("select * from LoginInfo2 where  UserName= '" + tbxUser.Text + "'").ToList();
                    List<LoginData> logdta = new List<LoginData>();
                    logdta = output;
                    if (logdta[0].Password == tbxPass.Text)
                    {
                        profileName = tbxUser.Text;
                        SuccessLogin();
                    }
                    else
                    { MessageBox.Show("This username is already taken. Choose another one... "); }
                }
                else
                {
                    reader.Close();
                    sql = "insert into LoginInfo2 values(@UserName,@Password)";
                    comm = new SqlCommand(sql, con);
                    comm.Parameters.AddWithValue("UserName", tbxUser.Text);
                    comm.Parameters.AddWithValue("Password", tbxPass.Text);
                    comm.ExecuteNonQuery();

                    profileName = tbxUser.Text;
                    SuccessLogin();
                    //MessageBox.Show("Your Account is created . Please login now.");
                }
                //this.Close();
            }
            else
            {
                MessageBox.Show("Come on! Fill all the boxes!");
            }
        }
        public void SuccessLogin()
        {
            var profile = new profile();
            profile.ShowDialog();
            
            
        }
        public string GetUserName()
        {
            return profileName;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tbxUser.Text);
            tbxUser.Clear();
            tbxPass.Clear();
        }

        private void tbxPass_TextChanged(object sender, EventArgs e)
        {
            if (tbxPass.TextLength >= 8)
            {

                Bitmap img = new Bitmap(@"C:\Users\114\source\repos\database2\database2\Resources\icons8-emoji-64.png");
                picBox.Image = img;
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (tbxPass.TextLength < 8 && tbxPass.TextLength > 0)
            {
                Bitmap img = new Bitmap(@"C:\Users\114\source\repos\database2\database2\Resources\icons8-emoji-64 (1).png");

                picBox.Image = img;
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }

    public class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
