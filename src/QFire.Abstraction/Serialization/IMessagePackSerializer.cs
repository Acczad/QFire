namespace QFire.Abstraction.Serialization
{
    public interface IMessagePackSerializer
    {
        byte[] Serialize(object data);
        T Deserialize<T>(byte[] bytes);
    }
}
