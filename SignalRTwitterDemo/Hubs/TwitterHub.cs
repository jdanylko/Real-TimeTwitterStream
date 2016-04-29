using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTwitterDemo.Hubs
{
    [HubName("twitterHub")]
    public class TwitterHub : Hub
    {
        private static ConcurrentDictionary<string, TwitterTaskData> _currentTasks;

        private ConcurrentDictionary<string, TwitterTaskData> CurrentTasks
        {
            get { return _currentTasks ?? (_currentTasks = new ConcurrentDictionary<string, TwitterTaskData>()); }
        }

        public async Task StartTwitterLive()
        {
            var tokenSource = new CancellationTokenSource();

            var taskId = string.Format("T-{0}", Guid.NewGuid());

            CurrentTasks.TryAdd(taskId, new TwitterTaskData
            {
                CancelToken = tokenSource,
                Id = taskId,
                Status = "Started."
            });

            await Clients.Caller.setTaskId(taskId);

            var task = TwitterStream.StartStream(tokenSource.Token);

            await task;
        }

        public async Task StopTwitterLive(string taskId)
        {
            if (CurrentTasks.ContainsKey(taskId))
            {
                CurrentTasks[taskId].CancelToken.Cancel();
            }

            await Clients.Caller.updateStatus("Stopped.");
        }
    }
}