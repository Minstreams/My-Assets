/// <summary>
/// Base class of general packet.
/// To minimize the size, name of packet class should be as short as possible.
/// 
/// <para>
/// Prefix of subclass:
/// </para>
/// 
/// UDP:
/// <list type="bullet">
///     <item>- U【UDP】</item>
/// </list>
/// 
/// Upload (from client to server):
/// <list type="bullet">
///     <item>I【Input】</item>
///     <item>R【Request】</item>
///     <item>D【Data】</item>
/// </list>
/// 
/// Download (from server to client):
/// <list type="bullet">
///     <item>S【from Server】</item>
/// </list>
/// </summary>
public class Pkt<T> : PacketBase
{
    public Pkt()
    {
        t = typeof(T).FullName;
    }
}