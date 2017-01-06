using System;

namespace Dune2RemakeTest.Events
{
    public class UnitSelectionChanged : EventArgs
    {
        public Graphics.Unit SelectedUnit { get; private set; }

        public UnitSelectionChanged(Graphics.Unit selectedUnit)
        {
            SelectedUnit = selectedUnit;
        }
    }
}
