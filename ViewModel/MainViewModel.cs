using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel
    {
        ModelApi Api { get; set; }
        public ObservableCollection<BallModel> Balls { get; set; }

        public int NumberOfBalls { get; set; }

        public ICommand AddCommand { get; set; }

        public MainViewModel() 
        {
            Api = ModelApi.Instance();
            Balls = Api.balls;
            AddCommand = new RelayCommand(AddBalls);
        }

        public void AddBalls() 
        {
            Api.AddBalls(NumberOfBalls);
        }
    }
}