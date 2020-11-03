namespace Home.Shared.DAL.Helpers
{
    public class PostgresConnectionHelper
    {
        public static string ParseConnectionURL(string url)
        {
            string connectionString = "";

            url = url.Replace("postgres://", string.Empty);
            var pgUserPass = url.Split("@")[0];
            var pgHostPortDb = url.Split("@")[1];
            var pgHostPort = pgHostPortDb.Split("/")[0];
            var pgDb = pgHostPortDb.Split("/")[1];
            var pgUser = pgUserPass.Split(":")[0];
            var pgPass = pgUserPass.Split(":")[1];
            var pgHost = pgHostPort.Split(":")[0];
            var pgPort = pgHostPort.Split(":")[1];

            connectionString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;Trust Server Certificate=true";

            return connectionString;
        }
    }
}
