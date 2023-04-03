namespace wucli;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsUpdate;
using WUApiLib;

public static class WindowsUpdates
{
    private const string InstalledUpdatesCriteria = "IsInstalled=1 AND IsHidden=0";

    public static async Task<int> Install(WuOptions options)
    {
        var client = new WindowsUpdateClient();

        var result = await client
            .SearchAsync(options.Criteria, CancellationToken.None);

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

    public static async Task<int> ListAvailable(WuOptions options)
    {
        var result = await new WindowsUpdateClient()
            .SearchAsync(options.Criteria, CancellationToken.None);

        var updates = result
            .Updates
            .Select(o => (IUpdate)o);

        foreach (var update in updates)
        {
            Console.WriteLine(update.Title);
        }

        return 0;
    }

    public static async Task<int> ListInstalled()
    {
        var result = await new WindowsUpdateClient()
            .SearchAsync(InstalledUpdatesCriteria, CancellationToken.None);

        var updates = result
            .Updates
            .Select(o => (IUpdate)o);

        foreach (var update in updates)
        {
            Console.WriteLine(update.Title);
        }

        return 0;
    }
}
