using System;
using System.Collections.Generic;
using System.Text;

namespace kek
{
    class TeamsJournal
    {
        private List<TeamsJournalEntry> entries = new List<TeamsJournalEntry>();

        public void OnResearchTeamsChanged(object source, ResearchTeamsChangedEventArgs<string> args)
        {
            var entry = new TeamsJournalEntry(args._teams, args.ChangeType, args.PropertyName, args.RegistrationNumber);
            entries.Add(entry);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }
}
