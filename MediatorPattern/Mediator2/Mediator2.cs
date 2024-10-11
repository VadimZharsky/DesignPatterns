using Autofac;
using JetBrains.Annotations;
using MediatR;

namespace Patterns.MediatorPattern.Mediator2;

public class PingCommand : IRequest<PongResponse>
{
    
}

public class PongResponse
{
    public PongResponse(DateTime timestamp)
    {
        Timestamp = timestamp;
    }

    public DateTime Timestamp { get; }
}

[UsedImplicitly]
public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
{
    public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new PongResponse(DateTime.UtcNow)).ConfigureAwait(false);
    }
}

public static class Mediator2
{
    public static void LocalMain()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(Mediator2).Assembly).AsImplementedInterfaces();
    }
}