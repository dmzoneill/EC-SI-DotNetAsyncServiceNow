namespace IntelServiceNow.Interfaces
{
    public interface IServiceLine
    {
        string Shortcode
        {
            get;
        }

        string Comment
        {
            get;
        }

        string Owner
        {
            get;
        }

        string Type
        {
            get;
        }

        string Service
        {
            get;
        }

        string ServiceComponent
        {
            get;
        }

        string SupportSkill
        {
            get;
        }

        string RequestTitle
        {
            get;
        }
    }
}