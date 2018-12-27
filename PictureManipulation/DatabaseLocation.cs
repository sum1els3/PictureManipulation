namespace PictureManipulation
{
    /// <summary>
    /// Location of database
    /// </summary>
    public static class DatabaseLocation
    {
        //Localhost (MSSQL) "Server=localhost\\SQLEXPRESS;Database={0};Trusted_Connection=True;"
        //Network (MSSQL) "data source={0},{1}; database={2};user id={3};password={4};"
        /// <summary>
        /// Database's IP
        /// </summary>
        public static string DatabaseIP { get; set; }
        /// <summary>
        /// Database's Port
        /// </summary>
        public static string DatabasePort { get; set; }
        /// <summary>
        /// Database's Name
        /// </summary>
        public static string DatabaseName { get; set; }
        /// <summary>
        /// Database's Username for authentication
        /// </summary>
        public static string DatabaseUsername { get; set; }
        /// <summary>
        /// Database's Username for authentication
        /// </summary>
        public static string DatabasePassword { get; set; }


        /// <summary>
        /// Database's connection string
        /// </summary>
        public static string DatabaseConnection
        {
            get
            {
                return string.Format("data source={0},{1}; database={2};user id={3};password={4}",
                    DatabaseIP,
                    DatabasePort,
                    DatabaseName,
                    DatabaseUsername,
                    DatabasePassword
                );
            }
        }
    }
}
