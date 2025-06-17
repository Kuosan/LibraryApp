using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace kek
{
    class Person : INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        public event PropertyChangedEventHandler PropertyChanged;


        public string Name { get { return _name; } set { _name = value; } }
        public string Surname { get { return _surname; } set { _surname = value; } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; } }
        public int BirthYear { set { BirthDate = new DateTime(value, BirthDate.Month, BirthDate.Day); } }

        public Person(string name, string surname, System.DateTime birthDate)
        {
            _name = name;
            _surname = surname;
            _birthDate = birthDate;
        }

        public Person()
        {
            _name = "Moka";
            _surname = "Sato";
            _birthDate = new DateTime(2000, 5, 1);
        }
        public override bool Equals(Object obj)
        {
            Person person = obj as Person;
            if (!this.GetType().Equals(person.GetType()))
            {
                return false;
            }
            else
            {
                return (person.Name == Name) && (person.Surname == Surname) && (person.BirthDate == BirthDate);
            }
        }
        public static bool operator ==(Person obj1, Person obj2)
        {
            if (obj1.Name == obj2.Name && obj1.Surname == obj2.Surname && obj1.BirthDate == obj2.BirthDate)
                return true;
            else
                return false;
        }
        public static bool operator !=(Person obj1, Person obj2) => !(obj1 == obj2);

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Surname.GetHashCode() ^ BirthDate.GetHashCode();
        }
        public object DeepCopy(object originalObject)
        {
            Person person = originalObject as Person;
            Person personCopy = new Person();
            personCopy.Name = person.Name;
            personCopy.Surname = person.Surname;
            personCopy.BirthDate = person.BirthDate;
            return personCopy;
        }
        public override string ToString()
        {
            return Name.ToString() + " " + Surname.ToString() + " " + BirthDate.Day + "." + BirthDate.Month + "." + BirthDate.Year;
        }
        public virtual string ToShortString()
        {
            return Name.ToString() + " " + Surname.ToString();
        }
        public string FirstName
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_name)));
                }
            }
        }
        public string LastName
        {
            get { return _surname; }
            set
            {
                if (value != _surname)
                {
                    _surname = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_surname)));
                }
            }
        }
        public DateTime birthDate
        {
            get { return _birthDate; }
            set
            {
                if (value != _birthDate)
                {
                    _birthDate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_birthDate)));
                }
            }
        }
    }
}
