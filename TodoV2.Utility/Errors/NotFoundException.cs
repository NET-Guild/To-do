using System;

namespace TodoV2.Utility.Errors
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}