namespace _Game.Scripts.CommandsSystem.Commands.MoveCommand
{
    public class MoveFactory : CommandFactory
    {
        public MoveFactory(MoveConfig config) : base(config)
        {
        }

        public override void CreateCommand()
        {
            Command = new MoveCommand();
            base.CreateCommand();
        }
    }
}