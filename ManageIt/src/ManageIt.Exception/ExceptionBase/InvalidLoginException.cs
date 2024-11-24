﻿using System.Net;

namespace ManageIt.Exception.ExceptionBase
{
    public class InvalidLoginException : ManageItException
    {
        public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
        {
        }
        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
