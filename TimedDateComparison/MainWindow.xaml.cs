using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace TimedDateComparison
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const int CheckPeriodInSeconds = 4;

        public SolidColorBrush DateColour { get; private set; }
        public DateTime RegisteredSubkey { get; private set; }
        public DateTime CurrentDate => DateTime.Now;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();

            var dateInterval = new TimeSpan(0, 0, CheckPeriodInSeconds / 2);
            new DispatcherTimer(dateInterval, DispatcherPriority.Background, CheckDate, Dispatcher.CurrentDispatcher);

            var colorInterval = new TimeSpan(0, 0, CheckPeriodInSeconds);
            new DispatcherTimer(colorInterval, DispatcherPriority.Background, CheckColour, Dispatcher.CurrentDispatcher);

            CheckColour(this, EventArgs.Empty);
        }

        private void InitializeWindow()
        {
            RegisteredSubkey = DateTime.Today.AddDays(-1);
            NotifyPropertyChanged(nameof(RegisteredSubkey));
        }

        private void CheckDate(object sender, EventArgs e)
        {
            if (RegisteredSubkey.Equals(DateTime.Today))
            {
                return;
            }

            RegisteredSubkey = DateTime.Today;
            NotifyPropertyChanged(nameof(RegisteredSubkey));
        }

        private void CheckColour(object sender, EventArgs e)
        {
            DateColour = RegisteredSubkey < DateTime.Today
                ? Brushes.Red
                : Brushes.LimeGreen;
            NotifyPropertyChanged(nameof(DateColour));
        }
    }
}
