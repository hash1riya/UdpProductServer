namespace UdpProductServer.Data.Model;
public abstract class Entity
{
    public uint Id { get; set; }
    public bool IsDeleted { get; set; }
}
