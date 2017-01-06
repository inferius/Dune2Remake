using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Dune2RemakeTest.Graphics;
using Dune2RemakeTest.Units.Events;

namespace Dune2RemakeTest.Units
{
    public class Unit
    {
        #region Events
        public delegate void ChangeDirectionHandler(object sender, Events.ChangeDirectionEventArgs e);
        public event ChangeDirectionHandler OnChangeDirection;

        public delegate void ChangePositionHandler(object sender, Events.ChangePositionEventArgs e);
        public event ChangePositionHandler OnChangePosition;
        #endregion
        public uint Price { get; protected set; }
        public uint BuildTime { get; protected set; }
        public uint AttackPower { get; protected set; }
        public uint AttackSpeed { get; protected set; }
        /// <summary>
        /// Rychlost otaceni v ms
        /// </summary>
        public uint RotateSpeed { get; protected set; }
        /// <summary>
        /// Rychlost otaceni kanonu
        /// </summary>
        public uint CanonRotateSpeed { get; protected set; }
        /// <summary>
        /// Počet milisekund za kterou prejede jedno pole
        /// </summary>
        public uint MoveSpeed { get; protected set; }
        /// <summary>
        /// Aktuální rychlost kterou se jednotka pohybuje
        /// </summary>
        public uint CurrentMoveSpeed { get; protected set; }
        /// <summary>
        /// Počet milisekund za kterou se stroj dostane na plnou rychlost
        /// </summary>
        public uint Acceleration { get; protected set; }
        /// <summary>
        /// Obsahuje seznam typu povrchu na který jednotka může vjet
        /// </summary>
        public List<TerrainType> CanMoveOnTerrainTypes { get; protected set; }
        /// <summary>
        /// Počet aktuálního zdraví jednotky
        /// </summary>
        public int Life { get; protected set; }
        private int _maxLife;
        /// <summary>
        /// Počet maximálních životů jednotky
        /// </summary>
        public int MaxLife
        {
            get { return _maxLife; }
            protected set
            {
                _maxLife = value;
                Life = _maxLife;
            }
        }
        public UnitSpecialAbilty SpecialAbility { get; protected set; }
        public List<UnitAction> ActionMenu { get; protected set; }
        public List<Buildings.Building> Prerquisites { get; protected set; }
        public Buildings.Building ProductionBuilding { get; protected set; }
        public List<Houses.House> HousesAvailable { get; protected set; }
        public string Name { get; protected set; }
        public string MentatText { get; protected set; }
        /// <summary>
        /// Celociselna hodnota symbolizujici jednotky procent kolik ma dana jednotka rychlostni bonus na danem povrchu
        /// </summary>
        public Dictionary<TerrainType, int> TerrainTypeMoveSpeedBonus { get; protected set; }

        Point _currentPosition = new Point();

        public Point CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                var ea = new ChangePositionEventArgs(_currentPosition, value);

                _currentPosition = value;
                if (OnChangePosition != null)
                    OnChangePosition(this, ea);
            }
        }

        UnitDirection _currentDirection;
        public UnitDirection CurrentDirection
        {
            get { return _currentDirection; }
            set
            {
                var ea = new ChangeDirectionEventArgs(_currentDirection, value);

                _currentDirection = value;
                if (OnChangeDirection != null)
                    OnChangeDirection(this, ea);
            }
        }
        public UnitDirection CanonDirection { get; protected set; }
        /// <summary>
        /// Indikuje, zda je kanon synchrozniovany z pohybem nebo ne. Pokud true, kanon se otaci s vozidlem (miri vzdy stejne kam vuz jede)
        /// </summary>
        public bool CannonSynchronize { get; protected set; }
        public bool IsMoving { get; protected set; }
        public bool IsFire { get; protected set; }

        public Uri BaseImage { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        private Dictionary<UnitDirection, TranslateTransform> _positionTransform = new Dictionary<UnitDirection, TranslateTransform>();
        public Dictionary<UnitDirection, TranslateTransform> PositionTransform
        {
            get { return _positionTransform; }
            set { _positionTransform = value; }
        }

        public Unit()
        {
            CurrentDirection = UnitDirection.East;
            TerrainTypeMoveSpeedBonus = new Dictionary<TerrainType, int>
            {
                {TerrainType.Sand, -50},
                {TerrainType.Rock, 0},
                {TerrainType.Pavement, 150}
            };

            CanMoveOnTerrainTypes = new List<TerrainType>
            {
                TerrainType.Pavement,
                TerrainType.Rock,
                TerrainType.Sand,
                TerrainType.Spice
            };
        }

        public void TurnLeft()
        {
            if (CurrentDirection == UnitDirection.NorthEast) CurrentDirection = UnitDirection.North;
            else CurrentDirection++;

            if (CannonSynchronize) CanonDirection = CurrentDirection;
        }

        public void TurnRight()
        {
            if (CurrentDirection == UnitDirection.North) CurrentDirection = UnitDirection.NorthEast;
            else CurrentDirection--;

            if (CannonSynchronize) CanonDirection = CurrentDirection;
        }

        public void CanonTurnLeft()
        {
            if (CanonDirection == UnitDirection.NorthEast) CanonDirection = UnitDirection.North;
            else CanonDirection++;
        }

        public void CanonTurnRight()
        {
            if (CanonDirection == UnitDirection.North) CanonDirection = UnitDirection.NorthEast;
            else CanonDirection--;
        }
    }


    public enum UnitDirection
    {
        North = 0,
        South = 4,
        East = 6,
        West = 2,
        NorthWest = 1,
        NorthEast = 7,
        SouthEast = 5,
        SouthWest = 3
    }
}
