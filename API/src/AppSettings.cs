namespace EmployeeCRUDAPI
{
    public static class AppSettings
    {
        private static IConfiguration _config;

        public static void Initialize(IConfiguration config)
        {
            _config = config;
        }

        public static string ConnectionString
        {
            get
            {
                return _config.GetConnectionString("DefaultConnection");
            }
        }
    }
}
