namespace Logic
{
    internal class Ball
    {
        private float x;
        private float y;

        public float X
        {
            get { return x; }
            set
            {
                x = value;
                OnPositionChanged();
            }
        }
        public float Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPositionChanged();
            }
        }

        public event EventHandler PositionChanged;

        protected virtual void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, EventArgs.Empty);
        }

        public float HorizontalSpeed { private get; set; }
        public float VerticalSpeed { private get; set; }
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
    }
}