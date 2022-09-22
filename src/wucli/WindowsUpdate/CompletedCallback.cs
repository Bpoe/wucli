namespace Microsoft.WindowsUpdate;

using System.Threading.Tasks;
using WUApiLib;

public class CompletedCallback<TJob, TArgs>
{
    private readonly TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

    public TJob? Job { get; private set; }

    public TArgs? Arguments { get; private set; }

    public void Invoke(TJob job, TArgs args)
    {
        Job = job;
        Arguments = args;
        taskCompletionSource.TrySetResult(true);
    }

    public Task<bool> WaitAsync() => taskCompletionSource.Task;
}
public class SearchCompletedCallback : CompletedCallback<ISearchJob, ISearchCompletedCallbackArgs>, ISearchCompletedCallback
{
}

public class DownloadCompletedCallback : CompletedCallback<IDownloadJob, IDownloadCompletedCallbackArgs>, IDownloadCompletedCallback
{
}

public class InstallationCompletedCallback : CompletedCallback<IInstallationJob, IInstallationCompletedCallbackArgs>, IInstallationCompletedCallback
{
}

public class InstallationProgressChangedCallback : CompletedCallback<IInstallationJob, IInstallationProgressChangedCallbackArgs>, IInstallationProgressChangedCallback
{
}

public class DownloadProgressChangedCallback : CompletedCallback<IDownloadJob, IDownloadProgressChangedCallbackArgs>, IDownloadProgressChangedCallback
{
}