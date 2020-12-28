// Special packets used to Communicate with the Server Helper
// (which helps to locate server ip address when the boradcast is blocked in the LAN)
// All class names here start with "H"
using System.Collections.Generic;

/// <summary>
/// Sent by server to register ip address on the Server Helper.
/// </summary>
public class HServerReg : Pkt<HServerReg>
{
    public string ip;

    public HServerReg(string ip) : base()
    {
        this.ip = ip;
    }
}

/// <summary>
/// Sent by client to search available servers in the LAN
/// </summary>
public class HReqList : Pkt<HReqList>
{

}

/// <summary>
/// Sent by Server Helper to pass the list of available servers to the client
/// </summary>
public class HServerList : Pkt<HServerList>
{
    public List<string> sList;

    public HServerList(List<string> sList) : base()
    {
        this.sList = sList;
    }
}