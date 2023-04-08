namespace Logic
{
    public class Ball
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }
        public int Radius { get; set; }

        private Random rnd;
        private int Speed=4;

        public Ball(float x, float y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
            rnd = new Random();
            HorizontalSpeed = rnd.NextSingle() * Speed - Speed / 2;
            VerticalSpeed = rnd.NextSingle() * Speed - Speed / 2;
        }

        public void updatePosition(Board board)
        {

            X += HorizontalSpeed;
            Y += VerticalSpeed;

            if (X < 0) 
            {
                X = 0;
                HorizontalSpeed = rnd.NextSingle() * Speed - Speed / 2;
            }
            if (Y < 0) 
            { 
                Y = 0;
                VerticalSpeed = rnd.NextSingle() * Speed - Speed / 2;
            }

            if (X + Radius > board.Width) 
            {
                X = board.Width - Radius;
                HorizontalSpeed = rnd.NextSingle() * Speed - Speed / 2;
            }
            if (Y + Radius > board.Height)
            {
                Y = board.Height - Radius;
                VerticalSpeed = rnd.NextSingle() * Speed - Speed / 2;
            }
        }
    }
}