using System;
using System.Windows;

namespace Dune2RemakeTest.Units.Events
{
    public class ChangePositionEventArgs : EventArgs
    {
        public Point OldPosition { get; private set; }
        public Point Position { get; private set; }

        public ChangePositionEventArgs(Point oldPosition, Point position)
        {
            OldPosition = oldPosition;
            Position = position;
        }
    }
}
