namespace QFire.Abstraction.Core
{
    public  interface IMessageKeyGenerator
    {
        string GenerateKey();
        string GetQfireCacheAbbrivation();
    }
}
