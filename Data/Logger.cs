using System.Collections.Concurrent;
using System.Text.Json;

namespace Data
{
    internal class Logger
    {
        internal class BallToSerialize
        {
            public float X { get; set; }
            public float Y { get; set; }
            public string Date { get; set; }
            public float SpeedHorizontal { get; set; }
            public float SpeedVertical { get; set; }
            public int Id { get; set; }


            public BallToSerialize(float x, float y, float speedHorizontal, float speedVertical, int id)
            {
                X = x;
                Y = y;
                Date = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss.fff");
                SpeedHorizontal = speedHorizontal;
                SpeedVertical = speedVertical;
                Id = id;
            }
        }

        ConcurrentQueue<BallToSerialize> _queue;
        public Logger()
        {
            _queue = new ConcurrentQueue<BallToSerialize>();
            WriteToFile();
        }

        public void AddObjectToQueue(IBall obj)
        {
            BallToSerialize ballToSerialize = new BallToSerialize(obj.Position.X, obj.Position.Y, obj.Speed.X, obj.Speed.Y, obj.ID);
            _queue.Enqueue(ballToSerialize);
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
                            if (_queue.TryDequeue(out BallToSerialize item))
                            {
                                string jsonString = JsonSerializer.Serialize(item);
                                string date = item.Date;
                                string log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", date, jsonString) + "}\n";
                                _streamWriter.WriteLine(log);
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
