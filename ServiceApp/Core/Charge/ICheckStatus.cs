namespace ServiceApp.Core.Charge
{
    public interface ICheckStatus
    {
        bool IsSuccessful(string status);
    }
}