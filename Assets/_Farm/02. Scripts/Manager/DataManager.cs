using System.Collections.Generic;
using UnityEngine;

namespace Farm
{
    public class DataManager : SingletonCore<DataManager>
    {
        public int SelectCharacterIndex { get; set; }
        public GameObject Player { get; set; }

        public string UserID { get; private set; }
        public int UserGold { get; private set; }

        private Dictionary<string, bool> MyUnits { get; set; } = new Dictionary<string, bool>();

        public void SetUserData(string id, int gold, Dictionary<string, bool> units)
        {
            UserID = id;
            UserGold = gold;
            MyUnits = units;
        }
    }
}