using System;
using System.Collections.Generic;
using System.Text;

namespace kek
{
    public delegate void ResearchTeamsChangedHandler<TKey>(object source, ResearchTeamsChangedEventArgs<TKey> args);
    public class ResearchTeamsChangedEventArgs<TKey> : EventArgs
    {
        public string _teams { get; }
        public Revision ChangeType { get; }
        public string PropertyName { get; }
        public int RegistrationNumber { get; }

        public ResearchTeamsChangedEventArgs(string collectionName, Revision changeType, string propertyName, int registrationNumber)
        {
            _teams = collectionName;
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
