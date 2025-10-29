namespace labs02.Models
{
    class Parliament
    {
        public event EventHandler<string> StartVote;
        public event EventHandler EndVote;

        private readonly List<Mop> _members = new();
        private int _votesYes;
        private int _votesNo;
        private string _currentTopic;

        public void AddMember(Mop member)
        {
            _members.Add(member);
            member.VoteGiven += OnMemberVoteGiven;
        }

        private void OnMemberVoteGiven(object sender, bool vote)
        {
            if (vote) _votesYes++;
            else _votesNo++;
        }

        public void StartVoting(string topic)
        {
            _currentTopic = topic;
            _votesYes = _votesNo = 0;
            Console.WriteLine($"\nVoting started on topic: {topic}\n");
            StartVote.Invoke(this, topic);
        }

        public void EndVoting()
        {
            Console.WriteLine("\nVoting ended.\n");
            EndVote.Invoke(this, EventArgs.Empty);
            ShowResult();
        }

        private void ShowResult()
        {
            Console.WriteLine($"\nVoting results for '{_currentTopic}':");
            Console.WriteLine($"YES: {_votesYes}");
            Console.WriteLine($"NO: {_votesNo}");
        }

        public IReadOnlyList<Mop> Members => _members.AsReadOnly();
    }
}