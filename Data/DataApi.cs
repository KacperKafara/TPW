namespace Data
{
    public abstract class DataApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract event EventHandler BallEvent;
        public abstract double GetX(IBall ball);
        public abstract double GetY(IBall ball);
        public abstract double GetWeight(IBall ball);
        public abstract double GetX(int number);
        public abstract double GetY(int number);
        public abstract double GetWeight(int number);
        public abstract void SetHorizontalMove(IBall ball, double value);
        public abstract void SetVerticalMove(IBall ball, double value);
        public abstract void SetHorizontalMove(int number, double value);
        public abstract void SetVerticalMove(int number, double value);
        public abstract void ReverseHorizontalMove(IBall ball);
        public abstract void ReverseVerticalMove(IBall ball);
        public abstract void SetMoveTime(IBall ball, int value);
        public abstract void SetMoveTime(int ball, int value);
        public abstract double GetHorizontalSpeed(IBall ball);
        public abstract double GetHorizontalSpeed(int number);
        public abstract double GetVerticalSpeed(IBall ball);
        public abstract double GetVerticalSpeed(int number);


        public static DataApi Instance()
        {
            return new Data();
        }

        private class Data : DataApi
        {
            private Mutex mutex = new Mutex();
            private List<IBall> Balls { get; }
            public override int Width { get; }
            public override int Height { get; }
            public override event EventHandler BallEvent;
            public Data()
            {
                Balls = new List<IBall>();
                Width = 500;
                Height = 500;
            }
            public override void CreateBalls(int number)
            {
                Random rnd = new Random();
                for (int i = 0; i < number; i++)
                {
                    mutex.WaitOne();
                    Ball ball = new Ball(rnd.Next(100, 300), rnd.Next(100, 300), 100);
                    Balls.Add(ball);
                    mutex.ReleaseMutex();
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
                }
            }
            public override double GetX(IBall ball)
            {
                return ball.X;
            }
            public override double GetY(IBall ball)
            {
                return ball.Y;
            }
            public override double GetX(int number)
            {
                return Balls[number].X;
            }
            public override double GetY(int number)
            {
                return Balls[number].Y;
            }
            public override double GetWeight(IBall ball)
            {
                return ball.Weight;
            }
            public override double GetWeight(int number)
            {
                return Balls[number].Weight;
            }
            public override void SetHorizontalMove(IBall ball, double value)
            {
                ball.HorizontalMove = value;
            }

            public override void SetVerticalMove(IBall ball, double value)
            {
                ball.VerticalMove = value;
            }
            public override void SetHorizontalMove(int number, double value)
            {
                Balls[number].HorizontalMove = value;
            }

            public override void SetVerticalMove(int number, double value)
            {
                Balls[number].VerticalMove = value;
            }
            public override void ReverseHorizontalMove(IBall ball)
            {
                ball.HorizontalMove *= -1;
            }

            public override void ReverseVerticalMove(IBall ball)
            {
                ball.VerticalMove *= -1;
            }
            public override void SetMoveTime(int number, int value)
            {
                Balls[number].MoveTime = value;
            }
            public override void SetMoveTime(IBall ball, int value)
            {
                ball.MoveTime *= -1;
            }
            public override double GetHorizontalSpeed(IBall ball)
            {
                return ball.HorizontalMove;
            }
            public override double GetHorizontalSpeed(int number)
            {
                return Balls[number].HorizontalMove;
            }
            public override double GetVerticalSpeed(IBall ball)
            {
                return ball.VerticalMove;
            }
            public override double GetVerticalSpeed(int number)
            {
                return Balls[number].VerticalMove;
            }
        }
    }
}