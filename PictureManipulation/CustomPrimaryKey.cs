using System;
using System.Data;
using System.Data.SqlClient;

namespace PictureManipulation
{
    /// <summary>
    /// Generates a custom primary key
    /// </summary>
    public static class CustomPrimaryKey
    {
        /// <summary>
        ///  Generates a 15 characters primary key
        /// </summary>
        /// <returns></returns>
        public static string GenerateCustomPrimaryKey()
        {
            return CreateCustomPrimaryKey(new Random());
        }

        /// <summary>
        /// Generates a 15 characters new primary key for a specific table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        /// <throws>NullException</throws>
        public static string GenerateCustomPrimaryKey(string tableName)
        {
            string custom_primary_key;
            do
            {
                custom_primary_key = CreateCustomPrimaryKey(new Random());
            } while (custom_primary_key == VerifyIfUnique(custom_primary_key, tableName));
            return custom_primary_key;
        }

        /// <summary>
        /// Verify primary key from a specific table to avoid constraint.
        /// </summary>
        /// <param name="primary_key"></param>
        /// <param name="table_name"></param>
        private static string VerifyIfUnique(string primary_key, string table_name)
        {
            string return_primary_key = "";
            using (SqlConnection con = new SqlConnection(DatabaseLocation.DatabaseConnection))
            {
                con.Open();
                SqlCommand command = new SqlCommand(string.Format("SELECT {0}ID FROM {0} WHERE {0}ID = @primary_key", table_name), con);
                command.Parameters.Add("@primary_key", SqlDbType.VarChar).Value = primary_key;
                try
                {
                    return_primary_key = command.ExecuteScalar().ToString();
                }
                catch (NullReferenceException)
                {
                    con.Close();
                    return "";
                }
                con.Close();
                return return_primary_key;
            }
        }

        /// <summary>
        /// Creates a new primary key.
        /// </summary>
        /// <returns></returns>
        private static string CreateCustomPrimaryKey(Random random)
        {
            string custom_primary_key = "";
            for (int key_length = 1; key_length <= 15; key_length++)
            {
                custom_primary_key += key_length % 4 == 0 && key_length != 0 ? "-" : GenerateLetterOrNumber(random.Next(1, 4), random);
            }
            return custom_primary_key;
        }

        private static string GenerateLetterOrNumber(int category, Random random)
        {
            return category == 1 ? ((char)random.Next('0', '9')).ToString() : category == 2 ? ((char)random.Next('A', 'Z')).ToString() : category == 3 ? ((char)random.Next('a', 'z')).ToString() : "";
        }
    }
}
