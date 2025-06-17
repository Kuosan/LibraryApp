using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace kek
{
    class ResearchTeam : Team, INotifyPropertyChanged
    {
        private string _theme;
        private TimeFrame _researchDuration;
        private List<Person> _projectMembers = new List<Person>();
        private List<Paper> _publicationList = new List<Paper>();
        public event PropertyChangedEventHandler PropertyChanged;

        public string Theme { get { return _theme; } set { _theme = value; NotifyPropertyChanged(); } }
        public TimeFrame ResearchDuration { get { return _researchDuration; } set { _researchDuration = value; NotifyPropertyChanged(); } }
        public List<Paper> PublicationList { get { return _publicationList; } set { _publicationList = value; } }
        public List<Person> ProjectMembers { get { return _projectMembers; } set { _projectMembers = value; } }
        public Paper Latest
        {
            get
            {
                if (_publicationList.Count() == 0)
                {
                    return new Paper();
                }
                else
                {
                    Paper minPaperDate = new Paper(null, null, DateTime.MinValue);
                    foreach (Paper paper in _publicationList)
                    {
                        if (paper.Date > minPaperDate.Date) minPaperDate = paper;
                    }
                    return minPaperDate;
                }
            }
        }
        public Team Team
        {
            get
            {
                return new Team(OrganizationName, RegistrationNumber);
            }
            set
            {
                value.OrganizationName = OrganizationName;
                value.RegistrationNumber = RegistrationNumber;
            }
        }
        public ResearchTeam(string theme, string organizationName, int registrationNumber, TimeFrame researchDuration)
        {
            _theme = theme;
            _organizationName = organizationName;
            _registrationNumber = registrationNumber;
            _researchDuration = researchDuration;
        }
        public ResearchTeam()
        {
            _theme = "Russian classic";
            _organizationName = "RuCl";
            _registrationNumber = 1;
            _researchDuration = TimeFrame.Year;
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public IEnumerator<Person> GetEnumerator()
        {
            if (_publicationList.Count() == 0)
            {
                foreach (Person person in _projectMembers)
                {
                    yield return person;
                }
            }
        }
        public IEnumerable<Paper> LastNYearsPublications(int n)
        {
            DateTime compareDateTime = new DateTime(DateTime.Now.Year - n, DateTime.Now.Month, DateTime.Now.Day);
            foreach (Paper paper in _publicationList)
            {
                if (paper.Date >= compareDateTime) yield return paper;
            }
        }
        public override bool Equals(Object obj)
        {
            ResearchTeam rt = obj as ResearchTeam;
            if (!this.GetType().Equals(rt.GetType()))
            {
                return false;
            }
            else
            {
                return (rt.Theme == Theme) && (rt.OrganizationName == OrganizationName) && (rt.RegistrationNumber == RegistrationNumber) && (rt.ResearchDuration == ResearchDuration)
                    && (rt._publicationList == _publicationList) && (rt._projectMembers == _projectMembers);
            }
        }
        public static bool operator ==(ResearchTeam obj1, ResearchTeam obj2)
        {
            if (obj1.Theme == obj2.Theme && obj1.OrganizationName == obj2.OrganizationName && obj1.RegistrationNumber == obj2.RegistrationNumber && obj1.ResearchDuration == obj2.ResearchDuration
                && obj1._publicationList == obj2._publicationList && obj1._projectMembers == obj2._projectMembers)
                return true;
            else
                return false;
        }
        public static bool operator !=(ResearchTeam obj1, ResearchTeam obj2) => !(obj1 == obj2);
        public override int GetHashCode()
        {
            return Theme.GetHashCode() ^ OrganizationName.GetHashCode() ^ RegistrationNumber.GetHashCode() ^
                ResearchDuration.GetHashCode() ^ _publicationList.GetHashCode() ^ _projectMembers.GetHashCode();
        }
        public bool this[TimeFrame index]
        {
            get
            {
                if (ResearchDuration == index) return true;
                else return false;
            }
        }
        public void AddPapers(params Paper[] papers)
        {
            if (papers != null)
            {
                for (int i = 0; i < papers.Count(); i++)
                {
                    _publicationList.Add(papers[i]);
                }
            }
        }
        public void AddMembers(params Person[] members)
        {
            for (int i = 0; i < members.Count(); i++)
            {
                _projectMembers.Add(members[i]);
            }
        }
        public object DeepCopy(object originalObject)
        {
            ResearchTeam rt = originalObject as ResearchTeam;
            ResearchTeam rtCopy = new ResearchTeam();
            rtCopy.Theme = rt.Theme;
            rtCopy.OrganizationName = rt.OrganizationName;
            rtCopy.RegistrationNumber = rt.RegistrationNumber;
            rtCopy.ResearchDuration = rt.ResearchDuration;
            rtCopy._publicationList = new List<Paper>();
            for (int i = 0; i < rt._publicationList.Count(); i++)
            {
                rtCopy._publicationList.Add(rt._publicationList[i]);
            }
            return rtCopy;
        }
        public override string ToString()
        {
            string s1 = "";
            string s2 = "";
            foreach (Paper paper in _publicationList)
            {
                s1 += paper.ToString() + "\n";
            }
            foreach (Person person in _projectMembers)
            {
                s2 += person.ToString() + "\n";
            }
            return _theme.ToString() + " " + _organizationName.ToString() + " " + _registrationNumber.ToString() + " " +
                _researchDuration.ToString() + "\n" + s1.ToString() + "\n" + s2.ToString();
        }
        public virtual string ToShortString()
        {
            return _theme.ToString() + " " + _organizationName.ToString() + " " + _registrationNumber.ToString() + " " + _researchDuration.ToString();
        }
        public List<Paper> PublicationDateSort()
        {
            var sortingPublications = from p in PublicationList orderby p.Date select p;
            return sortingPublications.ToList();
        }
        public List<Paper> PublicationNameSort()
        {
            var sortingPublications = from p in PublicationList orderby p.Name select p;
            return sortingPublications.ToList();
        }
        public List<Paper> PublicationAuthorSurnameSort()
        {
            var sortingPublications = from p in PublicationList orderby p.Author.Surname select p;
            return sortingPublications.ToList();
        }
        public string _Theme
        {
            get { return _theme; }
            set
            {
                if (value != _theme)
                {
                    _theme = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_theme)));
                }
            }
        }
        public int regnum
        {
            get { return _registrationNumber; }
            set
            {
                if (value != _registrationNumber)
                {
                    _registrationNumber = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_registrationNumber)));
                }
            }
        }
    }
}
