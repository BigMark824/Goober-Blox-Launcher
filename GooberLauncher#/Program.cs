using Microsoft.Win32;
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        // Register the protocol when the application is run
        RegisterProtocol();

        // Handle command-line arguments and launch the game
        if (args.Length > 0)
        {
            string url = args[0];
            string username = GetUsernameFromUrl(url);
            LaunchGame(username);
        }
        else
        {
            Console.WriteLine("No arguments provided.");
        }
    }

    static string GetUsernameFromUrl(string url)
    {
        // Parse the URL to extract the username
        // You may want to use a more sophisticated URL parsing method
        // This is just a simple example
        const string usernamePrefix = "username=";
        int index = url.IndexOf(usernamePrefix);
        if (index != -1)
        {
            return url.Substring(index + usernamePrefix.Length);
        }

        // Default to an empty string if the username is not found
        return string.Empty;
    }

    static void LaunchGame(string username)
    {
        // Add your game launch logic here
        // Use the 'username' variable in your game launch parameters
        Console.WriteLine($"Launching game for username: {username}");

        // Here, you would launch your game with the specified arguments
        // Replace the placeholders with your actual game executable path and arguments
        string gameExecutablePath = @"C:\2014M\RobloxPlayerBeta.exe";
        string gameArguments = $"-a\"http://localhost/www.civdefn.tk/\" -j\"http://localhost/www.civdefn.tk/game/join.php?port=2005&username={username}&app=1&ip=127.0.0.1&id=0&mode=0\" -t \"1\\";

        // Start the game process
        Process.Start(gameExecutablePath, gameArguments);

    }

    static void RegisterProtocol()
    {
        try
        {
            // Replace 'game' with your custom protocol name
            string protocolName = "games";
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
