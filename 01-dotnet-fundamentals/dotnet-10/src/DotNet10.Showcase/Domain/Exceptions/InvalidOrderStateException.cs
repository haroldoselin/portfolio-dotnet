using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet10.Showcase.Domain.Exceptions
{
    public sealed class InvalidOrderStateException : InvalidOperationException
    {
        public InvalidOrderStateException(
            string message)
            : base(message)
        {
        }

        public InvalidOrderStateException(
            string operation,
            string currentState)
            : base(
                $"Operation '{operation}' is not valid when order is in state '{currentState}'.")
        {
        }
    }
}
