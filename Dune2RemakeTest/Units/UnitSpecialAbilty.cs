using System;

namespace Dune2RemakeTest.Units
{
    [Flags]
    public enum UnitSpecialAbilty
    {
        Invisible = 1,
        AutoMove = 2,
        NoSelect = 4,
        SpiceHarvest = 8,
        Indestructable = 16,
        CanRunOver = 32
    }
}
