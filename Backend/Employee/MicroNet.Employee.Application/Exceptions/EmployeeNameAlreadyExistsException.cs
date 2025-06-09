namespace MicroNet.Employee.Application.Exceptions
{
    public class EmployeeNameAlreadyExistsException : AppException
    {
        public EmployeeNameAlreadyExistsException(string code) : base($"Employee with name: `{code}` already exists.")
        {
        }
    }
}
