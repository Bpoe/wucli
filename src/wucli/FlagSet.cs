namespace wucli;

using System;
using System.Collections.Generic;
using System.IO;

public class FlagSet
{
    private TextWriter output;

    public FlagSet(string name, ErrorHandling errorHandling = ErrorHandling.ContinueOnError)
    {
        Name = name;
        ErrorHandling = errorHandling;
    }

    public string Name { get; set; }

    public bool Parsed { get; set; }

    public IDictionary<string, Flag> Actual { get; set; }

    public IDictionary<string, Flag> Formal { get; set; }

    public string[] Args { get; set; }

    public Action<FlagSet> Usage { get; set; }

    public ErrorHandling ErrorHandling { get; set; }

    public TextWriter Output
    {
        get
        {
            return this.output ?? Console.Error;
        }
        
        set
        {
            this.output = value;
        }
    }

    public static void DefaultUsage(FlagSet f)
    {
        if (f.Name == "")
        {
            f.Output.WriteLine("Usage:");
        }
        else
        {
            f.Output.WriteLine("Usage of {0}", f.Name);
        }

        f.PrintDefaults();
    }

    public void PrintDefaults()
    {
    }
}
