using System;

namespace Lab04
{
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException(string message) : base(message)
        {
        }
    }
}
