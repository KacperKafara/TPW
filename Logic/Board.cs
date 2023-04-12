namespace Logic
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Ball> Balls { get; set; }
        public Board(int width, int height) 
        {
            Width = width;
            Height = height;
            Balls = new List<Ball>();
        }

        public void AddBall(Ball b) 
        {
            Balls.Add(b);
        }

        public void updatePosition()
        {
            foreach (Ball ball in Balls)
            {
                if (ball.X < 0)
                {
                    ball.X = 0;
                    ball.HorizontalSpeed = ball.generateRandomSpeed();
                }
                if (ball.Y < 0)
                {
                    ball.Y = 0;
                    ball.VerticalSpeed = ball.generateRandomSpeed();
                }

                if (ball.X + Ball.Radius > Width)
                {
                    ball.X = Width - Ball.Radius;
                    ball.HorizontalSpeed = ball.generateRandomSpeed();
                }
                if (ball.Y + Ball.Radius > Height)
                {
                    ball.Y = Height - Ball.Radius;
                    ball.VerticalSpeed = ball.generateRandomSpeed();
                }
            }
        }
    }
}
