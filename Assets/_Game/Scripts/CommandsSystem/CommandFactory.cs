using _Game.Scripts.CommandsSystem.Model;

namespace _Game.Scripts.CommandsSystem
{
    public class CommandFactory
    {
        private readonly CommandConfig _config;
        protected Command Command;

        public CommandFactory(CommandConfig config)
        {
            _config = config;
        }

        public virtual void CreateCommand()
        {
            if (Command == null) return;
            Command.SetDescription(_config.Title, _config.Description, _config.DisplayImage);
            Command.InitCommandTimer();
            Command.SetCooldown(_config.Cooldown);
            Command.SetCommandResourceCost(_config.CommandResourceCost);
            Command.SetTargetType(_config.TargetType);
            Command.ChangeCooldownTimer(_config.Cooldown);
            Command.ChangeStatus(CommandStatus.Ready);
        }

        public virtual Command GetCommand() => Command;
    }
}