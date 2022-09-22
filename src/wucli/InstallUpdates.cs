namespace wucli;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsUpdate;
using WUApiLib;

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
        var result = await new WindowsUpdateClient()
            .SearchAsync(Criteria, CancellationToken.None);

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