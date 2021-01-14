using System;

/// <summary>
/// Base class of Pkt & Pktid
/// </summary>
public class PacketBase
{
    /// <summary>
    /// Name of packet type.
    /// Simplified name to reduce packet size.
    /// </summary>
    public string t;

    // interface
    public Type PktType => Type.GetType(t);
    public bool MatchType(Type type) => type.FullName == t;
    public bool MatchType<T>() => typeof(T).FullName == t;
    public bool IsSubclassOf(Type type) => PktType.IsSubclassOf(type);
}