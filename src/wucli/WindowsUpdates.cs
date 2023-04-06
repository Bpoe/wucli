using System;

namespace wucli;

using System;
using System.Collections.Generic;
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

        if (result.Updates.Count == 0)
        {
            Console.WriteLine("No updates to install.");
            return 0;
        }

        Console.WriteLine($"{result.Updates.Count} updates to be installed.");

        var downloadResults = await client
            .DownloadAsync(result.Updates, CancellationToken.None);

        if (downloadResults.ResultCode == OperationResultCode.orcFailed)
        {
            Console.Error.WriteLine($"Download failed. 0x{downloadResults.HResult.ToString("X")}");
            return -1;
        }

        var installResults = await client
            .InstallAsync(result.Updates, CancellationToken.None);

        if (installResults.ResultCode == OperationResultCode.orcFailed)
        {
            Console.Error.WriteLine($"Installation failed. 0x{installResults.HResult.ToString("X")}");
            return -1;
        }

        Console.WriteLine("Installation complete.");
        
        if (installResults.RebootRequired)
        {
            if (options.Reboot)
            {
                Console.WriteLine("Rebooting...");
                NativeMethods.Reboot();
            }
            else
            {
                Console.WriteLine("Reboot Required");
            }

            return 1;
        }

        return 0;
    }

    public static async Task<int> ListAvailable(WuOptions options)
    {
        var result = await new WindowsUpdateClient()
            .SearchAsync(options.Criteria, CancellationToken.None);

        if (result.Updates.Count == 0)
        {
            Console.WriteLine("No updates available.");
            return 0;
        }

        var updates = result
            .Updates
            .Select(o => (IUpdate)o);

        WriteTable(updates);

        return 0;
    }

    public static async Task<int> ListInstalled()
    {
        var result = await new WindowsUpdateClient()
            .SearchAsync(InstalledUpdatesCriteria, false, CancellationToken.None);

        if (result.Updates.Count == 0)
        {
            Console.WriteLine("No updates installed.");
            return 0;
        }

        var updates = result
            .Updates
            .Select(o => (IUpdate)o);

        WriteTable(updates);

        return 0;
    }

    public static void WriteTable(IEnumerable<IUpdate> updates)
    {
        const int KbColumnWidth = 16;
        var maxTitleLength = Console.WindowWidth - KbColumnWidth - 1;

        Console.WriteLine("{0,-16} {1}", "KB", "Title");
        Console.WriteLine(new string('-', Console.WindowWidth));
        foreach (var update in updates)
        {
            var kbs = TrimToLength(
                string.Join(", ", update.KBArticleIDs.Select(s => "KB" + (string)s)), KbColumnWidth);
            Console.WriteLine("{0,-16} {1}", kbs, TrimToLength(update.Title, maxTitleLength));
        }
    }

    private static string TrimToLength(string s, int maxLength)
        => s.Length > maxLength ? string.Concat(s.AsSpan(0, maxLength - 3), "...") : s;
}
