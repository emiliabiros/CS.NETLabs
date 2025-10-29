using labs01.Dialog;

namespace labs01.World
{
    public class NonPlayerCharacter
    {
        public string Name { get; }
        private readonly NpcDialogPart _startDialog;

        public NonPlayerCharacter(string name, NpcDialogPart startDialog)
        {
            Name = name;
            _startDialog = startDialog;
        }

        public NpcDialogPart StartTalking() => _startDialog;
    }
}