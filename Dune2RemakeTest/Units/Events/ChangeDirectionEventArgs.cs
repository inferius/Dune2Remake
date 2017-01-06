using System;

namespace Dune2RemakeTest.Units.Events
{
    public class ChangeDirectionEventArgs : EventArgs
    {
        public UnitDirection OldDirection { get; private set; }
        public UnitDirection Direction { get; private set; }

        public ChangeDirectionEventArgs(UnitDirection oldDirection, UnitDirection direction)
        {
            OldDirection = oldDirection;
            Direction = direction;
        }
    }
}
