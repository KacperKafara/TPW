namespace Logic
{
    internal class Ball
    {
        private float x;
        private float y;

        internal float X
        {
            get { return x; }
            set
            {
                x = value;
                OnPositionChanged();
            }
        }
        internal float Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPositionChanged();
            }
        }

        internal event EventHandler PositionChanged;

        internal void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, EventArgs.Empty);
        }

        internal float HorizontalSpeed { get; set; }
        internal float VerticalSpeed { get; set; }
        internal const int Radius = 50;

        private Random rnd;
        private int Speed = 4;

        private static System.Timers.Timer aTimer;

        internal Ball(float x, float y)
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

        internal float generateRandomSpeed()
        {
            float speed;
            do
            {
                speed = rnd.NextSingle() * Speed - Speed / 2;
            }
            while (speed == 0);
            return speed * 2;
        }

        public async Task CheckCollisionWith(Ball other)
        {
            float dx = X - other.X;
            float dy = Y - other.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance < Radius)
            {
                float temp = HorizontalSpeed;
                HorizontalSpeed = other.HorizontalSpeed;
                other.HorizontalSpeed = temp;

                temp = VerticalSpeed;
                VerticalSpeed = other.VerticalSpeed;
                other.VerticalSpeed = temp;

                await Task.Delay(100);
            }
        }
    }
}