using System;
using System.Collections.Generic;
using System.Text;

namespace kek
{
    public class TeamsJournalEntry
    {
        public string _teams { get; }
        public Revision ChangeType { get; }
        public string PropertyName { get; }
        public int RegistrationNumber { get; }

        public TeamsJournalEntry(string _team, Revision changeType, string propertyName, int registrationNumber)
        {
            _teams = _team;
            ChangeType = changeType;
            PropertyName = propertyName;
            RegistrationNumber = registrationNumber;
        }

        public override string ToString()
        {
            return $"Collection: {_teams}, ChangeType: {ChangeType}, Property: {PropertyName}, RegistrationNumber: {RegistrationNumber}";
        }
    }
}
