namespace Logic
{
    public abstract class LogicApi
    {
        internal abstract List<Ball> Balls { get; }
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract float GetX(int number);
        public abstract float GetY(int number);
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
            public override void CreateBalls(int number)
            {
                Random rnd = new Random();
                for (int i = 0; i < number; i++)
                {
                    Ball ball = new Ball(rnd.Next(100, 300), rnd.Next(100, 300));
                    Balls.Add(ball);
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
        }
    }
}
