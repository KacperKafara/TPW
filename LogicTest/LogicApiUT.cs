using Logic;

namespace LogicTest
{
    [TestClass]
    public class LogicApiUT
    {
        [TestMethod]
        public void InstanceTest()
        {
            LogicApi logicApi = LogicApi.Instance();
            Assert.IsNotNull(logicApi);
            Assert.AreEqual(logicApi.Board.Width, 500);
            Assert.AreEqual(logicApi.Board.Height, 500);
        }

        [TestMethod]
        public void AddBallTest()
        {
            LogicApi logicApi = LogicApi.Instance();
            Assert.IsNotNull(logicApi);
            Ball ball = new Ball(20, 20, 10);
            Ball ball1 = new Ball(10, 10, 20);
            logicApi.AddBall(ball);
            Assert.AreEqual(logicApi.Board.Balls.Count, 1);
            logicApi.AddBall(ball1);
            Assert.AreEqual(logicApi.Board.Balls.Count, 2);
        }

        [TestMethod]
        public void UpdatePositionTest()
        {
            LogicApi logicApi = LogicApi.Instance();
            Assert.IsNotNull(logicApi);
            Ball ball = new Ball(20, 20, 10);
            logicApi.AddBall(ball);
            Assert.AreEqual(ball.X, 20);
            Assert.AreEqual(ball.Y, 20);
            logicApi.updatePosition();
            Assert.AreNotEqual(ball.X, 20);
            Assert.AreNotEqual(ball.Y, 20);
        }
    }
}