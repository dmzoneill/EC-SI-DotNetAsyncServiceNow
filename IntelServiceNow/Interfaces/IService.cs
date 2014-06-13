using System.Collections.Generic;

namespace IntelServiceNow.Interfaces
{
    public interface IService
    {
        string ServiceName
        {
            get;
        }

        List < IServiceLine > GetServiceLines();
        List < IServiceLine > GetRequestServiceLines();
        List < IServiceLine > GetIncidentServiceLines();
    }
}