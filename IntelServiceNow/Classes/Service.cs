using System.Collections.Generic;
using System.Linq;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow.Classes
{
    internal class Service : IService
    {
        private static readonly List < IService > Services;

        static Service()
        {
            Services = new List < IService >();
        }

        public Service( string name )
        {
            this.ServiceName = name;

            if( Services.Any( service => service.ServiceName.Equals( name ) ) )
            {
                return;
            }

            Services.Add( this );
        }

        #region IService Members

        public string ServiceName
        {
            get;
            private set;
        }

        public List < IServiceLine > GetServiceLines()
        {
            return Factory.GetServiceLines().Where( serviceline => serviceline.Service.Equals( this.ServiceName ) ).ToList();
        }

        public List < IServiceLine > GetRequestServiceLines()
        {
            return Factory.GetServiceLines().Where( service => service.Service.Equals( this.ServiceName ) && service.Type.Contains( "Request" ) ).ToList();
        }

        public List < IServiceLine > GetIncidentServiceLines()
        {
            return Factory.GetServiceLines().Where( service => service.Service.Equals( this.ServiceName ) && service.Type.Contains( "Incident" ) ).ToList();
        }

        #endregion

        public static List < IService > GetServices()
        {
            return Services;
        }
    }
}