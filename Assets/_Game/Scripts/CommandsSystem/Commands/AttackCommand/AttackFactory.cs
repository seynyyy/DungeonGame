namespace _Game.Scripts.CommandsSystem.Commands.AttackCommand
{
    public class AttackFactory : CommandFactory
    {
        public AttackFactory(AttackConfig config) : base(config)
        {
        }

        public override void CreateCommand()
        {
            Command = new AttackCommand();
            base.CreateCommand();
        }
    }
}