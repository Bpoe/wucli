namespace Microsoft.WindowsUpdate;

using System;
using WUApiLib;

public class WindowsUpdate : IUpdate
{
    internal IUpdate BaseIUpdate;

    public WindowsUpdate(IUpdate update)
    {
        BaseIUpdate = update;
    }

    public void AcceptEula()
    {
        BaseIUpdate.AcceptEula();
    }

    public void CopyFromCache(string path, bool toExtractCabFiles)
    {
        BaseIUpdate.CopyFromCache(path, toExtractCabFiles);
    }

    public string Title => BaseIUpdate.Title;

    public bool AutoSelectOnWebSites => BaseIUpdate.AutoSelectOnWebSites;

    public UpdateCollection BundledUpdates => BaseIUpdate.BundledUpdates;

    public bool CanRequireSource => BaseIUpdate.CanRequireSource;

    public ICategoryCollection Categories => BaseIUpdate.Categories;

    public object Deadline => BaseIUpdate.Deadline;

    public bool DeltaCompressedContentAvailable => BaseIUpdate.DeltaCompressedContentAvailable;

    public bool DeltaCompressedContentPreferred => BaseIUpdate.DeltaCompressedContentPreferred;

    public string Description => BaseIUpdate.Description;

    public bool EulaAccepted => BaseIUpdate.EulaAccepted;

    public string EulaText => BaseIUpdate.EulaText;

    public string HandlerID => BaseIUpdate.HandlerID;

    public IUpdateIdentity Identity => BaseIUpdate.Identity;

    public IImageInformation Image => BaseIUpdate.Image;

    public IInstallationBehavior InstallationBehavior => BaseIUpdate.InstallationBehavior;

    public bool IsBeta => BaseIUpdate.IsBeta;

    public bool IsDownloaded => BaseIUpdate.IsDownloaded;

    public bool IsHidden
    {
        get => BaseIUpdate.IsHidden;
        set => BaseIUpdate.IsHidden = value;
    }

    public bool IsInstalled => BaseIUpdate.IsInstalled;

    public bool IsMandatory => BaseIUpdate.IsMandatory;

    public bool IsUninstallable => BaseIUpdate.IsUninstallable;

    public StringCollection Languages => BaseIUpdate.Languages;

    public DateTime LastDeploymentChangeTime => BaseIUpdate.LastDeploymentChangeTime;

    public decimal MaxDownloadSize => BaseIUpdate.MaxDownloadSize;

    public decimal MinDownloadSize => BaseIUpdate.MinDownloadSize;

    public StringCollection MoreInfoUrls => BaseIUpdate.MoreInfoUrls;

    public string MsrcSeverity => BaseIUpdate.MsrcSeverity;

    public int RecommendedCpuSpeed => BaseIUpdate.RecommendedCpuSpeed;

    public int RecommendedHardDiskSpace => BaseIUpdate.RecommendedHardDiskSpace;

    public int RecommendedMemory => BaseIUpdate.RecommendedMemory;

    public string ReleaseNotes => BaseIUpdate.ReleaseNotes;

    public StringCollection SecurityBulletinIDs => BaseIUpdate.SecurityBulletinIDs;

    public StringCollection SupersededUpdateIDs => BaseIUpdate.SupersededUpdateIDs;

    public string SupportUrl => BaseIUpdate.SupportUrl;

    public UpdateType Type => BaseIUpdate.Type;

    public string UninstallationNotes => BaseIUpdate.UninstallationNotes;

    public IInstallationBehavior UninstallationBehavior => BaseIUpdate.UninstallationBehavior;

    public StringCollection UninstallationSteps => BaseIUpdate.UninstallationSteps;

    public StringCollection KBArticleIDs => BaseIUpdate.KBArticleIDs;

    public DeploymentAction DeploymentAction => BaseIUpdate.DeploymentAction;

    public DownloadPriority DownloadPriority => BaseIUpdate.DownloadPriority;

    public IUpdateDownloadContentCollection DownloadContents => BaseIUpdate.DownloadContents;
}