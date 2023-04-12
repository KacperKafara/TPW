namespace Logic
{
    public class Ball
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }
        public const int Radius = 50;

        private Random rnd;
        private int Speed = 4;

        private static System.Timers.Timer aTimer;

        public Ball(float x, float y)
        {
            X = x;
            Y = y;
            rnd = new Random();
            HorizontalSpeed = generateRandomSpeed();
            VerticalSpeed = generateRandomSpeed();

            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000 / 30;
            aTimer.Elapsed += OnUpdate;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnUpdate(Object source, System.Timers.ElapsedEventArgs e)
        {
            X += HorizontalSpeed;
            Y += VerticalSpeed;
        }

        public float generateRandomSpeed()
        {
            float speed;
            do
            {
                speed = rnd.NextSingle() * Speed - Speed / 2;
            }
            while (speed == 0);
            return speed * 2;
        }

        public void updatePosition(Board board)
        {
            if (X < 0) 
            {
                X = 0;
                HorizontalSpeed = generateRandomSpeed();
            }
            if (Y < 0) 
            { 
                Y = 0;
                VerticalSpeed = generateRandomSpeed();
            }

            if (X + Radius > board.Width) 
            {
                X = board.Width - Radius;
                HorizontalSpeed = generateRandomSpeed();
            }
            if (Y + Radius > board.Height)
            {
                Y = board.Height - Radius;
                VerticalSpeed = generateRandomSpeed();
            }
        }
    }
}