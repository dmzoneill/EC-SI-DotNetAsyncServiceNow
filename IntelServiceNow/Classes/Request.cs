using System.Xml;
using IntelServiceNow.Enums;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow.Classes
{
    internal class Request : ItemBase
    {
        public Request()
        {
        }

        public Request(IUser assignee, IUser customer, IUser requester, Priority priority, IServiceLine serviceLine, string description, string detailedDescription, Impact impact)
            : base(assignee, customer, requester, priority, impact, serviceLine, description, detailedDescription)
        {
        }

        protected override XmlDocument CreateDocument()
        {
            string requesterByWwid = this.Requester.Wwid == null
                ? "<sm:EmployeeId />"
                : "<sm:EmployeeId>" + this.Requester.Wwid + "</sm:EmployeeId>";
            string skillBlock = this.ServiceComponentSkillAndUrgency();
            string xml = SoapEnvelopeStart()
                + "<soap:Body><sm:CreateRequest><sm:request><sm:Assignee>"
                + this.EmployeeId(this.Assignee)
                + "</sm:Assignee><sm:Customer>"
                + this.EmployeeId(this.Customer)
                + "</sm:Customer><sm:Priority>"
                + this.PriorityValue()
                + "</sm:Priority><sm:Description>"
                + this.Description
                + "</sm:Description><sm:DetailedDescription>"
                + this.DetailedDescription
                + "</sm:DetailedDescription><sm:Requester>"
                + requesterByWwid
                + "</sm:Requester><sm:RequestItemList><sm:RequestItem><sm:Assignee>"
                + this.EmployeeId(this.Assignee)
                + "</sm:Assignee><sm:CatalogItem><sm:Name>Request - Lab Operating System</sm:Name><sm:Service><sm:Name>"
                + this.ServiceAndComponent()
                + "</sm:Name></sm:ServiceComponent></sm:CatalogItem><sm:Customer>"
                + this.EmployeeId(this.Customer)
                + "</sm:Customer><sm:Description>"
                + this.Description
                + "</sm:Description><sm:DetailedDescription>"
                + this.DetailedDescription
                + "</sm:DetailedDescription><sm:Location><sm:Name>"
                + this.Location
                + "</sm:Name></sm:Location><sm:Requester>"
                + this.EmployeeId(this.Requester)
                + "</sm:Requester><sm:Service><sm:Name>"
                + skillBlock
                + "</sm:Urgency></sm:RequestItem></sm:RequestItemList><sm:Service><sm:Name>"
                + skillBlock
                + "</sm:Urgency></sm:request></sm:CreateRequest></soap:Body></soap:Envelope>";

            return LoadEnvelope(xml);
        }
    }
}
