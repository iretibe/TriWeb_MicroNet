namespace MicroNet.Sundry.Application.Exceptions
{
    public class AccountingIdNotFoundException : AppException
    {
        public AccountingIdNotFoundException(Guid id) : base($"Sundry Accounting with Id: `{id}` was not found.")
        {
        }
    }
}
