using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;

namespace Data
{
    internal class Logger
    {
        readonly ConcurrentQueue<string> _queue;
        readonly object _lock = new();
        readonly private Stopwatch _stopwatch;

        public Logger()
        {
            _queue = new ConcurrentQueue<string>();
            _stopwatch = new Stopwatch();
            WriteToFile();
        }

        public void AddObjectToQueue(IBall obj)
        {
            string jsonString = JsonSerializer.Serialize(obj);
            string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            string log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", date, jsonString) + "}\n";
            _queue.Enqueue(log);
        }

        private void WriteToFile()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    _stopwatch.Restart();
                    _stopwatch.Start();
                    if (_queue.Count > 0)
                    {
                        lock (_lock)
                        {
                            if (_queue.TryDequeue(out string item))
                            {
                                File.AppendAllText("logs.json", item);
                            }
                        }
                    }
                    else
                    {
                        await Task.Delay(100);
                    }
                    _stopwatch.Stop();
                    await Task.Delay((int)_stopwatch.ElapsedMilliseconds);
                }
            });
        }
    }
}
