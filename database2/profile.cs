using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        
        byte[] imgbyte;

        List<GroupBox> gbList = new List<GroupBox>();

        List<ProductImages> output = DatabasePictures.LoadPics();
        private void btnSearch_Click(object sender, EventArgs e)
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

            int x=0, y=0;
            int j = 0;
            for (int i = 0; i < output.Count - 1; i++)
            {
                PictureBox pic = new PictureBox();
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                GroupBox gb = new GroupBox();
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
            }

        }

        //
    }
}
