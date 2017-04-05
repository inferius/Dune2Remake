using System;
using System.Windows.Media;

namespace Dune2RemakeTest.Units
{
    public class Tank : Unit
    {
        public Tank()
        {
            Name = "Tank";
            Height = 80;
            //Width = 66;
            Width = 52;
            BaseImage = new Uri(@"Resources\Units\tank_body_e.png", UriKind.Relative);
            BaseCannonImage = new Uri(@"Resources\Units\tank_cannon_e.png", UriKind.Relative);
            MoveSpeed = 2000;

            PositionTransform.Add(UnitDirection.East, new TranslateTransform(-7, 0)); // Default
            PositionTransform.Add(UnitDirection.NorthEast, new TranslateTransform(-60, 0));
            PositionTransform.Add(UnitDirection.North, new TranslateTransform(-120, 0));
            PositionTransform.Add(UnitDirection.NorthWest, new TranslateTransform(-173, 0));
            PositionTransform.Add(UnitDirection.West, new TranslateTransform(-224, 0));
            PositionTransform.Add(UnitDirection.SouthWest, new TranslateTransform(-289, 0));
            PositionTransform.Add(UnitDirection.South, new TranslateTransform(-346, 0));
            PositionTransform.Add(UnitDirection.SouthEast, new TranslateTransform(-397, 0));

            CannonPositionTransform.Add(UnitDirection.East, new TranslateTransform(0, 6));
            CannonPositionTransform.Add(UnitDirection.NorthEast, new TranslateTransform(-58, 6));
            CannonPositionTransform.Add(UnitDirection.North, new TranslateTransform(-114, 6));
            CannonPositionTransform.Add(UnitDirection.NorthWest, new TranslateTransform(-170, 6));
            CannonPositionTransform.Add(UnitDirection.West, new TranslateTransform(-224, 6));
            CannonPositionTransform.Add(UnitDirection.SouthWest, new TranslateTransform(-282, 6));
            CannonPositionTransform.Add(UnitDirection.South, new TranslateTransform(-338, 6));
            CannonPositionTransform.Add(UnitDirection.SouthEast, new TranslateTransform(-397, 6));

        }
    }
}
