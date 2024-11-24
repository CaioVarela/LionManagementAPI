namespace ManageIt.Exception.ExceptionBase
{
    public abstract class ManageItException : SystemException
    {
        protected ManageItException(string message) : base(message)
        {
            
        }

        public abstract int StatusCode { get; }
        public abstract List<string> GetErrors();
    }
}
