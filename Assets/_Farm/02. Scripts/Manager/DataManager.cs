using UnityEngine;

namespace Farm
{
    public class DataManager : SingletonCore<DataManager>
    {
        private int selectCharacterIndex;
        public int SelectCharacterIndex
        {
            get
            {
                return selectCharacterIndex;
            }
            set
            {
                Debug.Log($"선택한 캐릭터는 {value}번째 입니다.");
                selectCharacterIndex = value;
            }
        }
    }
}