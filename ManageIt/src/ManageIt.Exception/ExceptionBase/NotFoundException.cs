using System.Net;

namespace ManageIt.Exception.ExceptionBase
{
    public class NotFoundException : ManageItException
    {
        public NotFoundException(string message) : base (message)
        {
            
        }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
