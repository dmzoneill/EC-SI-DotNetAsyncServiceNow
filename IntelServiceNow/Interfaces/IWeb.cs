using System;
using IntelServiceNow.Delegates;

namespace IntelServiceNow.Interfaces
{
    public interface IWeb
    {
        OnWebResponseReceived OnWebResponseReceived
        {
            get;
            set;
        }

        OnWebErrorOccurred OnWebErrorOccurred
        {
            get;
            set;
        }

        OnWebLoginRequired OnWebLoginRequired
        {
            get;
            set;
        }

        string Password
        {
            get;
            set;
        }

        string Username
        {
            get;
            set;
        }

        int Id
        {
            get;
        }

        string Response
        {
            get;
        }

        IItem Payload
        {
            get;
        }

        Exception Error
        {
            get;
        }

        int SubmitIncident( IItem snincident );
        int QueryIncident(string incidentno);
        int QueryAssignedIncidents(IUser assignee);
        int UpdateIncident( string incidentno , string statusupdate );
        int AssignIncident( string incidentno , IUser user );
        int ResolveIncident( string incidentno , string resolution );
        int EscalateIncident( string incidentno );

        int SubmitRequest( IItem snrequest );
        int QueryRequest( string requestno );
        int UpdateRequest( string requestno , string statusupdate );
        int ResolveRequest( string requestno , string resolution );
        int CancelRequest( string requestno );
    }
}