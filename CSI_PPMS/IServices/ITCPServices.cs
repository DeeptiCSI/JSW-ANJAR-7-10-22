using CSI_PPMS.Models;
using System.Collections.Generic;
using System.Net.Sockets;

namespace CSI_PPMS.IServices
{
    public interface ITCPServices
    {
        TcpClient ConnectTechofor(string IPAddress, int Portno);

        List<byte[]> ConvertHexaFL1(FL1PunchingModel model);

        TcpClient ConnectPLC(string IPAddress, int Portno);
    }
}
