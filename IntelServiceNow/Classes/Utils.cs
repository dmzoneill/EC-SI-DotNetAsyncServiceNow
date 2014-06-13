using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IntelServiceNow.Classes
{
    internal class Utils
    {
        private static Assembly _assembly;
        private static Stream _textStream;
        private static StreamReader _reader;
        private static string _serviceLinesIni;

        public static void Init()
        {
            _assembly = Assembly.GetExecutingAssembly();
            ReadServiceLines();
        }

        public static void DeInit()
        {
        }

        private static void ReadServiceLines()
        {
            try
            {
                _textStream = _assembly.GetManifestResourceStream( "IntelServiceNow.servicelines.txt" );

                if( _textStream == null )
                {
                    return;
                }

                _reader = new StreamReader( _textStream );
                _serviceLinesIni = _reader.ReadToEnd();

                string[] lines = _serviceLinesIni.Split( Environment.NewLine.ToCharArray() );

                var categoryRegex = new Regex( @"^\[([a-zA-Z0-9\-]+)\]$" );
                var categoryValueRegex = new Regex( @"^\s*([a-zA-Z]+)\s*=\s*(.*?)$" );

                string shortcode = "";
                string comment = "";
                string owner = "";
                string type = "";
                string service = "";
                string servicecomponent = "";
                string supportskill = "";
                string requesttitle = "";

                bool first = true;

                foreach( string line in lines )
                {
                    Match cmatch = categoryRegex.Match( line );
                    Match vmatch = categoryValueRegex.Match( line );

                    if( cmatch.Success )
                    {
                        if( !first )
                        {
                            new ServiceLine( requesttitle , supportskill , servicecomponent , service , type , owner , comment , shortcode );

                            comment = null;
                            owner = null;
                            type = null;
                            service = null;
                            servicecomponent = null;
                            supportskill = null;
                            requesttitle = null;
                        }

                        shortcode = cmatch.Groups[ 1 ].ToString();
                        first = false;
                    }
                    else if( vmatch.Success )
                    {
                        string key = vmatch.Groups[ 1 ].ToString();

                        switch( key )
                        {
                            case "Comment":
                                comment = vmatch.Groups[ 2 ].ToString().Trim();
                                break;

                            case "Owner":
                                owner = vmatch.Groups[ 2 ].ToString().Trim();
                                break;

                            case "Type":
                                type = vmatch.Groups[ 2 ].ToString().Trim();
                                break;

                            case "Service":
                                service = vmatch.Groups[ 2 ].ToString().Trim();
                                new Service( service );
                                break;

                            case "ServiceComponent":
                                servicecomponent = vmatch.Groups[ 2 ].ToString().Trim();
                                break;

                            case "SupportSkill":
                                supportskill = vmatch.Groups[ 2 ].ToString().Trim();
                                break;

                            case "RequestTitle":
                                requesttitle = vmatch.Groups[ 2 ].ToString().Trim();
                                break;
                        }
                    }
                }

                new ServiceLine( requesttitle , supportskill , servicecomponent , service , type , owner , comment , shortcode );
            }
            catch( Exception e )
            {
                Factory.OnFrameworkErrorOccurred( e.Message );
            }
        }
    }
}