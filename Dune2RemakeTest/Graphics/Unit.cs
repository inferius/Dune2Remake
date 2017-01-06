using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Dune2RemakeTest.Events;
using Dune2RemakeTest.Units;
using Dune2RemakeTest.Units.Events;

namespace Dune2RemakeTest.Graphics
{
    public class Unit : Canvas
    {
        #region Events
        public delegate void UnitSelectionChangedHandler(object sender, Events.UnitSelectionChanged e);
        public event UnitSelectionChangedHandler OnUnitSelectionChanged;
        #endregion

        private Units.Unit _unit;
        //public Units.Unit CurrentUnit { get { return _unit; } private set { _unit = value; } }

        private SoundPlayer _sp = new SoundPlayer();
        internal Graphics.Unit _SelectedUnit;
        private Canvas _back_canvas;
        private double _offset_x = -7;

        public Unit(Units.Unit unit) {
            _unit = unit;
            Width = _unit.Width;
            Height = _unit.Height;
            SetValue(LeftProperty, _unit.CurrentPosition.X);
            SetValue(TopProperty, _unit.CurrentPosition.Y);
            Image i = new Image();

            _back_canvas = new Canvas();
            _back_canvas.Width = (9 * _unit.Width) - 15;
            _back_canvas.Height = _unit.Height - 10;
            Children.Add(_back_canvas);
            ClipToBounds = true;

            _back_canvas.Background = new ImageBrush()
            {
                ImageSource = new BitmapImage(_unit.BaseImage),
                Stretch = Stretch.UniformToFill,
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Transform = _unit.PositionTransform[UnitDirection.East]
            };

            MouseLeftButtonUp += Unit_MouseLeftButtonUp;
            _unit.OnChangeDirection += UnitOnOnChangeDirection;
        }

        #region Zpracovani udalosti
        private void UnitOnOnChangeDirection(object sender, ChangeDirectionEventArgs changeDirectionEventArgs)
        {
            //_back_canvas.Background.Transform = new TranslateTransform(((double)((int)changeDirectionEventArgs.Direction * Width) * -1 + ((int)changeDirectionEventArgs.Direction) * _offset_x), 0);
            _back_canvas.Background.Transform = _unit.PositionTransform[changeDirectionEventArgs.Direction];
        }

        public void DebugTransform(double dbl)
        {
            _back_canvas.Background.Transform = new TranslateTransform(dbl, 0);
        }

        void Unit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _sp.SoundLocation = (Path.Combine(Environment.CurrentDirectory, "Resources\\Sound\\units\\Sound_reporting.wav"));
            _sp.Load();
            _sp.Play();

            if (OnUnitSelectionChanged != null) OnUnitSelectionChanged(this, new UnitSelectionChanged(this));
        }
        #endregion

        private int _completed_animation = 0;
        private int _index_animation = 0;
        private MoveAnimations[] _animations_list;
        private int last_x = 0;
        private int last_y = 0;
        public void MoveTo(Canvas canvas)
        {
            if (canvas == this) return;
            //if (canvas.Tag != null)
            //{
            //    var target_point = Point.Parse(canvas.Tag as string);
            //    var range = Math.Sqrt(Math.Pow(target_point.X - _unit.CurrentPosition.X, 2f) + Math.Pow(target_point.Y - _unit.CurrentPosition.Y, 2f));


            //}
            //var p_target = new Point((double)canvas.GetValue(LeftProperty), (double)canvas.GetValue(TopProperty));
            //var p_source = new Point((double)GetValue(LeftProperty), (double)GetValue(TopProperty));
            //var time_one_pix = _unit.MoveSpeed / TerrainGenerator.OneFieldSize;
            //var time_move = Math.Abs(p_source.X - p_target.X) * time_one_pix;

            ////// DEBUG MOVE
            ////Vector offset = VisualTreeHelper.GetOffset(canvas);
            ////Vector offset2 = VisualTreeHelper.GetOffset(this);

            TranslateTransform trans = new TranslateTransform();
            this.RenderTransform = trans;
            //DoubleAnimation anim = new DoubleAnimation(p_source.X, p_target.X - 80, TimeSpan.FromMilliseconds(time_move));
            //trans.BeginAnimation(TranslateTransform.XProperty, anim);
            var t = canvas as Terrain;
            var p = new Point((double)GetValue(LeftProperty), (double)GetValue(TopProperty));
            var start = t.TerrainManager.GetCanvasTerrainByPoint(p);
            _animations_list = getMoveAnimations(start, t.X, t.Y);



            if (_animations_list[_index_animation].XAnimation != null)
            {
                _animations_list[_index_animation].XAnimation.Completed += XAnimationOnCompleted;
                trans.BeginAnimation(TranslateTransform.XProperty, _animations_list[_index_animation].XAnimation);
            }
            if (_animations_list[_index_animation].YAnimation != null)
            {
                _animations_list[_index_animation].YAnimation.Completed += XAnimationOnCompleted;
                trans.BeginAnimation(TranslateTransform.YProperty, _animations_list[_index_animation].YAnimation);
            }


        }

        private void XAnimationOnCompleted(object sender, EventArgs eventArgs)
        {
            if (++_completed_animation == 2)
            {
                _index_animation++;
                if (_animations_list.Length <= _index_animation) return;

                TranslateTransform trans = new TranslateTransform();
                RenderTransform = trans;

                _completed_animation = 0;
                //if (_animations_list[_index_animation - 1].XAnimation != null)
                //    SetValue(LeftProperty, _animations_list[_index_animation - 1].XAnimation.To);

                //if (_animations_list[_index_animation - 1].YAnimation != null)
                //    SetValue(LeftProperty, _animations_list[_index_animation - 1].YAnimation.To);

                if (_animations_list[_index_animation].XAnimation != null)
                {
                    _animations_list[_index_animation].XAnimation.Completed += XAnimationOnCompleted;
                    trans.BeginAnimation(TranslateTransform.XProperty, _animations_list[_index_animation].XAnimation);
                }
                if (_animations_list[_index_animation].YAnimation != null)
                {
                    _animations_list[_index_animation].YAnimation.Completed += XAnimationOnCompleted;
                    trans.BeginAnimation(TranslateTransform.YProperty, _animations_list[_index_animation].YAnimation);
                }
            }
        }


        private Terrain getShorterNextCanvas(Terrain start, int target_x, int target_y)
        {
            //CanMoveOnTerrainTypes
            var appl_x = 0;
            var appl_y = 0;

            if (start.X > target_x) appl_x = -1;
            if (start.X < target_x) appl_x = 1;
            if (start.Y > target_y) appl_y = -1;
            if (start.Y < target_y) appl_y = 1;

            var next_x = start.X + appl_x;
            var next_y = start.Y + appl_y;
            //var coord = string.Format("{0};{1}", next_x, next_y);
            var next_canvas = start.TerrainManager.GetCanvasTerrainByBasicCoordinate(next_x, next_y);

            // Testuje zda jednotka na dane pole muze vjet
            if (!_unit.CanMoveOnTerrainTypes.Any(item => next_canvas.Type == item))
            {
                // zrušit vjezd na toto pole
            }
            else
            {
                //break;
                return next_canvas;
            }

            return null;
        }

        private MoveAnimations[] getMoveAnimations(Terrain start, int target_x, int target_y)
        {
            var r = new List<MoveAnimations>();
            var s = start;
            var s2 = start;
            var i = 0;


            while (true)
            {
                var time_one_pix = _unit.MoveSpeed / TerrainGenerator.OneFieldSize;
                // nacteni bonusu k pohybu
                if (_unit.TerrainTypeMoveSpeedBonus.ContainsKey(start.Type)) time_one_pix *= 1 + (_unit.TerrainTypeMoveSpeedBonus[start.Type] / 100);

                MoveAnimations ma = new MoveAnimations();
                s2 = getShorterNextCanvas(s, target_x, target_y);
                var time_move_x = Math.Abs(s.Position.X - s2.Position.X) * time_one_pix;
                var time_move_y = Math.Abs(s.Position.Y - s2.Position.Y) * time_one_pix;

                if (s2.X != s.X) ma.XAnimation = new DoubleAnimation(s.Position.X, s2.Position.X, TimeSpan.FromMilliseconds(time_move_x));
                if (s2.Y != s.Y) ma.YAnimation = new DoubleAnimation(s.Position.Y, s2.Position.Y, TimeSpan.FromMilliseconds(time_move_y));
                //EasingDoubleKeyFrame  easingStart = new EasingDoubleKeyFrame();
                //easingStart.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(_unit.Acceleration));
                //easingStart.Value = TerrainGenerator.OneFieldSize/4;
                //ElasticEase ease = new ElasticEase();
                //ease.EasingMode = EasingMode.EaseIn;

                r.Add(ma);
                s = s2;
                i++;
                if (s2.X == target_x && s2.Y == target_y) break;

                if (i == 3000) throw new Exception("Nebyla nalezena cesta");
            }

            return r.ToArray();
        }

        public void Accelerate()
        {
            
        }

        public void TurnLeft()
        {
            _unit.TurnLeft();
        }

        public void TurnRight()
        {
            _unit.TurnRight();
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} x {2} - Action: ", _unit.Name, _unit.CurrentPosition.X,
                _unit.CurrentPosition.Y);
        }
    }

    public struct MoveAnimations
    {
        public DoubleAnimation XAnimation { get; set; }
        public DoubleAnimation YAnimation { get; set; }
    }

    public struct PathToken
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
    }
}
