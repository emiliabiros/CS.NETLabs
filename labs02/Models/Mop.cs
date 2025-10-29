namespace labs02.Models
{
    class Mop
    {
        private string Name { get; }
        private bool _canVote;
        private static readonly Random Rand = new Random();

        public event EventHandler<bool> VoteGiven;

        public Mop(string name)
        {
            Name = name;
        }

        public void Subscribe(Parliament parliament)
        {
            parliament.StartVote += async (s, topic) => await OnStartVoteAsync(topic);
            parliament.EndVote += OnEndVote;
        }

        private async Task OnStartVoteAsync(string topic)
        {
            _canVote = true;
            Console.WriteLine($"{Name} got notified: Voting started on '{topic}'.");
            int delayMs = Rand.Next(100, 1000);
            await Task.Delay(delayMs);
            Vote();
        }

        private void OnEndVote(object sender, EventArgs e)
        {
            _canVote = false;
            Console.WriteLine($"{Name} got notified: Voting ended.");
        }

        private void Vote()
        {
            if (!_canVote)
            {
                Console.WriteLine($"{Name} cannot vote right now!");
                return;
            }

            bool vote = Rand.Next(2) == 0;
            Console.WriteLine($"{Name} votes {(vote ? "YES" : "NO")}");
            VoteGiven.Invoke(this, vote);
            _canVote = false;
        }
    }
}