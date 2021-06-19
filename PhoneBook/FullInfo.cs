using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PhoneBook
{
    public partial class FullInfo : Form
    {

        public FullInfo()
        {
            InitializeComponent();
        }
        private void Clearlabel()
        {
            
        }
        public FullInfo(string n,string p,string e,string a)
        {
            InitializeComponent();
            lblName.Text = p.ToString();
            lblPhone.Text = n.ToString();
            lblEmail.Text = e.ToString();
            lblAddress.Text = a.ToString();
            if (File.Exists($"{Application.StartupPath}\\Images\\{n}.jpg"))
            {
                pictureBox1.Image = Image.FromFile($"{Application.StartupPath}\\Images\\{n}.jpg");
            }
            else
            {
                pictureBox1.Image = Properties.Resources.Copy_of_no_pictures_sign_flyer_template___Made_with_PosterMyWall;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            this.Close();
        }
    }
}
