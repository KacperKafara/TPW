using System.Numerics;

namespace Data
{
    public abstract class DataApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract Vector2 GetPosition(int number);
        public abstract IBall GetBall(int number);
        public abstract event EventHandler BallEvent;


        public static DataApi Instance()
        {
            return new Data();
        }

        private class Data : DataApi
        {
            private Logger _logger;
            private List<IBall> Balls { get; }
            public override int Width { get; }
            public override int Height { get; }
            public override event EventHandler BallEvent;
            public Data()
            {
                Balls = new List<IBall>();
                Width = 500;
                Height = 500;
                _logger = new Logger();
            }
            public override void CreateBalls(int number)
            {
                Random rnd = new Random();
                int a = Balls.Count;
                for (int i = 0; i < number; i++)
                {
                    Ball ball = new Ball(rnd.Next(100, 300), rnd.Next(100, 300), 10, i + a);
                    Balls.Add(ball);
                    ball.PositionChanged += Ball_PositionChanged;
                }
            }
            public override int GetNumberOfBalls()
            {
                return Balls.Count;
            }

            private void Ball_PositionChanged(object sender, EventArgs e)
            {
                if (sender != null)
                {
                    BallEvent?.Invoke(sender, EventArgs.Empty);
                    _logger.AddObjectToQueue((IBall)sender, DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss.fff"));
                }
            }
            public override Vector2 GetPosition(int number)
            {
                return Balls[number].Position;
            }
            public override IBall GetBall(int number)
            {
                return Balls[number];
            }
        }
    }
}