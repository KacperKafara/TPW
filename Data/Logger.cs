using System.Collections.Concurrent;
using System.Text.Json;

namespace Data
{
    internal class Logger
    {
        ConcurrentQueue<string> _queue;

        public Logger() 
        {
            _queue = new ConcurrentQueue<string>();

            /*Task.Run(async () =>
            {
                
            });*/
        }

        public void AddObjectToQueue(IBall obj)
        {
            string jsonString = JsonSerializer.Serialize(obj);
            _queue.Enqueue(jsonString);
            System.Diagnostics.Debug.WriteLine(jsonString);
        }
    }
}
