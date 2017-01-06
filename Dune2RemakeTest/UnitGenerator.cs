using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dune2RemakeTest
{
    public class UnitGenerator
    {
        private MainWindow _mainWindow;
        public Graphics.Unit SelectedUnit { get; private set; }
        public bool UnitBorder { get; set; }

        public UnitGenerator(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            UnitBorder = false;
        }

        public void AddUnit(Units.Unit unit, Point _point)
        {
            unit.CurrentPosition = _point;
            var c = new Graphics.Unit(unit);

            c.OnUnitSelectionChanged += (s, e) =>
            {
                SelectedUnit = e.SelectedUnit;

                _mainWindow._debugWindowd.SelectedUnitLabel.Content = e.SelectedUnit.ToString();
                _mainWindow._debugWindowd.Unit = e.SelectedUnit;
                _mainWindow.SelectedUnit = e.SelectedUnit;
            };

            if (UnitBorder)
            {
                Border b = new Border();
                b.BorderThickness = new Thickness(1);
                b.Child = c;
                b.BorderBrush = new SolidColorBrush(Color.FromRgb(255,255,255));
                _mainWindow.MainCanvas.Children.Add(b);
            }
            else
            {
                _mainWindow.MainCanvas.Children.Add(c);
            }
        }
    }
}
