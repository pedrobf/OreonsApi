namespace OreonsApi.core.Models.DataBase
{
    public class ConnectionMySql
    {
        public static string ConnectionString = $"server=localhost;database=oreons;user id=root;password=root;";

        // Esta seria a forma correta utilizando variaveis de ambiente para proteger nosso DB
        //$"server={Environment.GetEnvironmentVariable("OREONS_HOST")};port={Environment.GetEnvironmentVariable("OREONS_PORT")};database={Environment.GetEnvironmentVariable("OREONS_DATABASE")};user id={Environment.GetEnvironmentVariable("OREONS_USER")};password={Environment.GetEnvironmentVariable("OREONS_PASS")};sslmode=none";
    }
}
