using Logic;
using System.ComponentModel;

namespace Model
{
    public class BallModel : INotifyPropertyChanged
    {
        public String Color { get; set; }
        private float x;
        public float X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }
        private float y;
        public float Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }
        public float Radious
        {
            get { return 50; }
        }


        public BallModel(string color = "White")
        {
            Color = color;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}