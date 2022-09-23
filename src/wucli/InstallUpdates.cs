namespace wucli;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsUpdate;

public static class InstallUpdates
{
    private const string Criteria = "IsInstalled=1 AND IsHidden=0";

    public static readonly Command Command = new Command
    {
        Name = "install",
        Summary = "Get the updates that are installed on this machine.",
        Description = "install will install all available updates on this machine.",
        Run = InstallUpdates.InstalledUpdates,
    };

    public static async Task<int> InstalledUpdates(string[] args)
    {
        var client = new WindowsUpdateClient();

        var result = await client
            .SearchAsync(Criteria, CancellationToken.None);

        Console.WriteLine($"{result.Updates.Count} updates to be installed.");

        var installResults = await client
            .InstallAsync(result.Updates, CancellationToken.None);

        Console.WriteLine("Installation complete.");
        
        if (installResults.RebootRequired)
        {
            Console.WriteLine("Reboot Required");
            return 1;
        }

        return 0;
    }
}