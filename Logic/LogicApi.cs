﻿using Data;
using System.Runtime.Intrinsics;

namespace Logic
{
    public abstract class LogicApi
    {
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract double GetX(int number);
        public abstract double GetY(int number);
        public abstract event EventHandler LogicApiEvent;
        public static LogicApi Instance()
        {
            return new Logic();
        }
        private class Logic : LogicApi
        {
            DataApi dataApi;
            object _lock = new object();
            public Logic()
            {
                dataApi = DataApi.Instance();
                dataApi.BallEvent += Ball_PositionChanged;
            }
            public override event EventHandler LogicApiEvent;

            public override void CreateBalls(int number)
            {
                dataApi.CreateBalls(number);
            }
            public override int GetNumberOfBalls()
            {
                return dataApi.GetNumberOfBalls();
            }
            public override double GetX(int number)
            {
                return dataApi.GetX(number);
            }
            public override double GetY(int number)
            {
                return dataApi.GetY(number);
            }

            private void Ball_PositionChanged(object sender, EventArgs e)
            {
                IBall ball = (IBall)sender;
                if (ball != null) {
                    CheckCollisionWithBalls(ball);
                    CheckCollisionWithWalls(ball);
                    LogicApiEvent?.Invoke(this, EventArgs.Empty);
                }
            }

            private void CheckCollisionWithWalls(IBall ball)
            {
                if (ball.X < 0)
                {
                    ball.HorizontalSpeed *= -1;
                }
                if (ball.Y < 0)
                {
                    ball.VerticalSpeed *= -1;
                }

                if (ball.X + IBall.Radius > dataApi.Width)
                {
                    ball.HorizontalSpeed *= -1;
                }
                if (ball.Y + IBall.Radius > dataApi.Height)
                {
                    ball.VerticalSpeed *= -1;
                }
            }

            private void CheckCollisionWithBalls(IBall ball)
            {
                lock(_lock)
                {
                    for (int i = 0; i < dataApi.GetNumberOfBalls(); i++)
                    {
                        IBall secondBall = dataApi.GetBall(i);
                        if (secondBall != ball)
                        {
                            double d = Math.Sqrt(Math.Pow(ball.X - secondBall.X, 2) + Math.Pow(ball.Y - secondBall.Y, 2));
                            if (d - (IBall.Radius) <= 0)
                            {
                                double hv1 = (ball.HorizontalSpeed * (ball.Weight - secondBall.Weight) + 2 * secondBall.Weight * secondBall.HorizontalSpeed) / (ball.Weight + secondBall.Weight);
                                double vv1 = (ball.VerticalSpeed * (ball.Weight - secondBall.Weight) + 2 * secondBall.Weight * secondBall.VerticalSpeed) / (ball.Weight + secondBall.Weight);

                                double hv2 = (secondBall.HorizontalSpeed * (secondBall.Weight - ball.Weight) + 2 * ball.Weight * ball.HorizontalSpeed) / (secondBall.Weight + ball.Weight);
                                double vv2 = (secondBall.VerticalSpeed * (secondBall.Weight - ball.Weight) + 2 * ball.Weight * ball.VerticalSpeed) / (secondBall.Weight + ball.Weight);

                                ball.HorizontalSpeed = hv1;
                                ball.VerticalSpeed = vv1;

                                secondBall.HorizontalSpeed = hv2;
                                secondBall.VerticalSpeed = vv2;
                            }
                        }
                    }
                }
            }
        }
    }
}
