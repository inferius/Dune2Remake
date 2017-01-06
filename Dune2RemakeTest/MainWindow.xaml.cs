using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dune2RemakeTest.Units;

namespace Dune2RemakeTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly DebugWindow _debugWindowd = new DebugWindow();
        MusicManager _musicManager = new MusicManager();
        internal Graphics.Unit SelectedUnit;
        //internal Render _render;

        public MainWindow()
        {
            InitializeComponent();
            _debugWindowd.Left = SystemParameters.PrimaryScreenWidth - _debugWindowd.Width;
            _debugWindowd.Top = SystemParameters.PrimaryScreenHeight - _debugWindowd.Height;
            _debugWindowd.Show();
            _musicManager.AddMusic(new Uri(@"Resources\Music\building1.MID", UriKind.Relative));
            _musicManager.AddMusic(new Uri(@"Resources\Music\building0.MID", UriKind.Relative));
            _musicManager.AddMusic(new Uri(@"Resources\Music\building2.MID", UriKind.Relative));
            _musicManager.Play();

            TerrainGenerator t = new TerrainGenerator(this);
            UnitGenerator ug = new UnitGenerator(this);


            ug.AddUnit(new Devastator(), new Point(30, 50));
            ug.AddUnit(new Devastator(), new Point(30, 100));
        }

        private void MainCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            _debugWindowd.CursorPosition.Content = e.GetPosition(sender as Canvas).ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _debugWindowd.Close();
            _musicManager.Stop();
            Environment.Exit(Environment.ExitCode);
        }

        private void MainCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
