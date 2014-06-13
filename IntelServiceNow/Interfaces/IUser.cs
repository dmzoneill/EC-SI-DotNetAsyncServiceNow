namespace IntelServiceNow.Interfaces
{
    public interface IUser
    {
        string Wwid
        {
            get;
        }

        string Idsid
        {
            get;
        }

        string CommonName
        {
            get;
        }
    }
}