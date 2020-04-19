using Core.Domain.Events;
using System;

namespace Core.Domain.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
