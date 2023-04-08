using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        ModelApi Api { get; set; }
        private ObservableCollection<BallModel> _balls = new ObservableCollection<BallModel>();
        public ObservableCollection<BallModel> Balls 
        {
            get
            {
                return _balls;
            }
            set 
            { 
                _balls = value;
                OnPropertyChanged(nameof(Balls));
            }
        }

        private static System.Timers.Timer aTimer;
        private int number;

        public int NumberOfBalls 
        {
            get
            {
                return number;
            }
            set 
            {
                number = value;
                OnPropertyChanged(nameof(NumberOfBalls));
            }
        }

        public ICommand AddCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainViewModel() 
        {
            Api = ModelApi.Instance();
            Balls = Api.balls;
            AddCommand = new RelayCommand(AddBalls);
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000 / 60;
            aTimer.Elapsed += OnUpdate;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnUpdate(Object source, System.Timers.ElapsedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            Api.updatePosition();
        }

        public void AddBalls() 
        {
            Api.AddBalls(NumberOfBalls);
        }
    }
}