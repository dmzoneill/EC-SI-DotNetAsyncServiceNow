using System.Xml;
using IntelServiceNow.Enums;
using IntelServiceNow.Interfaces;

namespace IntelServiceNow.Classes
{
    internal class Incident : ItemBase
    {
        public Incident()
        {
        }

        public Incident(IUser assignee, IUser customer, IUser requester, Priority priority, Impact impact, IServiceLine serviceLine, string description, string detailedDescription)
            : base(assignee, customer, requester, priority, impact, serviceLine, description, detailedDescription)
        {
        }

        protected override XmlDocument CreateDocument()
        {
            string xml = SoapEnvelopeStart()
                + "<soap:Body><sm:CreateIncident><sm:incident><sm:Assignee>"
                + EmployeeIdForValue(this.Assignee)
                + "</sm:Assignee><sm:Customer>"
                + this.EmployeeId(this.Customer)
                + "</sm:Customer><sm:Priority>"
                + this.PriorityValue()
                + "</sm:Priority><sm:Requester>"
                + this.EmployeeId(this.Requester)
                + "</sm:Requester><sm:Service><sm:Name>"
                + this.ServiceAndComponent()
                + "</sm:Name></sm:ServiceComponent><sm:State>New</sm:State><sm:Description>"
                + this.Description
                + "</sm:Description><sm:DetailedDescription>"
                + this.DetailedDescription
                + "</sm:DetailedDescription><sm:Category>Incident</sm:Category><sm:SupportSkill><sm:Name>"
                + this.ServiceLine.SupportSkill
                + "</sm:Name></sm:SupportSkill><sm:Impact>"
                + this.ImpactValue()
                + "</sm:Impact></sm:incident></sm:CreateIncident></soap:Body></soap:Envelope>";

            return LoadEnvelope(xml);
        }
    }
}
