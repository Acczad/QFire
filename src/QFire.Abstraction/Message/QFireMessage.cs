using System;

namespace QFire.Abstraction.Message
{
    public abstract class QFireMessage
    {
        protected QFireMessage( Priority priority)
        {
            MessageId = Guid.NewGuid();
            Priority = priority;
        }
        public Guid MessageId { get; }
        public Priority Priority { get; }
    }
    
}