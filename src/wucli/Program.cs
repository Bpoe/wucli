namespace wucli;

using System;
using System.Linq;
using System.Threading.Tasks;

public class Program
{
	public static async Task<int> Main(string[] args)
    {
		if (args.Length < 1)
        {
			args = new[] { "help" };
		}

		var cmd = Context
			.Commands
			.FirstOrDefault(c => c.Name.Equals(args[0], StringComparison.InvariantCultureIgnoreCase));

		if (cmd is null)
		{
			Console.WriteLine($"{Context.cliName}: unknown subcommand: {args[0]}\n");
			Console.WriteLine($"Run '{Context.cliName} help' for usage.\n");
			return 2;
		}

		return await cmd.Run(args[1..]);
	}
}
