using System;
using System.IO;
using System.Security;
using System.Text;

namespace KWHotelDecryptor
{
    internal class Program
    {
        private static void PrintNotFound()
        {
            Console.WriteLine("[-] Nie znaleziono żadnych instalacji KWHotel.");
            Console.WriteLine("[?] Musisz przeciągnąć plik connect.config na ten program, lub " +
                              "podać ścieżkę do niego jako argument linii poleceń:");
            Console.WriteLine("[?] KWHotelDecryptor.exe \"C:\\KajWare\\KWHotel Pro\\connect.config\"");

            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("Naciśnij dowolny klawisz aby kontynuować...");
            Console.ReadKey(true);
        }
        
        private static void Decrypt(string filePath)
        {
            using (var crypto = new KajwareCrypto(
                       Encoding.UTF8.GetBytes(Consts.ConfigKey.Substring(0, 8)),
                       Consts.ConfigIv))
            {
                try
                {
                    var configFile = File.ReadAllText(filePath).Trim();
                    var configFileBytes = Convert.FromBase64String(configFile);
                    var plaintextBytes = crypto.Decrypt(configFileBytes);
                    var plaintext = Encoding.UTF8.GetString(plaintextBytes);

                    var cfg = KajwareConfigParser.Parse(plaintext);
                    Console.WriteLine(cfg);
                }
                catch (SecurityException securityException)
                {
                    Console.WriteLine($"[-] Odmowa dostępu: {securityException.Message}");
                }
                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    Console.WriteLine($"[-] Odmowa dostępu: {unauthorizedAccessException.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[-] Nie udało się odszyfrować pliku: {ex.Message}");
                }
            }
        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("KWHotelDecryptor v1.0.0 - Dawid Pągowski\n");
            
            string[] paths;
            if (args.Length < 1)
            {
                Console.WriteLine("[?] Brak podanej ścieżki. Szukanie instalacji KWHotel...");
                var sysRoot = Environment.GetEnvironmentVariable("SystemRoot") ?? "C:\\Windows";
                var root = Path.GetDirectoryName(sysRoot) ?? "C:\\";
                var kajwareDir = Path.Combine(root, "Kajware");

                if (!Directory.Exists(kajwareDir))
                {
                    PrintNotFound();
                    return;
                }
                
                var dirs = Directory.GetDirectories(kajwareDir);
                if (dirs.Length <= 0)
                {
                    PrintNotFound();
                    return;
                }

                const string connectConfigFilename = "connect.config";
                paths = new string[dirs.Length];
                for (var i = 0; i < dirs.Length; ++i)
                {
                    paths[i] = Path.Combine(dirs[i], connectConfigFilename);
                }
            }
            else
            {
                paths = args;
            }

            foreach (var path in paths)
            {
                Console.WriteLine($"[?] Przeszukiwanie {Path.GetDirectoryName(path)}");
                
                if (!File.Exists(path))
                {
                    Console.WriteLine($"[-] Plik {path} nie istnieje!");
                    continue;
                }

                Console.WriteLine($"[+] Znaleziono plik {path}");
                Console.WriteLine($"[+] Deszyfrowanie pliku {path}");
                Decrypt(path);
            }
            
            Pause();
        }
    }
}