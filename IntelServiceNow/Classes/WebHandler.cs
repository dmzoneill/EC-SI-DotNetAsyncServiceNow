using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using IntelServiceNow.Delegates;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow.Classes
{
    internal class WebHandler : IWeb
    {
        private static int _handlercount;
        private static CookieContainer _cookieContainer;

        static WebHandler()
        {
            _cookieContainer = new CookieContainer();
            _handlercount = 0;
        }

        public WebHandler()
        {
            this.Id = ++_handlercount;
        }

        private string SoapUrl
        {
            get;
            set;
        }

        #region IWeb Members

        public string Response
        {
            get;
            private set;
        }

        public IItem Payload
        {
            get;
            private set;
        }

        public Exception Error
        {
            get;
            private set;
        }

        public int SubmitIncident( IItem snincident )
        {
            this.SoapUrl = "https://e2esm-int.intel.com/IncidentManagement_v1.do?SOAP";
            return this.MakeRequest( snincident );
        }

        public int QueryIncident( string incidentno )
        {
            throw new NotImplementedException();
        }

        public int QueryAssignedIncidents( IUser assignee )
        {
            this.SoapUrl = "https://e2esm.intel.com/incident_list.do?CSV&sysparm_query=active=true^assigned_to=javascript:getMyAssignments()^incident_state!=6^EQ&active=true";
            
            return this.MakeRequest();
        }

        public int UpdateIncident( string incidentno , string statusupdate )
        {
            throw new NotImplementedException();
        }

        public int AssignIncident( string incidentno , IUser user )
        {
            throw new NotImplementedException();
        }

        public int ResolveIncident( string incidentno , string resolution )
        {
            throw new NotImplementedException();
        }

        public int EscalateIncident( string incidentno )
        {
            throw new NotImplementedException();
        }

        public int SubmitRequest( IItem snrequest )
        {
            this.SoapUrl = "https://e2esm-int.intel.com/RequestFulfillment_v1.do?SOAP";
            return this.MakeRequest( snrequest );
        }

        public int QueryRequest( string requestno )
        {
            throw new NotImplementedException();
        }

        public int UpdateRequest( string requestno , string statusupdate )
        {
            throw new NotImplementedException();
        }

        public int ResolveRequest( string requestno , string resolution )
        {
            throw new NotImplementedException();
        }

        public int CancelRequest( string requestno )
        {
            throw new NotImplementedException();
        }

        public OnWebResponseReceived OnWebResponseReceived
        {
            get;
            set;
        }

        public OnWebErrorOccurred OnWebErrorOccurred
        {
            get;
            set;
        }

        public OnWebLoginRequired OnWebLoginRequired
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public int Id
        {
            get;
            private set;
        }

        #endregion

        private int MakeRequest( IItem snrequest )
        {
            try
            {
                this.Payload = snrequest;

                var webRequest = ( HttpWebRequest ) WebRequest.Create( this.SoapUrl );
                webRequest.CookieContainer = _cookieContainer;

                if( _cookieContainer.Count == 0 )
                {
                    if( string.IsNullOrEmpty( this.Username ) || string.IsNullOrEmpty( this.Password ) )
                    {
                        _cookieContainer = new CookieContainer();
                        this.OnWebLoginRequired( this );
                        return this.Id;
                    }

                    string authInfo = this.Username + ":" + this.Password;
                    authInfo = Convert.ToBase64String( Encoding.Default.GetBytes( authInfo ) );
                    webRequest.Headers[ "Authorization" ] = "Basic " + authInfo;
                }

                webRequest.PreAuthenticate = true;
                webRequest.Headers.Add( @"SOAP:Action" );
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";

                Stream stream = webRequest.GetRequestStream();
                this.Payload.XmlDoc.Save( stream );

                this.DoWebRequest( webRequest , this.ReadReponse );
            }
            catch( Exception ex )
            {
                this.Error = ex;
                this.OnWebErrorOccurred( this );
            }

            return this.Id;
        }

        private int MakeRequest()
        {
            try
            {
                var webRequest = ( HttpWebRequest ) WebRequest.Create( this.SoapUrl );
                webRequest.CookieContainer = _cookieContainer;
                
                if( _cookieContainer.Count == 0 )
                {
                    if( string.IsNullOrEmpty( this.Username ) || string.IsNullOrEmpty( this.Password ) )
                    {
                        _cookieContainer = new CookieContainer();
                        this.OnWebLoginRequired( this );
                        return this.Id;
                    }

                    string authInfo = this.Username + ":" + this.Password;
                    authInfo = Convert.ToBase64String( Encoding.Default.GetBytes( authInfo ) );
                    webRequest.Headers[ "Authorization" ] = "Basic " + authInfo;
                }

                webRequest.PreAuthenticate = true;
                webRequest.Method = "GET";
                
                this.DoWebRequest( webRequest , this.ReadReponse );
            }
            catch( Exception ex )
            {
                this.Error = ex;
                this.OnWebErrorOccurred( this );
            }

            return this.Id;
        }

        public static void Init()
        {
            _cookieContainer = ReadCookiesFromDisk( "ServiceNowCookies" );
        }

        public static void DeInit()
        {
            WriteCookiesToDisk( "ServiceNowCookies" , _cookieContainer );
        }

        private static void WriteCookiesToDisk( string file , CookieContainer cookieJar )
        {
            using( Stream stream = File.Create( file ) )
            {
                try
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize( stream , cookieJar );
                }
                catch( Exception ex )
                {
                    Factory.OnFrameworkErrorOccurred( ex.Message );
                }
            }
        }

        private static CookieContainer ReadCookiesFromDisk( string file )
        {
            try
            {
                using( Stream stream = File.Open( file , FileMode.Open ) )
                {
                    var formatter = new BinaryFormatter();
                    return ( CookieContainer ) formatter.Deserialize( stream );
                }
            }
            catch( Exception ex )
            {
                Factory.OnFrameworkErrorOccurred( ex.Message );
                return new CookieContainer();
            }
        }

        private void ReadReponse( HttpWebResponse response )
        {
            Stream respStream = response.GetResponseStream();

            if( respStream == null )
            {
                this.OnWebErrorOccurred( this );
                return;
            }

            this.Response = new StreamReader( respStream ).ReadToEnd();
            this.OnWebResponseReceived( this );
        }

        private void OnResponseReceived( IAsyncResult iar , Action < HttpWebResponse > responseReaderAction )
        {
            try
            {
                var response = ( HttpWebResponse ) ( ( HttpWebRequest ) iar.AsyncState ).EndGetResponse( iar );
                responseReaderAction( response );
            }
            catch( WebException ex )
            {
                this.Error = ex;

                if( ex.Status == WebExceptionStatus.ProtocolError )
                {
                    _cookieContainer = new CookieContainer();
                    this.OnWebLoginRequired( this );
                    return;
                }
                
                this.OnWebErrorOccurred( this );
            }
        }

        private void EndAsyncRequest( IAsyncResult iar )
        {
            var action = ( Action ) iar.AsyncState;
            action.EndInvoke( iar );
        }

        private void DoWebRequest( HttpWebRequest request , Action < HttpWebResponse > responseAction )
        {
            try
            {
                Action wrapperAction = () => request.BeginGetResponse( iar => this.OnResponseReceived( iar , responseAction ) , request );
                wrapperAction.BeginInvoke( this.EndAsyncRequest , wrapperAction );
            }
            catch( Exception ex )
            {
                this.Error = ex;
                this.OnWebErrorOccurred( this );
            }
        }
    }
}