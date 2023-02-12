using MessagePack;
using QFire.Abstraction.Message;
using System.Runtime.Serialization;

[MessagePackObject(keyAsPropertyName: true)]
public class TestMessage : QFireMessage
{
    public string MyMessage { get; set; }
}
