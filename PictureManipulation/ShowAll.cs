using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureManipulation
{
    public partial class ShowAll : Form
    {
        private List<Picture> Pictures => Picture.GetPictures();

        public ShowAll()
        {
            InitializeComponent();
        }

        private void ShowAll_Load(object sender, EventArgs e)
        {

        }

        private void GetPictures()
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picture picture = Pictures.Find(item => item.FileName.Equals(comboBox1.SelectedItem.ToString()));
            textBox1.Text = picture.FileName;
            pictureBox1.Image = picture.Pic;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (Picture picture in Pictures)
            {
                comboBox1.Items.Add(picture.FileName);
            }
        }
    }
}
