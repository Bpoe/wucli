namespace wucli;

using System;
using System.CommandLine;

public static class CommandExtensions
{
    public static RootCommand AddInstallCommand(this RootCommand rootCommand)
    {
        var criteriaOption = new Option<string>("--criteria", "The criteria for updates (Default: \"IsInstalled=0 AND IsHidden=0\")");
        criteriaOption.AddAlias("-c");

        var rebootOption = new Option<bool>("--reboot", "Reboot the machine if any updates require a reboot.");
        rebootOption.AddAlias("-r");

        var installCommand = new Command("install", "Install all available updates on this machine.");
        installCommand.AddOption(criteriaOption);
        installCommand.AddOption(rebootOption);
        rootCommand.Add(installCommand);
        installCommand.SetHandler(
            (criteria, reboot) =>
            {
                var options = new WuOptions
                {
                    Criteria = criteria,
                    Reboot = reboot,
                };

                var result = WindowsUpdates.Install(options).Result;
                Environment.Exit(result);
            },
            criteriaOption,
            rebootOption);

        return rootCommand;
    }

    public static RootCommand AddListInstalledCommand(this RootCommand rootCommand)
    {
        var installCommand = new Command("list-installed", "Get the updates that are installed on this machine.");
        rootCommand.Add(installCommand);
        installCommand.SetHandler(() =>
        {
            var result = WindowsUpdates.ListInstalled().Result;
            Environment.Exit(result);
        });

        return rootCommand;
    }

    public static RootCommand AddListAvailableCommand(this RootCommand rootCommand)
    {
        var criteriaOption = new Option<string>("--criteria", "The criteria for updates (Default: \"IsInstalled=0 AND IsHidden=0\"");
        criteriaOption.AddAlias("-c");

        var installCommand = new Command("list-available", "Get the updates that are available for this machine.");
        installCommand.AddOption(criteriaOption);
        rootCommand.Add(installCommand);
        installCommand.SetHandler(
            (criteria) =>
            {
                var options = new WuOptions
                {
                    Criteria = criteria,
                };

                var result = WindowsUpdates.ListAvailable(options).Result;
                Environment.Exit(result);
            },
            criteriaOption);

        return rootCommand;
    }
}