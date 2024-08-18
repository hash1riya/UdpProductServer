using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpProductServer.Data.Remote;
public class Udp
{
    public UdpClient socket;
    public IPEndPoint local;

    public Udp(string ip, int port)
    {
        this.local = new(IPAddress.Parse(ip), port);
        this.socket = new UdpClient(local);
    }

    public async Task<UdpReceiveResult> Receive() => await this.socket.ReceiveAsync();
    public void Send(string datagramm, IPEndPoint remote)
    {
        byte[] data = Encoding.Unicode.GetBytes(datagramm);
        this.socket.SendAsync(data, remote);
    }
    public void Close() => this.socket.Close();
}
