namespace Balta.Localizacao.Core.Messages
{
    public abstract class Message
    {
        public Guid AggregateId { get; private set; }
        public string MessageType { get; private set; }

        protected Message(string messageType)
        {
            AggregateId = Guid.NewGuid();
            MessageType = messageType;
        }
    }

}
