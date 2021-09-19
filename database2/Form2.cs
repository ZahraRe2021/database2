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

        List<GroupBox> gbList = new List<GroupBox>();

        List<ProductImages> output = DatabasePictures.LoadPics();

        private void Form2_Load(object sender, EventArgs e)
        {
            flowLayoutPanel.Controls.Clear();
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.Dock = DockStyle.Fill;

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = DockStyle.Fill;



            //    if ((output[i].Pic).GetType() == (typeof(byte[])))
            //    {
            //        imgbyte = pI.Pic;
            //    }
            //    // else if ((output[i].Pic).GetType() == (typeof(string)))
            //    if (output[i].Id == 6)
            //    {
            //        MessageBox.Show("y");
            //        // imgbyte = Convert.FromBase64String(output[i].Pic);
            //    }

            //    using (MemoryStream ms = new MemoryStream(imgbyte))
            //    {
            //        Image img = Image.FromStream(ms);
            //        pic.Image = img;
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

            var selected = flowLayoutPanel.Controls.OfType<CheckBox>().Where(x => x.Checked);
            if (selected != null)
            {
                Debug.Print("Selected images: {0}", selected.Count());
                foreach (var item in selected)
                {
                    //Save the picture from item.BackgroundImage.
                    string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";

                    //byte[] imbyt = null;
                    using (IDbConnection con = new SqlConnection(sql))
                    {
                        List<UserSelecedItems> userItems = new List<UserSelecedItems>();

                        //    using(MemoryStream ms = new MemoryStream())
                        //{
                        //    item.Image.Save(ms, item.Image.RawFormat);
                        //      imbyt = ms.ToArray();
                        //}
                        userItems.Add(new UserSelecedItems { Id = j, Item = item.Text });
                        j++;
                        con.Execute("insert into UserItem(Id, Item) values(@Id,  @Item)", userItems);

                        LoadFromUserItemTable();
                    }

                }
                MessageBox.Show("Done!");
            }
            else { MessageBox.Show("Nothing to save!"); }
        }
        public void LoadFromUserItemTable()
        {
            flowLayoutPanel1.Controls.Clear();
            string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";
            using (IDbConnection con = new SqlConnection(sql))
            {
                UserSelecedItems userItemLoad = new UserSelecedItems();
                
                var outputLoad = con.Query<UserSelecedItems>("select * from UserItem", new DynamicParameters()).ToList();
                for (int i = 0; i < outputLoad.Count ; i++)
                {
                    CheckBox cb = new CheckBox();
                    cb.Appearance = Appearance.Button;
                    cb.Size = new Size(imageWidth, imageHeight);
                     cb.BackgroundImageLayout = ImageLayout.Zoom;
                    userItemLoad = outputLoad[i];
                    cb.Text = userItemLoad.Item;
                    flowLayoutPanel1.Controls.Add(cb);
                }
            }
        }
    }

    class UserSelecedItems
    {
        public byte[] im { get; set; }
        //public string Name { get; set; }
        public string Item { get; set; }
        public int Id { get; set; }
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



