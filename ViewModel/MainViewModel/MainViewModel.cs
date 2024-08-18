using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UdpProductServer.Data.Local.Service;
using UdpProductServer.Data.Model;
using UdpProductServer.Data.Remote;

namespace UdpProductServer.ViewModel.MainViewModel;
public partial class MainViewModel(ProductService products) : ObservableObject
{
    public required Udp Udp;

    private readonly ProductService products = products;

    [ObservableProperty]
    public bool isRunning = false;
    [ObservableProperty]
    public bool isNotRunning = true;
    [ObservableProperty]
    private string ip = "127.0.0.1";
    [ObservableProperty]
    private int port = 1024;

    public ObservableCollection<Message> MessageHistory { get; set; } = [];

    [RelayCommand]
    private void Start()
    {
        this.Udp = new Udp(Ip, Port);
        this.IsRunning = true;
        this.IsNotRunning = !IsRunning;
        this.MessageHistory.Add(new Message()
        {
            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
            Content = "Server was setted up. Waiting for client...",
            Sender = "SYSTEM"
        });
        this.BeginReceive();
    }
    [RelayCommand]
    private void Stop()
    {
        this.IsRunning = false;
        this.IsNotRunning = true;
        this.Udp.Close();
        this.MessageHistory.Add(new Message()
        {
            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
            Content = "Server was shutted down!",
            Sender = "SYSTEM"
        });
    }
    public async void BeginReceive()
    {
        UdpReceiveResult result = new();
        while (true)
        {
            try
            {
                result = await this.Udp.Receive();
                string message = Encoding.Unicode.GetString(result.Buffer);
                this.MessageHistory.Add(new Message()
                {
                    Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    Sender = $"{result.RemoteEndPoint.ToString()}",
                    Content = message
                });
                Product product = this.products.Find(message);
                if (product is not null)
                    Send($"Price of {product.Name}: {product.Price.ToString()}", result.RemoteEndPoint);
                else
                    Send("No such product found in database!", result.RemoteEndPoint);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                break;
            }
        }
    }
    public void Send(string message, IPEndPoint remote)
    {
        this.Udp.Send(message, remote);
        this.MessageHistory.Add(new Message()
        {
            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
            Sender = $"me => {remote.ToString()}",
            Content = message
        });
    }
}