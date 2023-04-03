namespace wucli;

using System.CommandLine;
using System.Threading.Tasks;

public class Program
{
	public static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Command line interface for working with Windows Updates.")
            .AddInstallCommand()
			.AddListAvailableCommand()
			.AddListInstalledCommand();

        return await rootCommand.InvokeAsync(args);
	}
}
