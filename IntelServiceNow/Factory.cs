using System.Collections.Generic;
using IntelServiceNow.Classes;
using IntelServiceNow.Delegates;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow
{
    public static class Factory
    {
        public static OnFrameworkErrorOccurred OnFrameworkErrorOccurred
        {
            get;
            set;
        }

        public static IItem CreateIncident()
        {
            return new Incident();
        }

        public static IItem QueryIncident( string incidentno )
        {
            return new Incident();
        }

        public static IItem CreateRequest()
        {
            return new Request();
        }

        public static IWeb GetWebHandler()
        {
            return new WebHandler();
        }

        public static IUser GetUser( string identifier )
        {
            return User.GetUser( identifier );
        }

        public static List < IService > GetServices()
        {
            return Service.GetServices();
        }

        public static List < IServiceLine > GetServiceLines()
        {
            return ServiceLine.GetServiceLines();
        }

        public static void Init()
        {
            Utils.Init();
            WebHandler.Init();
        }

        public static void DeInit()
        {
            Utils.DeInit();
            WebHandler.DeInit();
        }
    }
}