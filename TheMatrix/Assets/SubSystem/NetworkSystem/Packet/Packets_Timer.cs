using GameSystem;

/// <summary>
/// Request to sync time
/// </summary>
public class RTimer : Pkt<RTimer>
{
    public float ti;

    public RTimer() : base()
    {
        this.ti = NetworkSystem.timer;
    }
}
/// <summary>
/// sync time
/// </summary>
public class STimer : Pkt<STimer>
{
    /// <summary>
    /// the time this packets is received
    /// </summary>
    public float ti;
    public float tSent;

    public STimer(float tSent) : base()
    {
        this.ti = NetworkSystem.timer;
        this.tSent = tSent;
    }
}