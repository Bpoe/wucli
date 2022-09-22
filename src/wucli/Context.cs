namespace wucli;

public static class Context
{
    public const string cliName = "wucli";

    public const string cliDescription = "wucli";

    public static readonly Command[] Commands = new[]
    {
        Help.Command,
        ListAvailableUpdates.Command,
        ListInstalledUpdates.Command,
        InstallUpdates.Command,
    };

    public static readonly Flag[] GlobalFlags = new Flag[0];
}
