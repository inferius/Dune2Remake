namespace Dune2RemakeTest.Buildings
{
    public class Base
    {
        public bool CanBuild { get; protected set; }
        public int MoveSpeed { get; protected set; }
        public int BuildLife { get; protected set; }
    }

    public enum BaseType
    {
        Sand,
        Rock,
        Paving
    }
}
