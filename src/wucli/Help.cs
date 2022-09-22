namespace wucli;

using System;
using System.Linq;
using System.Threading.Tasks;

public static class Help
{
    public static readonly Command Command = new Command
    {
        Name = "help",
        Summary = "Show a list of commands or help for one command",
        Usage = "[COMMAND]",
        Description = "Show a list of commands or detailed help for one command",
        Run = Help.RunHelp,
    };

    public static async Task<int> RunHelp(string[] args)
    {
        if (args.Length < 1)
        {
            PrintGlobalHelp();
            return 0;
        }

        var cmd = Context.Commands
            .FirstOrDefault(c => c.Name.Equals(args[0], StringComparison.InvariantCultureIgnoreCase));

        if (cmd is null)
        {
            Console.WriteLine("Unrecognized command: " + string.Join(" ", args[0]));
            return 1;
        }

        PrintCommandUsage(cmd);
        return 0;
    }

    private static void PrintGlobalHelp()
    {
        Console.WriteLine(FormatGlobalUsage(
            Context.cliName,
            Context.cliDescription,
            "",
            Context.Commands,
            GetAllFlags()));
    }

    private static Flag[] GetAllFlags()
    {
        return Context.GlobalFlags;
    }

    private static void PrintCommandUsage(Command cmd)
    {
        Console.WriteLine(FormatCommandUsage(Context.cliName, cmd));
    }

    private static string FormatGlobalUsage(string executable, string description, string version, Command[] cmds, Flag[] flags)
    {
        var template = $@"
NAME:
    {executable} - {description}

USAGE:
    {executable} [global options] <command> [command options] [arguments...]

VERSION:
    {version}

COMMANDS:
";
        foreach (var cmd in cmds)
        {
            template += $"    {cmd.Name} - {cmd.Summary}\n";
        }

        template += "\nGLOBAL OPTIONS:\n";
        foreach (var flag in flags)
        {
            template += GetOptionString(flag);
        }

        template += $"\nRun \"{executable} help <command>\" for more details on a specific command.";

        return template;
    }

    private static string FormatCommandUsage(string executable, Command cmd)
    {
        var template = $@"
NAME:
    {cmd.Name} - {cmd.Summary}
USAGE:
    {executable} {cmd.Name} {cmd.Usage}

DESCRIPTION:
    {cmd.Description}";

        if (cmd.Flags != null)
        {
            template += "\nOPTIONS:\n";
            foreach (var option in cmd.Flags.Formal.Values)
            {
                template += GetOptionString(option);
            }
        }

        template += $"\nFor help on global options run \"{executable} help\"";

        return template;
    }

    private static string GetOptionString(Flag option)
    {
        var prefix = "--";
        return $"    {prefix}{option.Name}={option.DefaultValue ?? "\t"}\t{option.Usage}\n";
    }
}
