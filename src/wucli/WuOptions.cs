namespace wucli;

public class WuOptions
{
    public string Criteria { get; set; } = "IsInstalled=0 AND IsHidden=0";

    public bool Reboot { get; set; } = false;
}