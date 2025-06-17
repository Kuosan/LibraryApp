using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace kek
{
    class Paper : IComparable, IComparer
    {
        public string Name { get; set; }
        public Person Author { get; set; }
        public DateTime Date { get; set; }
        public Paper(string name, Person author, DateTime date)
        {
            Name = name;
            Author = author;
            Date = date;
        }
        public Paper()
        {
            Name = "Crime and publishment";
            Author = new Person("Fedor", "Dostoevsky", new DateTime(1821, 10, 30));
            Date = new DateTime(1866, 1, 1);
        }
        public override bool Equals(Object obj)
        {
            Paper paper = obj as Paper;
            if (!this.GetType().Equals(paper.GetType()))
            {
                return false;
            }
            else
            {
                return (paper.Name == Name) && (paper.Author == Author) && (paper.Date == Date);
            }
        }
        public static bool operator ==(Paper obj1, Paper obj2)
        {
            if (obj1.Name == obj2.Name && obj1.Author == obj2.Author && obj1.Date == obj2.Date)
                return true;
            else
                return false;
        }
        public static bool operator !=(Paper obj1, Paper obj2) => !(obj1 == obj2);

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Author.GetHashCode() ^ Date.GetHashCode();
        }
        public virtual object DeepCopy(object originalObject)
        {
            Paper paper = originalObject as Paper;
            Paper paperCopy = new Paper();
            paperCopy.Name = paper.Name;
            paperCopy.Author = paper.Author;
            paperCopy.Date = paper.Date;
            return paperCopy;
        }
        public override string ToString()
        {
            return Name.ToString() + " " + Author.ToString() + " " + Date.Day + "." + Date.Month + "." + Date.Year;
        }

        public int CompareTo(object obj)
        {
            return Date.CompareTo(obj);
        }
        public int Compare(object x, object y)
        {
            var xPaper = x as Paper;
            var yPaper = y as Paper;
            return Compare(xPaper.Author.Name, yPaper.Author.Name);
        }
    }
}
