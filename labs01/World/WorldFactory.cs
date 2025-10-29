using labs01.Dialog;

namespace labs01.World
{
    public static class WorldFactory
    {
        public static Location CreateStartingLocation()
        {
            
            var akaraEndNo = new HeroDialogPart("Nie, nie pomogę, żegnaj.");
            var akaraEndRadszSam = new HeroDialogPart("W takim razie radź sobie sam.");
            var akaraResponseThanks = new NpcDialogPart("Dziękuję.", new List<HeroDialogPart>());
            var heroResponseOk100 = new HeroDialogPart("OK, może być 100 sztuk złota.", akaraResponseThanks);
            var akaraResponseNoMoreGold = new NpcDialogPart(
                "Niestety nie mam więcej. Jestem bardzo biedny.",
                new List<HeroDialogPart> { heroResponseOk100, akaraEndRadszSam }
            );

            var akaraResponseReward = new NpcDialogPart(
                "Dziękuję! W nagrodę otrzymasz ode mnie 100 sztuk złota.",
                new List<HeroDialogPart>
                {
                    new HeroDialogPart("Dam znać jak będę gotowy"),
                    new HeroDialogPart("100 sztuk złota to za mało!", akaraResponseNoMoreGold)
                }
            );

            var heroResponseYes = new HeroDialogPart("Tak, chętnie pomogę", akaraResponseReward);
            var akaraStart = new NpcDialogPart(
                "Witaj, czy możesz mi pomóc dostać się do innego miasta?",
                new List<HeroDialogPart> { heroResponseYes, akaraEndNo }
            );

            var akara = new NonPlayerCharacter("Akara", akaraStart);
            var charsi = new NonPlayerCharacter("Charsi", akaraStart);
            
            var deckardResponse = new NpcDialogPart(
                "Hej, czy to Ty jesteś tym słynnym #HERONAME# – pogromcą smoków?",
                new List<HeroDialogPart>
                {
                    new HeroDialogPart("Tak, jestem #HERONAME#.", 
                        new NpcDialogPart("WOW! Miło poznać!", new List<HeroDialogPart>())),
                    new HeroDialogPart("Nie.")
                }
            );
            var deckard = new NonPlayerCharacter("Deckard", deckardResponse);

            return new Location("Village", new List<NonPlayerCharacter> { akara, charsi, deckard });
        }
    }
}