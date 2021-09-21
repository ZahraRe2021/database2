using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database2
{
    public partial class Form2 : Form
    {


        public Form2()
        {
            InitializeComponent();
        }

        byte[] imgbyte;

        List<ProductImages> output = DatabasePictures.LoadPics();

        private void Form2_Load(object sender, EventArgs e)
        {
            flowLayoutPanel.Controls.Clear();
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.Dock = DockStyle.Fill;

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = DockStyle.Fill;

            LoadFromUserItemTable();
            label1.Focus();
            label1.Visible = false;

            //    if ((output[i].Pic).GetType() == (typeof(byte[])))
            //    {
            //        imgbyte = pI.Pic;
            //    }
            //    // else if ((output[i].Pic).GetType() == (typeof(string)))
            //    if (output[i].Id == 6)
            //    {
            //        // imgbyte = Convert.FromBase64String(output[i].Pic);
            //    }

            //var result = from s in gbList
            //             where s.Focused == true
            //             //where s.selct()
            //             select s;

            //    foreach (var s in result)
            //{}

        }
        string noNullProperty(int i)
        {
            if (!string.IsNullOrEmpty(output[i].Printer))
            { return output[i].Printer; }
            if (!string.IsNullOrEmpty(output[i].Laser))
            { return output[i].Laser; }
            else { return output[i].Laminator; }
        }
        private const int imageWidth = 128;
        private const int imageHeight = 128;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            for (int i = 0; i < output.Count - 1; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Appearance = Appearance.Button;
                cb.Size = new Size(imageWidth, imageHeight);
                cb.BackgroundImageLayout = ImageLayout.Zoom;

                ProductImages pI = output[i];

                imgbyte = pI.Pic;

                //using (MemoryStream ms = new MemoryStream(imgbyte))
                //{
                //    Image img = Image.FromStream(ms);
                //    cb.BackgroundImage = img;
                //    //}
                //Don't dispose the MemoryStream, the Image class will need it!
                var ms = new MemoryStream(pI.Pic);
                cb.BackgroundImage = Image.FromStream(ms);
                cb.Text = noNullProperty(i);
                flowLayoutPanel.Controls.Add(cb);
            }
        }


        private void btnsave_Click(object sender, EventArgs e)
        {
            int j = 1;
            LoginForm Loginform = new LoginForm();
            

            var selected = flowLayoutPanel.Controls.OfType<CheckBox>().Where(x => x.Checked);

            Debug.Print("Selected images: {0}", selected.Count());
            foreach (var item in selected)
            {
                //Save the picture from item.BackgroundImage.
                string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";

                byte[] imbyt = null;
                using (IDbConnection con = new SqlConnection(sql))
                {
                    //con.Query("select * from LoginInfo2 where Username= ");
                    List<UserSelecedItems> userItems = new List<UserSelecedItems>();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        item.BackgroundImage.Save(ms, item.BackgroundImage.RawFormat);
                        imbyt = ms.ToArray();
                    }

                    userItems.Add(new UserSelecedItems {UserName= LoginForm.profileName, Id = j, Item = item.Text, ImageItem = imbyt });
                    j++;
                    con.Execute("insert into UserItem(UserName, Id, Item, ImageItem) values(@UserName, @Id, @Item, @ImageItem)", userItems);
                }
            }
            
            LoadFromUserItemTable();

            MessageBox.Show("Done!");
            label1.Focus();
        }

        public void LoadFromUserItemTable()
        {
            flowLayoutPanel1.Controls.Clear();
            string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";
            using (IDbConnection con = new SqlConnection(sql))
            {
                string user = LoginForm.profileName;
                var outputLoad = con.Query<UserSelecedItems>("select * from UserItem where UserName= {@User}", new { User = user }).ToList();
                for (int i = 0; i < outputLoad.Count; i++)
                {
                    CheckBox cb = new CheckBox();
                    //cb.Focused = true ;
                    cb.Appearance = Appearance.Button;
                    cb.Size = new Size(imageWidth, imageHeight);
                    cb.BackgroundImageLayout = ImageLayout.Zoom;

                    cb.Text = outputLoad[i].Item;
                    using (MemoryStream ms = new MemoryStream(outputLoad[i].ImageItem))
                    {
                        cb.BackgroundImage = Image.FromStream(ms);
                    }

                    flowLayoutPanel1.Controls.Add(cb);
                }
            }
        }
    }

    class UserSelecedItems
    {
        public byte[] ImageItem { get; set; }
        //public string Name { get; set; }
        public string Item { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
    }


    //class SelectablegroupBox : GroupBox
    //{
    //    public SelectablegroupBox()
    //    {
    //        this.SetStyle(ControlStyles.Selectable, true);
    //        this.TabStop = true;
    //    }

    //    protected override void OnEnter(EventArgs e)
    //    {
    //        //
    //        this.Focus();
    //        this.Invalidate();
    //        base.OnEnter(e);
    //    }

    //    protected override void OnPaint(PaintEventArgs pe)
    //    {
    //        base.OnPaint(pe);
    //        if (this.Focused)
    //        {
    //            var rc = this.ClientRectangle;
    //            rc.Inflate(-2, -2);
    //            ControlPaint.DrawFocusRectangle(pe.Graphics, rc);
    //        }
    //    }
    //}

}



