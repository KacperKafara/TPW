using Data;
using System.Runtime.Intrinsics;

namespace Logic
{
    public abstract class LogicApi
    {
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract double GetX(IBall number);
        public abstract double GetY(IBall number);
        public abstract double GetWeight(IBall number);
        public abstract double GetHorizontalSpeed(IBall number);
        public abstract double GetVerticalSpeed(IBall number);
        public abstract double GetX(int number);
        public abstract double GetY(int number);
        public abstract double GetWeight(int number);
        public abstract double GetHorizontalSpeed(int number);
        public abstract double GetVerticalSpeed(int number);
        public abstract event EventHandler LogicApiEvent;
        public static LogicApi Instance()
        {
            return new Logic();
        }
        private class Logic : LogicApi
        {
            DataApi dataApi;
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
            public override double GetX(IBall ball)
            {
                return dataApi.GetX(ball);
            }
            public override double GetY(IBall ball)
            {
                return dataApi.GetY(ball);
            }
            public override double GetWeight(IBall ball)
            {
                return dataApi.GetWeight(ball);
            }
            public override double GetHorizontalSpeed(IBall ball)
            {
                return dataApi.GetHorizontalSpeed(ball);
            }
            public override double GetVerticalSpeed(IBall ball)
            {
                return dataApi.GetVerticalSpeed(ball);
            }

            public override double GetX(int number)
            {
                return dataApi.GetX(number);
            }
            public override double GetY(int number)
            {
                return dataApi.GetY(number);
            }

            public override double GetWeight(int number)
            {
                return dataApi.GetWeight(number);
            }

            public override double GetHorizontalSpeed(int number)
            {
                return dataApi.GetHorizontalSpeed(number);
            }
            public override double GetVerticalSpeed(int number)
            {
                return dataApi.GetVerticalSpeed(number);
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
                if (dataApi.GetX(ball) < 0)
                {
                    dataApi.ReverseHorizontalMove(ball);
                }
                if (dataApi.GetY(ball) < 0)
                {
                    dataApi.ReverseVerticalMove(ball);
                }

                if (dataApi.GetX(ball) + IBall.Radius > 500)
                {
                    dataApi.ReverseHorizontalMove(ball);
                }
                if (dataApi.GetY(ball) + IBall.Radius > 500)
                {
                    dataApi.ReverseVerticalMove(ball);
                }
            }

            private void CheckCollisionWithBalls(IBall ball)
            {
                for (int i = 0; i < GetNumberOfBalls(); i++)
                {
                    if (GetX(ball) != GetX(i) && GetY(ball) != GetY(i))
                    {
                        double d = Math.Sqrt(Math.Pow(GetX(i) - GetX(ball), 2) + Math.Pow(GetY(i) - GetY(ball), 2));
                        if (d - (IBall.Radius) <= 0)
                        {
                            double hv1 = ((GetHorizontalSpeed(ball) * (GetWeight(ball) - GetWeight(i)) + 2 * GetWeight(i) * GetHorizontalSpeed(i)) / (GetWeight(ball) + GetWeight(i)));
                            double vv1 = ((GetVerticalSpeed(ball) * (GetWeight(ball) - GetWeight(i)) + 2 * GetWeight(i) * GetVerticalSpeed(i)) / (GetWeight(ball) + GetWeight(i)));

                            double hv2 = ((GetHorizontalSpeed(i) * (GetWeight(i) - GetWeight(ball)) + 2 * GetWeight(ball) * GetHorizontalSpeed(ball)) / (GetWeight(i) + GetWeight(ball)));
                            double vv2 = ((GetVerticalSpeed(i) * (GetWeight(i) - GetWeight(ball)) + 2 * GetWeight(ball) * GetVerticalSpeed(ball)) / (GetWeight(i) + GetWeight(ball)));

                            dataApi.SetHorizontalMove(ball, hv1);
                            dataApi.SetVerticalMove(ball, vv1);

                            dataApi.SetHorizontalMove(i, hv2);
                            dataApi.SetVerticalMove(i, vv2);
                        }
                    }
                }
            }
        }
    }
}
