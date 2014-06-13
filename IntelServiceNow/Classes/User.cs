using System;
using System.DirectoryServices;
using System.Text.RegularExpressions;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow.Classes
{
    internal class User : IUser
    {
        private static DirectoryEntry _entry;
        private static DirectorySearcher _dsearch;

        private User( string wwid , string idsid , string commonname )
        {
            this.Wwid = wwid;
            this.Idsid = idsid;
            this.CommonName = commonname;
        }

        #region IUser Members

        public string Wwid
        {
            get;
            private set;
        }

        public string Idsid
        {
            get;
            private set;
        }

        public string CommonName
        {
            get;
            private set;
        }

        #endregion

        private static string GetProperty( SearchResult searchResult , string propertyName )
        {
            if( searchResult.Properties.Contains( propertyName ) )
            {
                return searchResult.Properties[ propertyName ][ 0 ].ToString();
            }
            return string.Empty;
        }

        private static User SearchByProperty( string property , string identifier )
        {
            var workersous = new string[ 4 ];

            workersous[ 0 ] = "OU=Workers,DC=ger,DC=corp,DC=intel,DC=com";
            workersous[ 1 ] = "OU=Workers,DC=amr,DC=corp,DC=intel,DC=com";
            workersous[ 2 ] = "OU=Workers,DC=ccr,DC=corp,DC=intel,DC=com";
            workersous[ 3 ] = "OU=Workers,DC=gar,DC=corp,DC=intel,DC=com";

            foreach( string workersou in workersous )
            {
                _entry = new DirectoryEntry( "GC://" + workersou );
                _dsearch = new DirectorySearcher( _entry )
                           {
                               SearchScope = SearchScope.Subtree , Filter = "(&(objectClass=user)(" + property + "=" + identifier + "))"
                           };

                SearchResult sResultSet = _dsearch.FindOne();

                if( sResultSet != null )
                {
                    return new User( GetProperty( sResultSet , "employeeID" ) , GetProperty( sResultSet , "sAMAccountName" ) , GetProperty( sResultSet , "cn" ) );
                }
            }

            return null;
        }

        public static User GetUser( string identifier )
        {
            if( identifier.Equals( "faceless" ) )
            {
                return new User( "10659388" , "facless" , "faceless" );
            }

            var wwidregex = new Regex( "[0-9]{7,9}" );
            var idsidregex = new Regex( "[a-zA-Z0-9]{8}" );
            var commonnameregex = new Regex( "[a-zA-Z0-9]+,[ a-zA-Z0-9]+" );

            User user = null;

            try
            {
                if( wwidregex.IsMatch( identifier ) )
                {
                    user = SearchByProperty( "employeeID" , identifier );
                }
                else if( idsidregex.IsMatch( identifier ) )
                {
                    user = SearchByProperty( "sAMAccountName" , identifier );
                }
                else if( commonnameregex.IsMatch( identifier ) )
                {
                    user = SearchByProperty( "cn" , identifier );
                }
            }
            catch( Exception ex )
            {
                Console.WriteLine( ex.Message );
            }

            return user;
        }
    }
}