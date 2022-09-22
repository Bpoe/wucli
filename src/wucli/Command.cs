namespace wucli;

using System;
using System.Threading.Tasks;

public class Command
{
    public string Name { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }

    public string Usage { get; set; }

    public FlagSet Flags { get; set; }

    public Func<string[], Task<int>> Run { get; set; }
}
