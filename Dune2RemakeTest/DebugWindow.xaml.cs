using System.Windows;

namespace Dune2RemakeTest
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        Graphics.Unit _unit;
        public Graphics.Unit Unit {
            get { return _unit; }
            set {
                if (_unit != null) _unit.OnUnitSelectionChanged -= Unit_OnUnitSelectionChanged;
                _unit = value;
                if (_unit != null) _unit.OnUnitSelectionChanged += Unit_OnUnitSelectionChanged;
                Unit_OnUnitSelectionChanged(null, null);
            }
        }
        public DebugWindow()
        {
            InitializeComponent();
        }

        private void Unit_OnUnitSelectionChanged(object sender, Events.UnitSelectionChanged e)
        {
            vehicleTurnState.Content = Unit.CurrentUnit.CurrentDirection.ToString();
            cannonTurnState.Content = Unit.CurrentUnit.CannonDirection.ToString();
        }

        private void TurnRight_OnClick(object sender, RoutedEventArgs e)
        {
            if (Unit == null) return;
            Unit.TurnRight();
            vehicleTurnState.Content = Unit.CurrentUnit.CurrentDirection.ToString();
        }

        private void TurnLeft_OnClick(object sender, RoutedEventArgs e)
        {
            if (Unit == null) return;
            Unit.TurnLeft();
            vehicleTurnState.Content = Unit.CurrentUnit.CurrentDirection.ToString();
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

        private void TurnCannonLeft_Click(object sender, RoutedEventArgs e)
        {
            if (Unit == null) return;
            Unit.TurnCannonLeft();
            cannonTurnState.Content = Unit.CurrentUnit.CannonDirection.ToString();
        }

        private void TurnCannonRight_Click(object sender, RoutedEventArgs e)
        {
            if (Unit == null) return;
            Unit.TurnCannonRight();
            cannonTurnState.Content = Unit.CurrentUnit.CannonDirection.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tw = new TestWB();
            tw.Show();
        }
    }
}
