using System;
using System.Xml;
using IntelServiceNow.Enums;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow.Classes
{
    internal class Request : IItem
    {
        public Request()
        {
        }

        public Request( IUser assignee , IUser customer , IUser requester , Priority priority , IServiceLine serviceLine , string description , string detaileddescription , Impact impact )
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
            xml += "<soap:Envelope ";
            xml += "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ";
            xml += "xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" ";
            xml += "xmlns:sm=\"http://intel.com/IT/SM\" ";
            xml += "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" ";
            xml += "soap:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" ";
            xml += "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">";
            xml += "<soap:Body>";
            xml += "<sm:CreateRequest>";
            xml += "<sm:request>";
            xml += "<sm:Assignee>";

            xml += ( this.Assignee == null ) ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + this.Assignee.Wwid + "</sm:EmployeeId>";

            xml += "</sm:Assignee>";

            xml += "<sm:Customer>";

            xml += ( this.Customer == null ) ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + this.Customer.Wwid + "</sm:EmployeeId>";

            xml += "</sm:Customer><sm:Priority>";

            xml += Enum.GetName( typeof( Priority ) , this.Priority );

            xml += "</sm:Priority><sm:Description>";

            xml += this.Description;

            xml += "</sm:Description><sm:DetailedDescription>";

            xml += this.DetailedDescription;

            xml += "</sm:DetailedDescription><sm:Requester>";

            xml += ( this.Requester.Wwid == null ) ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + this.Requester.Wwid + "</sm:EmployeeId>";

            xml += "</sm:Requester>";

            xml += "<sm:RequestItemList>";
            xml += "<sm:RequestItem>";
            xml += "<sm:Assignee>";

            xml += ( this.Assignee == null ) ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + this.Assignee.Wwid + "</sm:EmployeeId>";

            xml += "</sm:Assignee>";

            xml += "<sm:CatalogItem>";
            xml += "<sm:Name>Request - Lab Operating System</sm:Name>";

            xml += "<sm:Service><sm:Name>";

            xml += this.ServiceLine.Service;

            xml += "</sm:Name></sm:Service><sm:ServiceComponent><sm:Name>";

            xml += this.ServiceLine.ServiceComponent;

            xml += "</sm:Name></sm:ServiceComponent>";
            xml += "</sm:CatalogItem>";

            xml += "<sm:Customer>";

            xml += ( this.Customer == null ) ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + this.Customer.Wwid + "</sm:EmployeeId>";

            xml += "</sm:Customer><sm:Description>";

            xml += this.Description;

            xml += "</sm:Description><sm:DetailedDescription>";

            xml += this.DetailedDescription;

            xml += "</sm:DetailedDescription><sm:Location><sm:Name>";

            xml += this.Location;

            xml += "</sm:Name></sm:Location><sm:Requester>";

            xml += ( this.Requester == null ) ? "<sm:EmployeeId />" : "<sm:EmployeeId>" + this.Requester.Wwid + "</sm:EmployeeId>";

            xml += "</sm:Requester><sm:Service><sm:Name>";

            xml += this.ServiceLine.Service;

            xml += "</sm:Name></sm:Service><sm:ServiceComponent><sm:Name>";

            xml += this.ServiceLine.ServiceComponent;

            xml += "</sm:Name></sm:ServiceComponent><sm:SupportSkill><sm:Name>";

            xml += this.ServiceLine.SupportSkill;

            xml += "</sm:Name></sm:SupportSkill><sm:Urgency>";

            xml += Enum.GetName( typeof( Impact ) , this.Impact );

            xml += "</sm:Urgency>";
            xml += "</sm:RequestItem></sm:RequestItemList><sm:Service><sm:Name>";

            xml += this.ServiceLine.Service;

            xml += "</sm:Name></sm:Service><sm:ServiceComponent><sm:Name>";

            xml += this.ServiceLine.ServiceComponent;

            xml += "</sm:Name></sm:ServiceComponent><sm:SupportSkill><sm:Name>";

            xml += this.ServiceLine.SupportSkill;

            xml += "</sm:Name></sm:SupportSkill><sm:Urgency>";

            xml += Enum.GetName( typeof( Impact ) , this.Impact );

            xml += "</sm:Urgency></sm:request></sm:CreateRequest></soap:Body></soap:Envelope>";

            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml( xml );

            return soapEnvelopeXml;
        }
    }
}