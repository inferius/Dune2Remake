using System;
using System.Windows.Media;

namespace Dune2RemakeTest.Units
{
    public class Devastator : Unit
    {
        public Devastator()
        {
            Name = "Devastator";
            Height = 80;
            //Width = 66;
            Width = 52;
            BaseImage = new Uri(@"Resources\Units\devastator_e.png", UriKind.Relative);
            MoveSpeed = 2000;

            PositionTransform.Add(UnitDirection.East, new TranslateTransform(-7, 0));
            PositionTransform.Add(UnitDirection.NorthEast, new TranslateTransform(-60, 0));
            PositionTransform.Add(UnitDirection.North, new TranslateTransform(-120, 0));
            PositionTransform.Add(UnitDirection.NorthWest, new TranslateTransform(-173, 0));
            PositionTransform.Add(UnitDirection.West, new TranslateTransform(-224, 0));
            PositionTransform.Add(UnitDirection.SouthWest, new TranslateTransform(-289, 0));
            PositionTransform.Add(UnitDirection.South, new TranslateTransform(-346, 0));
            PositionTransform.Add(UnitDirection.SouthEast, new TranslateTransform(-397, 0));

        }
    }
}
