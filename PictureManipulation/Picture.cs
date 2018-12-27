using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;

namespace PictureManipulation
{
    class Picture
    {
        public Picture(string FileName, string FileLocation)
        {
            AddPicture(FileName, FileLocation);
        }

        private Picture(string PrimaryKey, string FileName, string FileLocation)
        {
            pictureID = PrimaryKey;
            AddPicture(FileName, FileLocation);
        }

        private void AddPicture(string FileName, string FileLocation)
        {
            fileName = FileName;
            fileLocation = FileLocation;
        }

        public string FileName => fileName;
        public Image Pic => new Bitmap(string.Format("\\\\{0}\\{1}", DatabaseLocation.DatabaseIP, fileLocation));

        private string pictureID;
        private string fileName;
        private string fileLocation;

        public void InsertIntoDatabase()
        {
            if (string.IsNullOrEmpty(pictureID))
            {
                using (SqlConnection con = new SqlConnection(DatabaseLocation.DatabaseConnection))
                {
                    SqlCommand command = new SqlCommand("INSERT INTO picture VALUES (@primaryKey, @pictureName, @pictureLocation)", con);
                    command.Parameters.Add("@primaryKey", SqlDbType.VarChar).Value = CustomPrimaryKey.GenerateCustomPrimaryKey("picture");
                    command.Parameters.Add("@pictureName", SqlDbType.VarChar).Value = fileName;
                    command.Parameters.Add("@pictureLocation", SqlDbType.VarChar).Value = fileLocation;
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public static List<Picture> GetPictures()
        {
            List<Picture> pictures = new List<Picture>();
            using (SqlConnection con = new SqlConnection(DatabaseLocation.DatabaseConnection))
            {
                SqlCommand command = new SqlCommand("SELECT pictureID, pictureName, pictureLocation FROM picture", con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pictures.Add(
                        new Picture(reader["pictureID"].ToString(), reader["pictureName"].ToString(), reader["pictureLocation"].ToString())
                    );
                }
                con.Close();
            }
            return pictures;
        }
    }
}
