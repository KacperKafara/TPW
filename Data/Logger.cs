using System.Collections.Concurrent;
using System.Text.Json;

namespace Data
{
    internal class Logger
    {
        ConcurrentQueue<string> _queue;
        private StreamWriter _streamWriter;

        public Logger()
        {
            _queue = new ConcurrentQueue<string>();
            _streamWriter = new StreamWriter("logs.json");
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
