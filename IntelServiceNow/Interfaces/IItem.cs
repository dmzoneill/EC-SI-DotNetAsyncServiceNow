using System.Xml;
using IntelServiceNow.Enums;

namespace IntelServiceNow.Interfaces
{
    public interface IItem
    {
        XmlDocument XmlDoc
        {
            get;
        }

        string Location
        {
            get;
            set;
        }

        IUser Assignee
        {
            get;
            set;
        }

        IUser Customer
        {
            get;
            set;
        }

        Priority Priority
        {
            get;
            set;
        }

        IUser Requester
        {
            get;
            set;
        }

        IServiceLine ServiceLine
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }

        string DetailedDescription
        {
            get;
            set;
        }

        Impact Impact
        {
            get;
            set;
        }
    }
}