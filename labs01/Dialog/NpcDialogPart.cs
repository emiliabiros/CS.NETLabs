namespace labs01.Dialog
{
    public class NpcDialogPart : IDialogPart
    {
        public string Content { get; }
        public List<HeroDialogPart> HeroResponses { get; }

        public NpcDialogPart(string content, List<HeroDialogPart> heroResponses)
        {
            Content = content;
            HeroResponses = heroResponses;
        }
    }
}