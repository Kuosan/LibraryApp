using kek;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace kek
{
    delegate TKey KeySelector<TKey>(ResearchTeam rt);
    class ResearchTeamCollection<TKey>
    {
        private Dictionary<TKey, ResearchTeam> _teams = new Dictionary<TKey, ResearchTeam>();
        public event PropertyChangedEventHandler PropertyChanged;
        public event ResearchTeamsChangedHandler<TKey> ResearchTeamsChanged;
        private KeySelector<TKey> _keySelector;

        public string CollectionName { get; }

        public ResearchTeamCollection(string collectionName)
        {
            CollectionName = collectionName;
        }

        // Добавление элемента в коллекцию
        public void Add(TKey key, ResearchTeam researchTeam)
        {
            _teams.Add(key, researchTeam);
            researchTeam.PropertyChanged += HandlePropertyChange;
            OnCollectionChanged(Revision.Property, key);
        }

        // Удаление элемента из коллекции
        public bool Remove(TKey key)
        {
            if (_teams.Remove(key))
            {
                OnCollectionChanged(Revision.Remove, key);
                return true;
            }
            return false;
        }

        // Замена элемента в коллекции
        public bool Replace(TKey oldKey, TKey newKey, ResearchTeam researchTeam)
        {
            if (_teams.ContainsKey(oldKey))
            {
                _teams.Remove(oldKey);
                Add(newKey, researchTeam);
                OnCollectionChanged(Revision.Replace, newKey);
                return true;
            }
            return false;
        }

        // Обработчик изменения свойств элемента
        private void HandlePropertyChange(object sender, PropertyChangedEventArgs e)
        {
            var team = (ResearchTeam)sender;
            var key =_teams.FirstOrDefault(x => x.Value == team).Key;
            OnCollectionChanged(Revision.Property, key, e.PropertyName);
        }

        // Вспомогательный метод для вызова события изменения коллекции
        private void OnCollectionChanged(Revision changeType, TKey key, string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_teams)));
            ResearchTeamsChanged?.Invoke(this, new ResearchTeamsChangedEventArgs<TKey>(CollectionName, changeType, propertyName, key.GetHashCode()));
        }
   

    public DateTime LatestPublication 
        { 
            get 
            {
                if(_teams.Count > 0) return _teams.Values.Max(m => m.Latest.Date);
                return new DateTime();
            } 
        }
        public IEnumerable<IGrouping<TimeFrame,KeyValuePair<TKey,ResearchTeam>>> GroupByDuration
        { 
            get 
            {
                return _teams.GroupBy(item => item.Value.ResearchDuration);
            } 
        }
        public IEnumerable<KeyValuePair<TKey, ResearchTeam>>TimeFrameGroup(TimeFrame value)
        {
            return _teams.Where(item => item.Value.ResearchDuration== value);
        }
        public ResearchTeamCollection(KeySelector<TKey> keySelector)
        {
            _keySelector = keySelector;
            _teams = new Dictionary<TKey, ResearchTeam>();
        }
        public static string KeyDefiner(ResearchTeam value)
        {
            return value.RegistrationNumber.ToString();
        }
        public void AddDefaults()
        {
            ResearchTeam rt = new ResearchTeam();
            _teams.Add(_keySelector(rt), rt);
        }
        public void AddResearchTeams(params ResearchTeam[] rts)
        {
            foreach(var rt in rts)
            {
                _teams.Add(_keySelector(rt), rt);
            }
        }
        public override string ToString()
        {
            foreach (KeyValuePair<TKey, ResearchTeam> rtPair in _teams)
            {
                Console.WriteLine($"TKey = {rtPair.Key} || Value:");
                Console.WriteLine(rtPair.Value.ToString());
            }
            return "\n";

        }
        public string ToShortString() 
        {
            foreach (KeyValuePair<TKey, ResearchTeam> rtPair in _teams)
            {
                Console.WriteLine($"TKey = {rtPair.Key} || Value:");
                Console.WriteLine(rtPair.Value.ToShortString());
            }
            return "\n";
        }
    }
}