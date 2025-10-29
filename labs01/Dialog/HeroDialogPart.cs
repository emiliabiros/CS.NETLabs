namespace labs01.Dialog
{
    public class HeroDialogPart : IDialogPart
    {
        public string Content { get; }
        public NpcDialogPart? NextNpcPart { get; }

        public HeroDialogPart(string content, NpcDialogPart? nextNpcPart = null)
        {
            Content = content;
            NextNpcPart = nextNpcPart;
        }
    }
}