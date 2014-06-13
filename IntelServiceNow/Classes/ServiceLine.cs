using System.Collections.Generic;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow.Classes
{
    internal class ServiceLine : IServiceLine
    {
        private static readonly List < IServiceLine > ServiceLines;

        static ServiceLine()
        {
            ServiceLines = new List < IServiceLine >();
        }

        public ServiceLine( string requestTitle , string supportSkill , string serviceComponent , string service , string type , string owner , string comment , string shortcode )
        {
            this.Shortcode = shortcode;
            this.Comment = comment;
            this.Owner = owner;
            this.Type = type;
            this.Service = service;
            this.ServiceComponent = serviceComponent;
            this.SupportSkill = supportSkill;
            this.RequestTitle = requestTitle;

            ServiceLines.Add( this );
        }

        #region IServiceLine Members

        public string Shortcode
        {
            get;
            private set;
        }

        public string Comment
        {
            get;
            private set;
        }

        public string Owner
        {
            get;
            private set;
        }

        public string Type
        {
            get;
            private set;
        }

        public string Service
        {
            get;
            private set;
        }

        public string ServiceComponent
        {
            get;
            private set;
        }

        public string SupportSkill
        {
            get;
            private set;
        }

        public string RequestTitle
        {
            get;
            private set;
        }

        #endregion

        public static List < IServiceLine > GetServiceLines()
        {
            return ServiceLines;
        }
    }
}