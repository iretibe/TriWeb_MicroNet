namespace MicroNet.Employee.Application.Exceptions
{
    public class EmployeeIdNotFoundException : AppException
    {
        public EmployeeIdNotFoundException(Guid code) : base($"Employee with Id: `{code}` is not found.")
        {
        }
    }
}
