namespace _Game.Scripts.CommandsSystem.Failure
{
    public enum FailureReason
    {
        None,
        Cooldown,
        CantUseOnAllies,
        CantReachTarget,
        CantReachLocation,
        NotEnoughResources,
        NotReady,
        TargetNotFound
    }
}