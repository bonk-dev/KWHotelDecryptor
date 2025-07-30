namespace KWHotelDecryptor
{
    public static class KajwareConfigParser
    {
        public static KwHotelConfig Parse(string plaintextConfig)
        {
            var lines = plaintextConfig.Trim().Split('\n');
            var hostPort = lines[1].Split(',');
            return new KwHotelConfig(
                lines[0],
                hostPort[0],
                hostPort.Length > 1 ? hostPort[1] : "brak (domyślny)",
                bool.Parse(lines[2]),
                lines[3],
                lines[4],
                lines[5]);
        }
    }
}