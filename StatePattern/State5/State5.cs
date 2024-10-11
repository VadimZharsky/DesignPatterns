using Stateless;
using Stateless.Graph;
using State = Patterns.StatePattern.State2.State;

namespace Patterns.StatePattern.State5;

public enum Health
{
    NonReproductive,
    Pregnant,
    Reproductive
}

public enum Activity
{
    GiveBirth,
    ReachPuberty,
    HaveAbortion,
    HaveUnprotectedSex,
    Historectomy
}

public class State5
{
    public static void LocalMain()
    {
        var machine = new StateMachine<Health, Activity>(Health.NonReproductive);
        
        machine.Configure(Health.NonReproductive)
            .Permit(Activity.ReachPuberty, Health.Reproductive);
        
        machine.Configure(Health.Reproductive)
            .Permit(Activity.Historectomy, Health.NonReproductive)
            .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant,
                () => ParentsNotWatching);
        
        machine.Configure(Health.Pregnant)
            .Permit(Activity.GiveBirth, Health.Reproductive)
            .Permit(Activity.HaveAbortion, Health.Reproductive);

        machine.Fire(Activity.ReachPuberty);

        Console.WriteLine(machine.State);
        
        if (machine.CanFire(Activity.HaveUnprotectedSex))
           machine.Fire(Activity.HaveUnprotectedSex); 

        Console.WriteLine(machine.State);

    }

    public static bool ParentsNotWatching { get; set; }
}