using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel
    {
        ModelApi Api { get; set; }
        public ObservableCollection<BallModel> Balls { get; set; }

        private static System.Timers.Timer aTimer;

        public int NumberOfBalls { get; set; }

        public ICommand AddCommand { get; set; }

        public MainViewModel() 
        {
            Api = ModelApi.Instance();
            Balls = Api.balls;
            AddCommand = new RelayCommand(AddBalls);
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000 / 30;
            aTimer.Elapsed += OnUpdate;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnUpdate(Object source, System.Timers.ElapsedEventArgs e)
        {
            Api.updatePosition();
        }

        public void AddBalls() 
        {
            Api.AddBalls(NumberOfBalls);
        }
    }
}