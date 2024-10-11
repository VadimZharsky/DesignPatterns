namespace Patterns.DoesntMatter;

public interface IDevice<out T>
{
    IDevice<T> Clone();
    
    public int IdDevice { get; }
}

public interface ISlaveDevice<out T> : IDevice<T>
{
    public int IdOwner { get; }
}

public abstract class BaseDevice<T> : IDevice<T>
{
    protected BaseDevice(int idDevice)
    {
        IdDevice = idDevice;
    }
    
    public int IdDevice { get; }

    public abstract IDevice<T> Clone();

    public override string ToString() => $"{nameof(IdDevice)}: {IdDevice}";
}

public class CameraDto : BaseDevice<CameraDto>
{
    public CameraDto(int idDevice, int idOwner) : base(idDevice)
    {
        IdOwner = idOwner;
    }

    public int IdOwner { get; }

    public override CameraDto Clone() => new (idDevice: IdDevice, idOwner: IdOwner);

    public override string ToString() => $"{nameof(IdDevice)}: {IdDevice}, {nameof(IdOwner)}: {IdOwner}";
}

public class ServerDto : BaseDevice<ServerDto>
{
    public ServerDto(int idDevice) : base(idDevice) { }

    public override ServerDto Clone() => new (idDevice: IdDevice);
}


public interface IDbContext<T>
    where T : class
{
    List<T> GetAll();
    T? GetById(int id);
    bool Write(T obj);
}

public interface ICameraDbContext : IDbContext<ISlaveDevice<CameraDto>> 
{
    List<CameraDto> GetByOwnerId(int ownerId);
}

public class GenericContext<T> : IDbContext<IDevice<T>>
{
    protected readonly Dictionary<int, IDevice<T>> Context = [];

    public virtual List<IDevice<T>> GetAll()
        => Context.Values.Select(x => x.Clone()).ToList();

    public virtual IDevice<T>? GetById(int id) => Context.GetValueOrDefault(id);

    public virtual bool Write(IDevice<T> obj)
    {
        if (Context.ContainsKey(obj.IdDevice))
            return false;
        Context[obj.IdDevice] = obj.Clone();
        return true;
    }
}

public class SlaveGenericContext<T> : GenericContext<ISlaveDevice<T>>
{
    
}



public class ServerDbContext : GenericContext<ServerDto>;


public static class Exercise
{
    public static void LocalMain()
    {
        var serverContext = new ServerDbContext();
        var serv1 = new ServerDto(idDevice: 121);
        var serv2 = new ServerDto(idDevice: 134);

        var cam1 = new CameraDto(idDevice: 139, idOwner: 121);
        var cam2 = new CameraDto(idDevice: 96, idOwner: 121);
        var cam3 = new CameraDto(idDevice: 87, idOwner: 134);

        serverContext.Write(serv1);
        serverContext.Write(serv2);
        var res = serverContext.GetAll();
        res.ForEach(Console.WriteLine);
    }
}