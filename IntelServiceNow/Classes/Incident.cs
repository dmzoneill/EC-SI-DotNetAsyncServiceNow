using System;
using System.Xml;
using IntelServiceNow.Enums;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow.Classes
{
    internal class Incident : IItem
    {
        public Incident()
        {
        }

        public Incident( IUser assignee , IUser customer , IUser requester , Priority priority , Impact impact , IServiceLine serviceLine , string description , string detaileddescription )
        {
            this.Assignee = assignee;
            this.Customer = customer;
            this.Priority = priority;
            this.Requester = requester;
            this.ServiceLine = serviceLine;
            this.Description = description;
            this.DetailedDescription = detaileddescription;
            this.Impact = impact;
        }

        #region IItem Members

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

        #endregion

        private XmlDocument CreateDocument()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

            xml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" ";
            xml += "xmlns:sm=\"http://intel.com/IT/SM\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" soap:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" ";
            xml += "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">";
            xml += "<soap:Body><sm:CreateIncident><sm:incident><sm:Assignee>";

            xml += ( this.Assignee == null ) ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + this.Assignee + "</sm:EmployeeId>";

            xml += "</sm:Assignee><sm:Customer>";

            xml += ( this.Customer == null ) ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + this.Customer.Wwid + "</sm:EmployeeId>";

            xml += "</sm:Customer><sm:Priority>";

            xml += Enum.GetName( typeof( Priority ) , this.Priority );

            xml += "</sm:Priority><sm:Requester>";

            xml += ( this.Requester == null ) ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + this.Requester.Wwid + "</sm:EmployeeId>";

            xml += "</sm:Requester><sm:Service><sm:Name>";

            xml += this.ServiceLine.Service;

            xml += "</sm:Name></sm:Service><sm:ServiceComponent><sm:Name>";

            xml += this.ServiceLine.ServiceComponent;

            xml += "</sm:Name></sm:ServiceComponent><sm:State>";

            xml += "New";

            xml += "</sm:State><sm:Description>";

            xml += this.Description;

            xml += "</sm:Description><sm:DetailedDescription>";

            xml += this.DetailedDescription;

            xml += "</sm:DetailedDescription><sm:Category>Incident</sm:Category><sm:SupportSkill><sm:Name>";

            xml += this.ServiceLine.SupportSkill;

            xml += "</sm:Name></sm:SupportSkill><sm:Impact>";

            xml += Enum.GetName( typeof( Impact ) , this.Impact );

            xml += "</sm:Impact></sm:incident></sm:CreateIncident></soap:Body></soap:Envelope>";

            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml( xml );

            return soapEnvelopeXml;
        }
    }
}