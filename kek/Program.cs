using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

namespace kek
{
    class Program
    {
        static void Main(string[] args)
        {
            Paper[] papers1 = { new Paper("Apocalypse", new Person("Sara", "Adams", new DateTime(1996, 5, 12)), new DateTime(2017, 12, 22)),
                                 new Paper("Eternity", new Person("Bernice", "Campbell", new DateTime(1996, 8, 23)), new DateTime(2003, 3, 11)),
                                    new Paper("Exams", new Person("Kirk", "Ramsey", new DateTime(1987, 1, 12)), new DateTime(2005, 6, 30))};
            Paper[] papers2 = { new Paper("Detective", new Person("Patricia", "Hart", new DateTime(1988, 6, 22)), new DateTime(2021, 9, 28)),
                                 new Paper("Illness", new Person("Bobby", "Summers", new DateTime(1994, 11, 30)), new DateTime(2010, 11, 17)),
                                    new Paper("Oldness", new Person("Paul", "Simmons", new DateTime(1990, 8, 1)), new DateTime(2013, 6, 6))};
            Paper[] papers3 = { new Paper("Twins", new Person("Norman", "Garcia", new DateTime(1989, 9, 15)), new DateTime(2015, 8, 25)),
                                 new Paper("Dance", new Person("Helen", "Clark", new DateTime(1995, 3, 27)), new DateTime(2009, 5, 12)),
                                    new Paper("Danger", new Person("Jean", "Daniels", new DateTime(1998, 12, 19)), new DateTime(2000, 4, 16))};

            Person[] people1 = { new Person("Jacob", "Wells", new DateTime(1990, 12, 17)),
                                 new Person("Henry", "Fowler", new DateTime(1993, 2, 28)),
                                    new Person("Jay", "Foster", new DateTime(1996, 6, 30))};
            Person[] people2 = { new Person("Aaron", "Brown", new DateTime(1995, 9, 13)),
                                 new Person("Patricia", "Perkins", new DateTime(1988, 7, 21)),
                                    new Person("Lisa", "Lewis", new DateTime(1989, 4, 1))};
            Person[] people3 = { new Person("Carmen", "Nichols", new DateTime(1986, 10, 18)),
                                 new Person("Beverly", "Jacobs", new DateTime(1987, 1, 25)),
                                    new Person("Louis", "Brown", new DateTime(1991, 7, 9))};

            ResearchTeam rt1 = new ResearchTeam("Painter", "PSociety", 2, TimeFrame.Long);
            rt1.AddPapers(papers1);
            rt1.AddMembers(people1);
            Console.WriteLine($"Research team 1: {rt1}");

            Console.WriteLine("Date Sort: ");
            foreach (Paper paper in rt1.PublicationDateSort())
            {
                Console.WriteLine($"{paper}");
            }
            Console.WriteLine();
            Console.WriteLine("Name Sort: ");
            foreach (Paper paper in rt1.PublicationNameSort())
            {
                Console.WriteLine($"{paper}");
            }
            Console.WriteLine();
            Console.WriteLine("Author Surname Sort: ");
            foreach (Paper paper in rt1.PublicationAuthorSurnameSort())
            {
                Console.WriteLine($"{paper}");
            }
            Console.WriteLine();

            ResearchTeamCollection<string> rtc = new ResearchTeamCollection<string>(ResearchTeamCollection<string>.KeyDefiner);

            ResearchTeam rt2 = new ResearchTeam("Death", "WindD", 3, TimeFrame.Year);
            rt2.AddPapers(papers2);
            rt2.AddMembers(people2);

            ResearchTeam rt3 = new ResearchTeam("Plane", "AirPlanes", 4, TimeFrame.TwoYears);
            rt3.AddPapers(papers3);
            rt3.AddMembers(people3);

            rtc.AddDefaults();
            rtc.AddResearchTeams(rt1);
            rtc.AddResearchTeams(rt2);

            Console.WriteLine(rtc);

            Console.WriteLine($"Latest publication: {rtc.LatestPublication}");

            var groupedTimeFrame = rtc.TimeFrameGroup(TimeFrame.Year);
            Console.WriteLine("Year TimeFrame: ");
            foreach (var item in groupedTimeFrame)
            {
                Console.WriteLine(item.Value);
            }

            Console.WriteLine();
            Console.WriteLine("Group by duration: ");

            foreach (var item in rtc.GroupByDuration)
            {
                Console.WriteLine();
                Console.WriteLine(item.Key);
                foreach (var name in item)
                {
                    Console.WriteLine(name);
                }
            }

            TestCollections<Team, ResearchTeam> test = new TestCollections<Team, ResearchTeam>(4, TestCollections<Team, ResearchTeam>.GenerateElement);
            test.SearchKV();
            test.SearchSV();
            test.SearchDictionaryKeysK();
            test.SearchDictionaryValueV();
            test.SearchDictionaryKeysV();

            

            var collection1 = new ResearchTeamCollection<string>("Collection 1");
            var collection2 = new ResearchTeamCollection<string>("Collection 2");

            var journal = new TeamsJournal();
            collection1.ResearchTeamsChanged += journal.OnResearchTeamsChanged;
            collection2.ResearchTeamsChanged += journal.OnResearchTeamsChanged;

            var team1 = new ResearchTeam("Topic 1", "boba",1,TimeFrame.TwoYears);
            var team2 = new ResearchTeam("Topic 2", "biba",2,TimeFrame.Year);

            collection1.Add("1", team1);
            collection1.Add("2", team2);
            collection2.Add("1", team2);

            team1._Theme = "Updated Topic 1";
            team2._Theme = "Updated Topic 2";
            team2.RegistrationNumber = 3;

            collection2.Remove("1");
            collection1.Replace("1", "3", new ResearchTeam("Topic 3","booba", 4, TimeFrame.Year));

            Console.WriteLine(journal);
        }
    }
}