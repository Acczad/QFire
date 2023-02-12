namespace QFire.Abstraction.Message
{

    public abstract class QFireMessage
    {
        protected QFireMessage(Priority priority = Priority.Low)
        {
            Priority= priority;
        }

        public Priority GetPriority()
        {
            return this.Priority;
        }
        public bool IsHighPriority()
        {
            return this.Priority==Priority.High;
        }
        public void SetPriority(Priority priority)
        {
            Priority=priority;
        }
        public string GetId()
        {
            return this.MessageId;
        }
        public void SetId(string messageId)
        {
            this.MessageId=messageId;
        }

        public string MessageId { get; set; }
        public Priority Priority { get; set; }
    }

}