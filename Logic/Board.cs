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
            foreach (Ball ball in Balls.ToList())
            {
                ball.updatePosition(this);
            }
        }
    }
}
