/// <summary>
/// Base class of packet tagged with netid, sent by server 
/// <para>
/// Prefix of subclass: Si【from Server with netId】
/// </para>
/// </summary>
public class Pktid<T> : Pktid
{
    public Pktid(string id) : base(id)
    {
        this.t = typeof(T).FullName;
    }
}
/// <summary>
/// Base of <typeparamref name="Pktid"/>&lt;T&gt;
/// </summary>
public class Pktid : PacketBase
{
    public string id;
    public Pktid(string id)
    {
        this.id = id;
    }
}