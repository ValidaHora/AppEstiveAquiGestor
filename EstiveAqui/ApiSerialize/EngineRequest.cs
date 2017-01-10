using System;
using System.Threading;
using System.Threading.Tasks;

namespace EstiveAqui.ApiSerialize
{
    public static class EngineRequest
    {
        public static Task Interval(int ms, Action action, CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 10000; i++)
                {
                    if (token.WaitCancellationRequested(ms))
                        break;

                    if (App.Current.Properties.ContainsKey("IdApp"))
                        action();
                }
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
    }

    static class CancellationTokenExtensions
    {
        public static bool WaitCancellationRequested(this CancellationToken token, int ms)
        {
            return token.WaitHandle.WaitOne(ms);
        }
    }
}
