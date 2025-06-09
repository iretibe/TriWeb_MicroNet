namespace MicroNet.Employee.Application.Exceptions
{
    public class EmployeeIdAlreadyExistsException : AppException
    {
        public EmployeeIdAlreadyExistsException(Guid code) : base($"Employee with Id: `{code}` already exists.")
        {
        }
    }
}
