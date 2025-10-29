using labs01.Dialog;
using labs01.Models;

namespace labs01.Core
{
    public class DialogParser
    {
        private readonly Hero _hero;

        public DialogParser(Hero hero)
        {
            _hero = hero;
        }

        public string ParseDialog(IDialogPart dialogPart)
        {
            string text = dialogPart.Content;
            return text.Replace("#HERONAME#", _hero.Name);
        }
    }
}