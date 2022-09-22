namespace wucli;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsUpdate;
using WUApiLib;

public static class ListInstalledUpdates
{
    private const string Criteria = "IsInstalled=1 AND IsHidden=0";

    public static readonly Command Command = new Command
    {
        Name = "list-installed",
        Summary = "Get the updates that are installed on this machine.",
        Description = "list-installed will return a list of Windows Updates that are installed on this machine.",
        Run = ListInstalledUpdates.RunListInstalledUpdates,
    };

    public static async Task<int> RunListInstalledUpdates(string[] args)
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
