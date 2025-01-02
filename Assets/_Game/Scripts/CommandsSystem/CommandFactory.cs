namespace _Game.Scripts.CommandsSystem
{
    public class CommandFactory
    {
        private CommandConfig _config;
        protected Command Command;

        public CommandFactory(CommandConfig config)
        {
            _config = config;
        }

        public virtual void CreateCommand()
        {
            if (Command != null)
            {
                Command.SetDescription(_config.Title, _config.Description, _config.DisplayImage);
                Command.SetCooldown(_config.Cooldown);
                Command.SetCommandResourceCost(_config.CommandResourceCost);
                Command.ChangeCooldownTimer(_config.Cooldown);
                Command.ChangeStatus(CommandStatus.Ready);
            }
        }

        public virtual Command GetCommand() => Command;
    }
}