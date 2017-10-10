using System.Threading;
using Newtonsoft.Json;
using Tweetinvi.Models;

namespace SignalRTwitterDemo.Hubs
{
    public class TwitterTaskData
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public IOEmbedTweet Tweet { get; set; }
        [JsonIgnore]
        public CancellationTokenSource CancelToken { get; set; }
    }
}