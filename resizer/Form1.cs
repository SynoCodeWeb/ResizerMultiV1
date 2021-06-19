using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace resizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string[] files;
        public static string exportDir;
        private void button2_Click(object sender, EventArgs e)
        {
            pbLoaded.Increment(-100);
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    exportDir = fbd.SelectedPath;
                    System.Windows.Forms.MessageBox.Show("Dir found: " + exportDir, "Message");
                }
            }
                              //1920 //1080  
            ResizeImage(0, 0, 256, 256);
        }
        public void ResizeImage(int X1, int Y1, int width, int height)
        {
            for (int i = 0; i < files.Length; i++)
            {
                string fileSaveName = exportDir + "\\" + Path.GetFileName(files[i]);
                lblProgress.Text = files[i];
                pbLoaded.Increment(100/files.Length);
                using (Image image = Image.FromFile(files[i]))
                {
                    new Bitmap(image, width, height).Save(fileSaveName);
                }
            }
            pbLoaded.Increment(100);
            System.Windows.Forms.MessageBox.Show("Done!", "Message");

        }

        private void btnCarica_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    files = Directory.GetFiles(fbd.SelectedPath);
                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
                button2.Enabled = true;
            }
        }
    }
}
