using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine;

namespace Farm
{
    public class DataManager : SingletonCore<DataManager>
    {
        private FirebaseDatabase database;
        private DatabaseReference reference;
        
        public int SelectCharacterIndex { get; set; }
        public GameObject Player { get; set; }

        public string UserID { get; private set; }
        public int UserGold { get; private set; }
        private Dictionary<string, bool> MyUnits { get; set; } = new Dictionary<string, bool>();
        
        public string UserKey { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            database = FirebaseDatabase.GetInstance("https://hellofirebase3-7fa03-default-rtdb.firebaseio.com/");
            reference = database.RootReference;
        }
        
        public void SetUserData(string id, int gold, Dictionary<string, bool> units, string key)
        {
            UserID = id;
            UserGold = gold;
            MyUnits = units;
            UserKey = key;
        }

        public async UniTaskVoid SetUnitData(string unitName)
        {
            MyUnits[unitName] = true; // 데이터 수정

            if (!string.IsNullOrEmpty(UserKey))
            {
                string data = JsonConvert.SerializeObject(MyUnits);
                await reference.Child("UserInfo").Child(UserKey).Child("UnitList").SetValueAsync(data);
            }
        }

        public async UniTaskVoid SetGold(int addValue)
        {
            UserGold += addValue;
            Debug.Log($"{addValue} 획득! / 현재 골드 : {UserGold}");
            
            if (!string.IsNullOrEmpty(UserKey))
            {
                await reference.Child("UserInfo").Child(UserKey).Child("Gold").SetValueAsync(UserGold);
                Debug.Log($"{addValue} 획득! / 현재 골드 : {UserGold}");
            }
        }
        
        // public void SetGold(int addValue)
        // {
        //     UserGold += addValue;
        //     Debug.Log($"{addValue} 획득! / 현재 골드 : {UserGold}");
        // }
        //
        // public async UniTaskVoid SaveData()
        // {
        //     if (!string.IsNullOrEmpty(UserKey))
        //     {
        //         await reference.Child("UserInfo").Child(UserKey).Child("Gold").SetValueAsync(UserGold);
        //     }
        // }
    }
}