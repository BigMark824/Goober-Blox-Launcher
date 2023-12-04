using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        RegisterProtocol();

        if (args.Length > 0)
        {
            (string username, int placeid, string ip, int port) = GetParametersFromUrl(args[0]);
            LaunchGame(username, placeid, ip, port);
        }
        else
        {
            Console.WriteLine("No arguments provided.");
        }
    }

    static (string username, int placeid, string ip, int port) GetParametersFromUrl(string url)
    {
        const string usernamePattern = "&username=(?<username>[^&]+)";
        const string placeidPattern = "&placeid=(?<placeid>[^&/]+)";
        const string ipPattern = "&ip=(?<ip>[^&/]+)";
        const string portPattern = "&port=(?<port>[^&/]+)";

        Match usernameMatch = Regex.Match(url, usernamePattern);
        Match placeidMatch = Regex.Match(url, placeidPattern);
        Match ipMatch = Regex.Match(url, ipPattern);
        Match portMatch = Regex.Match(url, portPattern);

        string username = usernameMatch.Success ? usernameMatch.Groups["username"].Value : string.Empty;
        int placeid = placeidMatch.Success && int.TryParse(placeidMatch.Groups["placeid"].Value, out int parsedPlaceid) ? parsedPlaceid : 0;
        string ip = ipMatch.Success ? ipMatch.Groups["ip"].Value : string.Empty;
        int port = portMatch.Success && int.TryParse(portMatch.Groups["port"].Value, out int parsedPort) ? parsedPort : 0;

        return (username, placeid, ip, port);
    }

    static bool TryParseInt(string input, int startIndex, int length, out int result)
    {
        string substring = input.Substring(startIndex, length);
        return int.TryParse(substring, out result);
    }

    static void LaunchGame(string username, int placeid, string ip, int port)
    {
        Console.WriteLine($"Launching game for username: {username}, placeid: {placeid}, ip: {ip}, port: {port}");

        string gameExecutablePath = @"C:\2015\RobloxPlayerBeta.exe";
        string gameArguments = $"-a\"http://localhost/login/negotiate.ashx\" -j\"http://localhost/game/placelaunchrrr.php/?placeid={placeid}&ip={ip}&port={port}&id=0&app=0&user={username}\" -t \"1\\";

        Process.Start(gameExecutablePath, gameArguments);
        Console.ReadLine();
    }

    static void RegisterProtocol()
    {
        try
        {
            string protocolName = "goober-player";
            string executablePath = Process.GetCurrentProcess().MainModule.FileName;

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(protocolName))
            {
                key.SetValue(string.Empty, $"URL:{protocolName} Protocol");
                key.SetValue("URL Protocol", string.Empty);

                using (RegistryKey commandKey = key.CreateSubKey(@"shell\open\command"))
                {
                    commandKey.SetValue(string.Empty, $"\"{executablePath}\" \"%1\"");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to register protocol: {ex.Message}");
        }
    }
}