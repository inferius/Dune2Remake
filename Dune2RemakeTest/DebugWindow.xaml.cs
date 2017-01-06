using System.Windows;

namespace Dune2RemakeTest
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public Graphics.Unit Unit { get; set; }
        public DebugWindow()
        {
            InitializeComponent();
        }

        private void TurnRight_OnClick(object sender, RoutedEventArgs e)
        {
            if (Unit == null) return;
            Unit.TurnRight();
        }

        private void TurnLeft_OnClick(object sender, RoutedEventArgs e)
        {
            if (Unit == null) return;
            Unit.TurnLeft();
        }

        private void TranslateManualButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Unit == null) return;
            double dbl = 0;
            if (double.TryParse(ValueTranslaTextBox.Text, out dbl))
            {
                Unit.DebugTransform(dbl);
            }
        }
    }
}
