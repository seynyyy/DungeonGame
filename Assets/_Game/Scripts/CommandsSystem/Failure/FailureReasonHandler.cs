using System.Collections.Generic;

namespace _Game.Scripts.CommandsSystem.Failure
{
    public static class FailureReasonHandler
    {
        private const string DefaultFailureReason = "Command failed";
        private const string CantReachTargetFailureReason = "Can't reach target";
        private const string CantUseOnAlliesFailureReason = "Can't use on allies";
        private const string CantReachLocationFailureReason = "Can't reach location";
        private const string NotEnoughResourcesFailureReason = "Not enough resources";
        private const string NotReadyFailureReason = "Command is not ready";
        private const string TargetNotFoundFailureReason = "Target not found";

        private static readonly Dictionary<FailureReason, string> FailureReasons = new()
        {
            { FailureReason.CantReachTarget, CantReachTargetFailureReason },
            { FailureReason.CantUseOnAllies, CantUseOnAlliesFailureReason },
            { FailureReason.CantReachLocation, CantReachLocationFailureReason },
            { FailureReason.NotEnoughResources, NotEnoughResourcesFailureReason },
            { FailureReason.NotReady, NotReadyFailureReason },
            { FailureReason.TargetNotFound, TargetNotFoundFailureReason }
        };

        public static string GetFailureReasonString(FailureReason failureReason) =>
            FailureReasons.GetValueOrDefault(failureReason, DefaultFailureReason);
    }
}