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
            tabPage1.Controls.Clear();

            string noNullProperty(int i)
            {
                if (!string.IsNullOrEmpty(output[i].Printer))
                { return output[i].Printer; }
                if (!string.IsNullOrEmpty(output[i].Laser))
                { return output[i].Laser; }
                else { return output[i].Laminator; }
            }

            int x = 0, y = 0;
            int j = 0;
            for (int i = 0; i < output.Count - 1; i++)
            {
                SelectablePictureBox pic = new SelectablePictureBox();
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
               // GroupBox gb = new GroupBox();
                SelectablegroupBox gb = new SelectablegroupBox();
                gb.Controls.Add(pic);
                //pic.Location= new Point()
                pic.Dock = DockStyle.Right;
                gb.Text = noNullProperty(i);

                gbList.Add(gb);

                ProductImages pI = output[i];

                if ((output[i].Pic).GetType() == (typeof(byte[])))
                {
                    imgbyte = pI.Pic;
                }
                // else if ((output[i].Pic).GetType() == (typeof(string)))
                if (output[i].Id == 6)
                {
                    MessageBox.Show("y");
                    // imgbyte = Convert.FromBase64String(output[i].Pic);
                }

                using (MemoryStream ms = new MemoryStream(imgbyte))
                {
                    Image img = Image.FromStream(ms);
                    pic.Image = img;
                }

                tabPage1.Controls.Add(gbList[j]);
                gbList[j].Location = new Point(x, y);
                //x += gbList[i].Width;
                y += gbList[i].Height;
                j++;
                //UserSelecedItems userItems = new UserSelecedItems();
            }


            // private void btnSelect_Click_1(object sender, EventArgs e)
            // {
            // for ( j = 0; j < gbList.Count; j++)
            //{
           // MessageBox.Show(gbList.Count.ToString());
            
            var result = from s in gbList
                         where s.Focused == true
                         //where s.selct()
                         select s;

                foreach (var s in result)
            {
                MessageBox.Show(gbList.Count.ToString());
                string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";


                    using (IDbConnection con = new SqlConnection(sql))
                    {
                        List<UserSelecedItems> userItems = new List<UserSelecedItems>();
                        userItems.Add(new UserSelecedItems { Id = 1, Item = s.Text }); ;

                        con.Execute("insert into UserItem(Id, Item) values(@Id,  @Item)", userItems);
                    }
                }
                //    Console.WriteLine(student.Id + ", " + student.Name);
                //if (gbList[j].Focused)
                //{
                //     MessageBox.Show("f");
                //   // MessageBox.Show(gbList.Count.ToString());
                //    string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";


                //    using (IDbConnection con = new SqlConnection(sql))
                //    {//, im = pic.Image
                //        List<UserSelecedItems> userItems = new List<UserSelecedItems>();
                //         userItems.Add(new UserSelecedItems { Id = 1, Item = gbList[j].Text }); ;

                //         con.Execute("insert into UserItem(Id, Item) values(@Id,  @Item)", userItems);
                //    }
                //}
           // }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < gbList.Count; j++)
            {
              
                if (gbList[j].Focused)
                {
                    MessageBox.Show("f");
                    // MessageBox.Show(gbList.Count.ToString());
                    string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";


                    using (IDbConnection con = new SqlConnection(sql))
                    {
                        List<UserSelecedItems> userItems = new List<UserSelecedItems>();
                        userItems.Add(new UserSelecedItems { Id = 1, Item = gbList[j].Text }); ;

                        con.Execute("insert into UserItem(Id, Item) values(@Id,  @Item)", userItems);
                    }
                }
            }
        }
    }
    class UserSelecedItems
    {
        //public Image im { get; set; }
        //public string Name { get; set; }
        public string Item { get; set; }
        public int Id { get; set; }
    }

    class SelectablePictureBox : PictureBox
    {
        public SelectablePictureBox()
        {
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }
        protected override void OnEnter(EventArgs e)
        {
            //
            this.Focus();
            this.Invalidate();
            base.OnEnter(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (this.Focused)
            {
                var rc = this.ClientRectangle;
                rc.Inflate(-2, -2);
                ControlPaint.DrawFocusRectangle(pe.Graphics, rc);
            }
        }
    }

    class SelectablegroupBox : GroupBox
    {
        public SelectablegroupBox()
        {
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }
       
        protected override void OnEnter(EventArgs e)
        {
            //
            this.Focus();
            this.Invalidate();
            base.OnEnter(e);
        }
       
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (this.Focused)
            {
                var rc = this.ClientRectangle;
                rc.Inflate(-2, -2);
                ControlPaint.DrawFocusRectangle(pe.Graphics, rc);
            }
        }
    }
}

    
