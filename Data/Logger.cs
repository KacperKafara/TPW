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
            WriteToFile();
        }

        public void AddObjectToQueue(string jsonString)
        {
            string date = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss.fff");
            string log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", date, jsonString) + "}\n";
            _queue.Enqueue(log);
        }

        private void WriteToFile()
        {
            Task.Run(async () =>
            {
                using StreamWriter _streamWriter = new StreamWriter("logs.json");
                while (true)
                {
                    if (_queue.Count > 0)
                    {
                        while (!_queue.IsEmpty)
                        {
                            if (_queue.TryDequeue(out string item))
                            {
                                _streamWriter.WriteLine(item);
                            }
                        }
                        await _streamWriter.FlushAsync();
                    }
                    else
                    {
                        await Task.Delay(100);
                    }
                }
            });
        }
    }
}
