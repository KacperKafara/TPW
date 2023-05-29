using System;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Data
{
    public interface IBall
    {
        int ID { get; }
        [JsonConverter(typeof(Vector2Converter))]
        Vector2 Position { get; }
        const int Radius = 50;
        [JsonIgnore]
        float Weight { get; }
        [JsonConverter(typeof(Vector2Converter))]
        Vector2 Speed { get; set; }
    }

    internal class Ball : IBall
    {
        private int _moveTime;
        private float _weight;
        private Vector2 _speed;
        private Vector2 _position;
        private Stopwatch _stopwatch;
        public int ID { get; }

        public Ball(int x, int y, float weight, int id)
        {
            _stopwatch = new Stopwatch();
            _weight = weight;
            ID = id;
            Random rnd = new Random();
            Speed = new Vector2(x, y)
            {
                X = (float)(rnd.NextDouble() * (0.75 - (-0.75)) + (-0.75)),
                Y = (float)(rnd.NextDouble() * (0.75 - (-0.75)) + (-0.75))
            };
            Position = new Vector2(x, y)
            {
                X = x,
                Y = y
            };
            MoveTime = 1000/60;
            RunTask();
        }

        internal event EventHandler PositionChanged;

        internal void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private int MoveTime
        {
            get => _moveTime;
            set
            {
                _moveTime = value;
            }
        }
        public Vector2 Position
        {
            get => _position;
            private set { _position = value; }
        }
        public Vector2 Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
            }
        }
        public float Weight { get => _weight; }
        public void Move()
        {
            Position += Speed * MoveTime;
            OnPositionChanged();
        }
        private void RunTask()
        {
            Task.Run(async () =>
            {
                int delay = 0;
                while (true)
                {
                    _stopwatch.Restart();
                    _stopwatch.Start();
                    Move();
                    _stopwatch.Stop();
                    if (MoveTime - _stopwatch.ElapsedMilliseconds < 0)
                    {
                        delay = 0;
                    }
                    else
                    {
                        delay = MoveTime - (int)_stopwatch.ElapsedMilliseconds;
                    }
                    await Task.Delay(delay);
                }
            });
        }
    }

    internal class Vector2Converter : JsonConverter<Vector2>
    {
        public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("X", value.X);
            writer.WriteNumber("Y", value.Y);
            writer.WriteEndObject();
        }
    }
}