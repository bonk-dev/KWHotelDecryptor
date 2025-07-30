using System.Text;

namespace KWHotelDecryptor
{
    public class KwHotelConfig
    {
        public string DatabaseServerType { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public bool EnableSsl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        
        public KwHotelConfig(
            string databaseServerType, 
            string host, 
            string port, 
            bool enableSsl, 
            string user, 
            string password, 
            string databaseName)
        {
            DatabaseServerType = databaseServerType;
            Host = host;
            Port = port;
            EnableSsl = enableSsl;
            User = user;
            Password = password;
            DatabaseName = databaseName;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Serwer baz danych: {0}\n", DatabaseServerType);
            builder.AppendFormat("Adres serwera: {0}\n", Host);
            builder.AppendFormat("Port serwera: {0}\n", Port);
            builder.AppendFormat("Użytkownik: {0}\n", User);
            builder.AppendFormat("Hasło: {0}\n", Password);
            builder.AppendFormat("Baza danych: {0}\n", DatabaseName);
            builder.AppendFormat("SSL: {0}", EnableSsl ? "włączone": "wyłączone");
            return builder.ToString();
        }
    }
}