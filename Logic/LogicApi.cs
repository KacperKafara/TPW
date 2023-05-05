namespace Logic
{
    public abstract class LogicApi
    {
        internal abstract List<Ball> Balls { get; }
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract float GetX(int number);
        public abstract float GetY(int number);
        public abstract event EventHandler LogicApiEvent;
        public static LogicApi Instance()
        {
            return new Logic();
        }
        private class Logic : LogicApi
        {
            internal override List<Ball> Balls { get; }
            public Logic()
            {
                Balls = new List<Ball>();
            }
            public override event EventHandler LogicApiEvent;

            public override void CreateBalls(int number)
            {
                Random rnd = new Random();
                for (int i = 0; i < number; i++)
                {
                    Ball ball = new Ball(rnd.Next(100, 300), rnd.Next(100, 300));
                    Balls.Add(ball);
                    ball.PositionChanged += Ball_PositionChanged;
                }
            }
            public override int GetNumberOfBalls()
            {
                return Balls.Count;
            }
            public override float GetX(int number)
            {
                return Balls[number].X;
            }
            public override float GetY(int number)
            {
                return Balls[number].Y;
            }

            private void Ball_PositionChanged(object sender, EventArgs e)
            {
                Ball ball = sender as Ball;
                if (ball != null)
                {
                    updatePosition(ball);
                    LogicApiEvent?.Invoke(ball, EventArgs.Empty);
                }
            }

            private void updatePosition(Ball ball)
            {
                if (ball.X < 0)
                {
                    ball.X = 0;
                    ball.HorizontalSpeed *= -1;
                }
                if (ball.Y < 0)
                {
                    ball.Y = 0;
                    ball.VerticalSpeed *= -1;
                }

                if (ball.X + Ball.Radius > 500)
                {
                    ball.X = 500 - Ball.Radius;
                    ball.HorizontalSpeed *= -1;
                }
                if (ball.Y + Ball.Radius > 500)
                {
                    ball.Y = 500 - Ball.Radius;
                    ball.VerticalSpeed *= -1;
                }

                Task.Run(async () =>
                {
                    Task[] collisionTasks = new Task[Balls.Count];
                    int taskIndex = 0;
                    for (int i = 0; i < Balls.Count; i++)
                    {
                        collisionTasks[taskIndex] = ball.CheckCollisionWith(Balls[i]);
                        taskIndex++;
                    }

                    await Task.Delay(50);
                });
            }
        }
    }
}
