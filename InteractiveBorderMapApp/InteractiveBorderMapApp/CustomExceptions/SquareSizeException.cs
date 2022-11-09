using System;

namespace InteractiveBorderMapApp.CustomExceptions
{
    public class SquareSizeException : Exception
    {
        public SquareSizeException() : base() {}

        public SquareSizeException(string message) : base(message) { }
    }
}