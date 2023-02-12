using MessagePack;
using QFire.Abstraction.Serialization;
using System;

namespace QFire.Serialization
{
    public class QFireMessagePackSerializer : IMessagePackSerializer
    {
        public byte[] Serialize(object data)
        {
            Type type = data.GetType();
            return MessagePackSerializer.Serialize(type, data);
        }
        public T Deserialize<T>(byte[] data)
        {
            return data is null ? default(T) : MessagePackSerializer.Deserialize<T>(data);
        }
    }
}
