using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase.Database;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UniTaskManager : MonoBehaviour
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

    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button loginButton;

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

        // 캐릭터 4개 생성
        for (int i = 0; i < 4; i++)
            unitDics.Add("Unit" + i, false);

        loginButton.onClick.AddListener(() => CreateData().Forget());
    }

    private async UniTaskVoid CreateData()
    {
        NickName = nickNameInput.text;

        if (string.IsNullOrEmpty(NickName))
        {
            infoText.text = "닉네임을 입력하세요.";
            return;
        }

        infoText.text = "데이터 확인 중...";
        loginButton.interactable = false; // 중복 클릭 방지

        try
        {
            DataSnapshot snapshot = await reference.Child("UserInfo").OrderByChild("NickName").EqualTo(NickName).GetValueAsync();

            if (snapshot.HasChildren)
            {
                infoText.text = "이미 존재하는 닉네임입니다.";
                loginButton.interactable = true;
                return;
            }
            else
            {
                string unitList = JsonConvert.SerializeObject(unitDics);

                User user = new User(NickName, unitList, Level, Damage, Gold);
                string jsonData = JsonConvert.SerializeObject(user);

                await reference.Child("UserInfo").Push().SetRawJsonValueAsync(jsonData);
                Debug.Log("계정 생성됨");

                SceneManager.LoadScene(1);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"에러 발생 : {e}");
            infoText.text = "오류가 발생하였습니다.";
            loginButton.interactable = true;
        }
    }
}