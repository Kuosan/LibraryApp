using System;
using System.Collections.Generic;
using System.Text;

namespace kek
{
    class Team : INameAndCopy
    {
        protected string _organizationName;
        protected int _registrationNumber;
        public string OrganizationName { get { return _organizationName; } set { _organizationName = value; } }
        public int RegistrationNumber
        {
            get { return _registrationNumber; }
            set
            {
                try
                {
                    if (value <= 0)
                    {
                        throw new Exception("Value is less or equals 0");
                    }
                    else
                    {
                        _registrationNumber = value;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public string Name { get; set; }

        public Team(string organisationName, int registrationNumber)
        {
            _organizationName = organisationName;
            _registrationNumber = registrationNumber;
        }
        public Team()
        {
            _organizationName = "RuCl";
            _registrationNumber = 1;
        }
        object INameAndCopy.DeepCopy(object originalObject)
        {
            Team team = originalObject as Team;
            Team teamCopy = new Team();
            teamCopy._organizationName = team._organizationName;
            teamCopy._registrationNumber = team._registrationNumber;

            return teamCopy;
        }
        public override bool Equals(Object obj)
        {
            Team team = obj as Team;
            if (!this.GetType().Equals(team.GetType()))
            {
                return false;
            }
            else
            {
                return (team._organizationName == _organizationName) && (team._registrationNumber == _registrationNumber);
            }
        }
        public static bool operator ==(Team obj1, Team obj2)
        {
            return (obj1._organizationName == obj2._organizationName) && (obj1._registrationNumber == obj2._registrationNumber);
        }
        public static bool operator !=(Team obj1, Team obj2) => !(obj1 == obj2);

        public override int GetHashCode()
        {
            return _organizationName.GetHashCode() ^ _registrationNumber.GetHashCode();
        }
        public override string ToString()
        {
            return _organizationName.ToString() + " " + _registrationNumber.ToString();
        }
    }
}
