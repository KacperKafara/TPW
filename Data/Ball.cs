using System;

namespace Data
{
    public interface IBall
    {
        double X { get; }
        double Y { get; }
        double HorizontalMove { get; set; }
        double VerticalMove { get; set;}
        int MoveTime { get; set;}
        const int Radius = 50;
        double Weight { get; }
        double HorizontalSpeed { get; set; }
        double VerticalSpeed { get; set; }
        void Move();
        void RunTask();
    }

    internal class Ball : IBall
    {
        private double _x;
        private double _y;
        private double _horizontalMove;
        private double _verticalMove;
        private int _moveTime;
        private double _horizontalSpeed;
        private double _verticalSpeed;
        private double _weight;

        public Ball(int x, int y, int weight)
        {
            _x = x;
            _y = y;
            _weight = weight;
            Random rnd = new Random();
            HorizontalMove = rnd.NextDouble() * 20 - 10;
            VerticalMove = rnd.NextDouble() * 20 - 10;
            MoveTime = 1000/60;
            HorizontalSpeed = HorizontalMove / MoveTime;
            VerticalSpeed = HorizontalMove / MoveTime;
        }

        internal event EventHandler PositionChanged;

        internal void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, EventArgs.Empty);
        }

        public double X
        {
            get => _x;
            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
            }
        }

        public double HorizontalMove
        {
            get => _horizontalMove;
            set
            {
                _horizontalMove = value;
            }
        }

        public double VerticalMove
        {
            get => _verticalMove;
            set
            {
                _verticalMove = value;
            }
        }
        
        public int MoveTime
        {
            get => _moveTime;
            set
            {
                _moveTime = value;
            }
        }

        public double Weight { get => _weight; }
        public double HorizontalSpeed 
        { 
            get => _horizontalSpeed; 
            set
            {
                _horizontalSpeed = value;
                HorizontalMove = value * MoveTime;
            }
        }
        public double VerticalSpeed 
        {
            get => _verticalSpeed; 
            set
            {
                _verticalSpeed = value;
                VerticalMove = value * MoveTime;
            }
        }

        public void Move()
        {
            X += HorizontalMove;
            Y += VerticalMove;
            OnPositionChanged();
        }
        public void RunTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    Move();
                    await Task.Delay(MoveTime);
                }
            });
        }
    }
}
