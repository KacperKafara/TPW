using System.Collections.ObjectModel;
using Logic;

namespace Model
{
    public abstract class ModelApi
    {
        public LogicApi logicApi;
        public ObservableCollection<BallModel> balls;
        public abstract void AddBalls(int number);
        public static ModelApi Instance()
        {
            return new Model();
        }

        private class Model : ModelApi
        {
            public Model() 
            {   
                balls = new ObservableCollection<BallModel>();
                logicApi = LogicApi.Instance();
            }

            public override void AddBalls(int number)
            {
                Random rnd = new Random();
                for (int i = 0; i < number; i++)
                {
                    Ball ball = new Ball(rnd.Next(300) + 100, rnd.Next(300) + 100);
                    logicApi.AddBall(ball);
                    balls.Add(new BallModel());
                }
            }
        }
    }
}
