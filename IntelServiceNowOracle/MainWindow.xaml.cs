using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using IntelServiceNow;
using IntelServiceNow.Enums;
using IntelServiceNow.Interfaces;

namespace IntelServiceNowOracle
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

        private void WindowLoaded1( object sender , RoutedEventArgs e )
        {
            Factory.OnFrameworkErrorOccurred += this.OnFrameworkErrorOccurred;
            Factory.Init();

            //IServiceLine line = null;

            //List < IServiceLine > servicelines = Factory.GetServiceLines();

            //foreach( IServiceLine serviceline in servicelines )
            //{
                //if( serviceline.Shortcode.Equals( "labos" ) )
                //{
                    //line = serviceline;
                //}
            //}

            IUser customer = Factory.GetUser( "11302960" );
            //IUser faceless = Factory.GetUser( "faceless" );

            //IItem incident = Factory.CreateIncident();
            //incident.Customer = customer;
            //incident.Requester = faceless;
            //incident.Description = "test subject";
            //incident.DetailedDescription = "test description";
            //incident.Impact = Impact.Low;
            //incident.Priority = Priority.Low;
            //incident.ServiceLine = line;

            IWeb webhand = Factory.GetWebHandler();
            webhand.OnWebLoginRequired += this.OnWebLoginRequired;
            webhand.OnWebErrorOccurred += this.OnWebErrorOccurred;
            webhand.OnWebResponseReceived += this.OnWebResponseReceived;
            webhand.Username = "dmoneil2";
            webhand.Password = "testpassword";
            //webhand.SubmitIncident( incident );

            webhand.QueryAssignedIncidents( customer );

            //IItem request = Factory.CreateRequest();
            //request.Customer = customer;
            //request.Requester = faceless;
            //request.Description = "test subject";
            //request.DetailedDescription = "test description";
            //request.Impact = Impact.Low;
            //request.Priority = Priority.Low;
            //request.ServiceLine = line;

            //webhand = Factory.GetWebHandler();
            //webhand.OnWebLoginRequired += this.OnWebLoginRequired;
            //webhand.OnWebErrorOccurred += this.OnWebErrorOccurred;
            //webhand.OnWebResponseReceived += this.OnWebResponseReceived;
            //webhand.MakeRequest( request );
        }

        private void OnWebLoginRequired( IWeb webhand )
        {
            MessageBox.Show( "Login required" );
            MessageBox.Show(webhand.Error.Message);
            //webhand.Username = "dmoneil2";
            //webhand.Password = "testpassword";
            //webhand.MakeRequest( webhand.Payload );
        }

        private void OnFrameworkErrorOccurred( string error )
        {
            MessageBox.Show( error );
        }

        private void OnWebResponseReceived( IWeb webhand )
        {
            MessageBox.Show( webhand.Response );
        }

        private void OnWebErrorOccurred( IWeb webhand )
        {
            MessageBox.Show( webhand.Error.Message );
        }

        private void WindowClosing1( object sender , CancelEventArgs e )
        {
            Factory.DeInit();
        }
    }
}
