namespace wucli;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsUpdate;
using WUApiLib;

public static class ListAvailableUpdates
{
    private const string Criteria = "IsInstalled=0 AND IsHidden=0";

    public static readonly Command Command = new Command
    {
        Name = "list-available",
        Summary = "Get the updates that are available for this machine.",
        Description = "list-available will return a list of Windows Updates that are available to be installed on this machine.",
        Run = ListAvailableUpdates.RunListAvailableUpdates,
    };

    public static async Task<int> RunListAvailableUpdates(string[] args)
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
