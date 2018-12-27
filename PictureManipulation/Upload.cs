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
    public partial class Upload : Form
    {
        private string NetworkLocation = "\\Share\\";

        public Upload()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Upload_Load(object sender, EventArgs e)
        {
            DatabaseLocation.DatabaseIP = "192.168.43.39";
            DatabaseLocation.DatabasePort = "1433";
            DatabaseLocation.DatabaseName = "PictureManipulation";
            DatabaseLocation.DatabaseUsername = "user0";
            DatabaseLocation.DatabasePassword = "user0";
        }

        /// <summary>
        /// Executes when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string location = GetPictureLocation();
            if (!string.IsNullOrEmpty(location))
            {
                pictureBox1.Image = new Bitmap(location);
                MessageBox.Show("Complete!");
            }
        }

        /// <summary>
        /// Get the image's location from a opendialogbox,
        /// inserts it in the database then returns the new location
        /// </summary>
        /// <returns></returns>
        private string GetPictureLocation()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                InsertIntoDatabase(string.Format("{0}{1}", NetworkLocation, openFileDialog.SafeFileName), openFileDialog.SafeFileName.Substring(0, openFileDialog.SafeFileName.LastIndexOf(".")));
                return NewLocation(openFileDialog.FileName, openFileDialog.SafeFileName);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Copies the image to the server's location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string NewLocation(string location, string fileName)
        {
            string newLocation = string.Format("\\\\{0}{1}{2}", DatabaseLocation.DatabaseIP, NetworkLocation, fileName);
            if (System.IO.File.Exists(location) == true)
            {
                System.IO.File.Copy(location, newLocation);
            }
            return newLocation;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowAll showAll = new ShowAll();
            showAll.Show();
        }

        /// <summary>
        /// Creates a new instance then inserts it in the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="name"></param>
        private void InsertIntoDatabase(string location, string name)
        {
            new Picture(name, location).InsertIntoDatabase();
        }
    }
}
