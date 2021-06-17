/*Project - Phone Book in C# - InfoBrother*/

using System;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace PhoneBook
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Code For "New" Button:
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = null;
                panel1.Enabled = true;
                //Add a New Row:
                App.PhoneBook.AddPhoneBookRow(App.PhoneBook.NewPhoneBookRow());
                phoneBookBindingSource.MoveLast();
                txtName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                App.PhoneBook.RejectChanges();
            }
        }

        //Code for "Edit" button:
        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txtPhone.Focus();
        }


        //Code for "Cancel"  Button:
        private void btnCancel_Click(object sender, EventArgs e)
        {
            phoneBookBindingSource.ResetBindings(false);
            panel1.Enabled = false;
        }

        //Code for "Save"  button:
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //End Edit, Save Data To file:
                phoneBookBindingSource.EndEdit();
                App.PhoneBook.AcceptChanges();
                App.PhoneBook.WriteXml(string.Format("{0}//data.dat", Application.StartupPath));
                panel1.Enabled = false;
                if (Directory.Exists("Images"))
                {
                    pictureBox1.Image.Save($"{Application.StartupPath}\\Images\\{txtPhone.Text}.jpg");
                }
                else
                {
                    Directory.CreateDirectory($"{Application.StartupPath}\\Images");
                    pictureBox1.Image.Save($"{Application.StartupPath}\\Images\\{txtPhone.Text}.jpg");
                }

                MessageBox.Show("Number Store Successfully:");
                pictureBox1.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Use Singleton Pattern to Create an Instance.
        static PhoneData db;
        protected static PhoneData App
        {
            get
            {
                if (db == null)
                {
                    db = new PhoneData();
                }
                return db;
            }
        }


        //Code for "Form".
        private void Form1_Load(object sender, EventArgs e)
        {
            string filename = string.Format("{0}//data.dat", Application.StartupPath);
            if (File.Exists(filename))
            {
                App.PhoneBook.ReadXml(filename);
            }
            phoneBookBindingSource.DataSource = App.PhoneBook;
            panel1.Enabled = false;
            Stream stream = Properties.Resources.Help;
            SoundPlayer player = new SoundPlayer(stream);
            player.Play();
        }


        //Code for "DataGridView". 
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        //code for "Search box";
        private void txtSearch_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) //enter Key:
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {

                    //we can use linq to Query data:
                    var query = from o in App.PhoneBook
                                where o.PhoneNo == txtSearch.Text 
                                ||    o.FullName.ToLowerInvariant().Contains(txtSearch.Text.ToLowerInvariant())
                                ||    o.Email.ToLowerInvariant() == txtSearch.Text.ToLowerInvariant()
                                select o;
                    dataGridView1.DataSource = query.ToList();
                }
                else
                {
                    dataGridView1.DataSource = phoneBookBindingSource;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure that you want to Delete this Record?", "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (File.Exists($"{Application.StartupPath}\\Images\\" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + ".jpg"))
                {
                    File.Delete($"{Application.StartupPath}\\Images\\" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + ".jpg");
                }
                phoneBookBindingSource.RemoveCurrent();
                phoneBookBindingSource.EndEdit();
                App.PhoneBook.AcceptChanges();
                App.PhoneBook.WriteXml(string.Format("{0}//data.dat", Application.StartupPath));
                pictureBox1.Image = null;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure that you want to Delete this Record?", "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete("data.dat");
                DirectoryInfo del = new DirectoryInfo("Images");
                foreach(FileInfo file in del.GetFiles())
                {
                    file.Delete();
                }
                Directory.Delete("Images");
                Application.Restart();
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Contact contact = new Contact(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), dataGridView1.CurrentRow.Cells[3].Value.ToString());
            FullInfo info = new FullInfo(contact.name, contact.phone, contact.email, contact.adress);
            info.Show();
        }

        private void BtnAboutUS_Click(object sender, EventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
        }
    }
}
