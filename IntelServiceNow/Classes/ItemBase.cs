using System;
using System.Xml;
using IntelServiceNow.Enums;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow.Classes
{
    internal abstract class ItemBase : IItem
    {
        protected ItemBase()
        {
        }

        protected ItemBase(IUser assignee, IUser customer, IUser requester, Priority priority, Impact impact, IServiceLine serviceLine, string description, string detailedDescription)
        {
            this.Assignee = assignee;
            this.Customer = customer;
            this.Priority = priority;
            this.Requester = requester;
            this.ServiceLine = serviceLine;
            this.Description = description;
            this.DetailedDescription = detailedDescription;
            this.Impact = impact;
        }

        public string Location
        {
            get;
            set;
        }

        public IUser Assignee
        {
            get;
            set;
        }

        public IUser Customer
        {
            get;
            set;
        }

        public Priority Priority
        {
            get;
            set;
        }

        public IUser Requester
        {
            get;
            set;
        }

        public IServiceLine ServiceLine
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string DetailedDescription
        {
            get;
            set;
        }

        public Impact Impact
        {
            get;
            set;
        }

        public XmlDocument XmlDoc
        {
            get
            {
                return this.CreateDocument();
            }
        }

        protected abstract XmlDocument CreateDocument();

        protected static string SoapEnvelopeStart()
        {
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" "
                + "xmlns:sm=\"http://intel.com/IT/SM\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" soap:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" "
                + "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">";
        }

        protected static string EmployeeIdForValue(object value)
        {
            return value == null ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + value + "</sm:EmployeeId>";
        }

        protected string EmployeeId(IUser user)
        {
            return user == null ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + user.Wwid + "</sm:EmployeeId>";
        }

        protected string PriorityValue()
        {
            return Enum.GetName(typeof(Priority), this.Priority);
        }

        protected string ImpactValue()
        {
            return Enum.GetName(typeof(Impact), this.Impact);
        }

        protected string ServiceAndComponent()
        {
            return this.ServiceLine.Service
                + "</sm:Name></sm:Service><sm:ServiceComponent><sm:Name>"
                + this.ServiceLine.ServiceComponent;
        }

        protected string ServiceComponentSkillAndUrgency()
        {
            return this.ServiceAndComponent()
                + "</sm:Name></sm:ServiceComponent><sm:SupportSkill><sm:Name>"
                + this.ServiceLine.SupportSkill
                + "</sm:Name></sm:SupportSkill><sm:Urgency>"
                + this.ImpactValue();
        }

        protected static XmlDocument LoadEnvelope(string xml)
        {
            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(xml);
            return soapEnvelopeXml;
        }
    }
}
