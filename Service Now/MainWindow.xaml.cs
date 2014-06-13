using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Service_Now
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void WindowLoaded( object sender , RoutedEventArgs e )
        {
            this.ServiceNowIcon.TrayLeftMouseUp += this.ServiceNowIconTrayLeftMouseUp;
            this.Position();
        }

        private void ServiceNowIconTrayLeftMouseUp( object sender , RoutedEventArgs e )
        {
            if( this.WindowState == WindowState.Normal )
            {
                this.WindowState = WindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.ShowInTaskbar = true;
                this.Position();
            }
        }

        protected override void OnClosing( CancelEventArgs e )
        {
            this.ServiceNowIcon.Dispose();
            base.OnClosing( e );
        }

        private void Position()
        {
            double width = this.ActualWidth + 20;
            double height = this.ActualHeight + 20;
            double screenheight = SystemParameters.PrimaryScreenHeight;
            double screenwidth = SystemParameters.PrimaryScreenWidth;
            double top = ( screenheight / 2 ) - ( height / 2 );
            double left = screenwidth - width;
            this.Top = top;
            this.Left = left;
        }

        private void CreateIncidentButtonClick( object sender , RoutedEventArgs e )
        {
            this.MainContent.Child = new Form();
            this.Left = this.Left - ( 700 - this.Width );
            this.Top = this.Top - ( ( 750 - this.Height ) / 2 );
            this.Height = 750;
            this.Width = 700;
            this.RequestChoicePanel.Visibility = Visibility.Hidden;
            this.RequestPanel.Visibility = Visibility.Visible;
        }

        private void MinimizeButtonClick( object sender , RoutedEventArgs e )
        {
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void CloseButtonClick( object sender , RoutedEventArgs e )
        {
            this.Close();
        }

        private void WindowHeaderBarMouseDown( object sender , MouseButtonEventArgs e )
        {
            this.DragMove();
        }

        private void CancelButtonClick( object sender , RoutedEventArgs e )
        {
            this.Height = 550;
            this.Width = 400;
            this.MainContent.Child = null;
            this.RequestChoicePanel.Visibility = Visibility.Visible;
            this.RequestPanel.Visibility = Visibility.Hidden;
            this.Position();
        }
    }
}