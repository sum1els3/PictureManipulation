﻿using System;
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
    public partial class Form1 : Form
    {
        private string NetworkLocation = "\\\\192.168.43.39\\Share";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string location = GetPictureLocation();
            if (!string.IsNullOrEmpty(location))
            {
                pictureBox1.Image = new Bitmap(location);
                MessageBox.Show("Complete!");
            }
        }

        private string GetPictureLocation()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                return NewLocation(openFileDialog.FileName, openFileDialog.SafeFileName);
            }
            else
            {
                return string.Empty;
            }
        }

        private string NewLocation(string location, string fileName)
        {
            string newLocation = string.Format("{0}\\{1}", NetworkLocation, fileName);
            if (System.IO.File.Exists(location) == true)
            {
                System.IO.File.Copy(location, newLocation);
            }
            return newLocation;
        }
    }
}
