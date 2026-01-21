using System.Collections.Generic;
using Firebase.Database;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    #region User 클래스
    public class User
    {
        public string NickName;
        public string UnitList;
        public int Level;
        public int Damage;
        public int Gold;

        public User(string NickName, string UnitList, int Level, int Damage, int Gold)
        {
            this.NickName = NickName;
            this.UnitList = UnitList;
            this.Level = Level;
            this.Damage = Damage;
            this.Gold = Gold;
        }
    }

    #endregion

    #region 필드값

    private FirebaseDatabase database;
    private DatabaseReference reference;
    private UnityMainThreadDispatcher dispatcher;

    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button loginButton;

    // 캐릭터 초기 세팅값
    private string NickName;
    private int Level = 1;
    private int Damage = 10;
    private int Gold = 100;

    private Dictionary<string, bool> unitDics = new Dictionary<string, bool>();

    #endregion

    void Start()
    {
        database = FirebaseDatabase.GetInstance("https://hellofirebase3-7fa03-default-rtdb.firebaseio.com/");
        reference = database.RootReference;

        dispatcher = UnityMainThreadDispatcher.Instance();

        unitDics.Add("Unit0", false);

        loginButton.onClick.AddListener(CreateData);
    }

    private void CreateData()
    {
        NickName = nickNameInput.text;

        reference.Child("UserInfo").OrderByChild("NickName").EqualTo(NickName).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
                return;

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.HasChildren) // 이미 닉네임(키)이 있을 때
                {
                    dispatcher.Enqueue(() => { infoText.text = "중복된 닉네임입니다."; });
                }
                else // 닉네임이 없을 때
                {
                    if (NickName.Length < 1) // 닉네임 입력을 안했을 때
                    {
                        dispatcher.Enqueue(() => { infoText.text = "닉네임을 입력하세요."; });
                    }
                    else // 닉네임을 정상적으로 입력 했을 때
                    {
                        string unitList = JsonConvert.SerializeObject(unitDics);

                        User user = new User(NickName, unitList, Level, Damage, Gold);
                        string jsonData = JsonConvert.SerializeObject(user);
                        
                        // 데이터 업로드
                        reference.Child("UserInfo").Push().SetRawJsonValueAsync(jsonData).ContinueWith(task1 =>
                        {
                            if (task1.IsFaulted)
                            {
                                dispatcher.Enqueue(() =>
                                {
                                    Debug.Log("업로드 실패");
                                });
                            }
                            else if (task1.IsCompleted)
                            {
                                dispatcher.Enqueue(() =>
                                {
                                    Debug.Log("업로드 성공");
                                    SceneManager.LoadScene(1);
                                });
                            }
                        });
                    }
                }
            }
        });
    }
}