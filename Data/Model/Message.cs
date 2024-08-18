﻿namespace UdpProductServer.Data.Model;
public class Message
{
    public string Time { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
}
