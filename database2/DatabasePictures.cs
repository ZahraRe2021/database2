using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database2
{
    public class DatabasePictures
    {
        private static Image img;

        public static byte[] imgToArray()
        {
            // string path = @"C:\Users\114\Pictures\M100.JPG";
            //string path = @"C:\Users\114\Pictures\MH800+II.PNG";
            string path = @"C:\Users\114\Pictures\HpLaserMFP.jfif";
            Bitmap img = new Bitmap(path);
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                byte[] pic_array = ms.ToArray();
                return pic_array;
            }
        }
        public static string imgTobase64()
        {
            byte[] pic_array = imgToArray();
            string base64String = Convert.ToBase64String(pic_array);
            return base64String;
        }
        public static void savetodatabase()
        {
            string base64String = imgTobase64();

            // MessageBox.Show("Done");
            using (IDbConnection cnn = new SQLiteConnection(AccessData.connString("ProductsDb")))
            {
                //cnn.Execute("UPDATE Products SET Pic = @_array Where Laminator= 'M100' ", new { _array = pic_array });
                //cnn.Execute("UPDATE Products SET Pic = @_array Where Printer= 'MH1100+' ", new { _array = pic_array });
                //cnn.Execute("UPDATE Products SET Pic = @_array Where Printer= 'M220' ", new { _array = pic_array });
                //cnn.Execute("UPDATE Products SET Pic = @_array Where laser= 'LT2D3D' ", new { _array = pic_array });
                //cnn.Execute("UPDATE Products SET Pic = @_array Where Printer= 'MH800+ II' ", new { _array = pic_array });
                cnn.Execute("INSERT INTO Products(Pic) VALUES( @_String) ", new { _String = base64String });
            }
        }
       //
       
        public static List<ProductImages> LoadPics()
        {
            using (IDbConnection cnn = new SQLiteConnection(AccessData.connString("ProductsDb")))
            {
                int num = cnn.ExecuteScalar<int>("select count(Id) from Products");
               // MessageBox.Show(num.ToString());

                var output = cnn.Query<ProductImages>("select * from Products Where Id BETWEEN 1 AND (@_num)",new { _num = num} ).ToList();
                //var output = cnn.Query<ProductImages>($"select * from Products Where Id BETWEEN 1 AND {num})" ).ToList();
                   // MessageBox.Show(output.Capacity.ToString());

                return output;

            }
        }


    }
}
