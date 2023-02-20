using MessagePack;
using QFire.Abstraction.Message;
using System;
using System.Runtime.Serialization;

[MessagePackObject(keyAsPropertyName: true)]
public class TestMessage : QFireMessage
{
    public TestMessage()
    {
        CreateDate= DateTime.Now;
    }
    public string MyMessage { get; set; }
    public DateTime CreateDate { get; set; }
}
