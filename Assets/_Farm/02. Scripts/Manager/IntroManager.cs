using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Farm;
using Firebase.Database;
using Google.MiniJSON;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class User
{
    public string UnitList;
    public string ID;
    public string PW;
    public int Gold;

    public User(string UnitList, string ID, string PW, int Gold)
    {
        this.UnitList = UnitList;
        this.ID = ID;
        this.PW = PW;
        this.Gold = Gold;
    }
}

public class IntroManager : MonoBehaviour
{
    private FirebaseDatabase database;
    private DatabaseReference reference;
    
    [SerializeField] private TMP_InputField idInput;
    [SerializeField] private TMP_InputField pwInput;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button createButton;
    [SerializeField] private Button loginButton;

    void Start()
    {
        database = FirebaseDatabase.GetInstance("https://hellofirebase3-7fa03-default-rtdb.firebaseio.com/");
        reference = database.RootReference;
        
        createButton.onClick.AddListener(() => CreateUserData().Forget());
        loginButton.onClick.AddListener(() => LoginUserData().Forget());
    }

    #region 계정 생성
    private async UniTaskVoid CreateUserData()
    {
        string id = idInput.text;
        string pw = pwInput.text;
        if (string.IsNullOrEmpty(id))
        {
            infoText.text = "생성할 아아디를 입력하세요.";
            await UniTask.Delay(1000);
            ClearText();
            return;
        }
        
        if (string.IsNullOrEmpty(pw))
        {
            infoText.text = "비밀번호를 입력하세요.";
            await UniTask.Delay(1000);
            ClearText();
            return;
        }

        infoText.text = "중복 확인 및 생성 중...";
        SetButtonInteractable(false);

        try
        {
            var snapshot = await reference.Child("UserInfo").OrderByChild("ID").EqualTo(id).GetValueAsync();

            if (snapshot.HasChildren) // 중복 확인
            {
                infoText.text = "중복된 아이디입니다.";
                await UniTask.Delay(1000);
                ClearText();
                SetButtonInteractable(true);
                return;
            }

            Dictionary<string, bool> unitDics = new Dictionary<string, bool>();

            for (int i = 0; i < 4; i++) // 계정 생성시 캐릭터 초기화
                unitDics.Add($"Unit{i}", false);

            string unitListJson = JsonConvert.SerializeObject(unitDics);
            int Gold = 100; // 초기 금화 세팅

            User newUser = new User(unitListJson, id, pw, Gold);
            string userData = JsonConvert.SerializeObject(newUser);

            await reference.Child("UserInfo").Push().SetRawJsonValueAsync(userData);
            Debug.Log("계정 생성 완료");
            infoText.text = "계정이 생성되었습니다.";
            
            await UniTask.Delay(1000);
            ClearText();
            SetButtonInteractable(true);
        }
        catch (Exception e) // 네트워크 통신 에러, 파이어베이스 세팅 문제, 파이어베이스 권한 문제 등
        {
            Debug.LogError(e);
            infoText.text = "오류가 발생하였습니다. 다시 시도해주세요.";
            await UniTask.Delay(1000);
            ClearText();
            SetButtonInteractable(true);
        }
    }
    #endregion

    #region 계정 로그인
    private async UniTaskVoid LoginUserData()
    {
        string id = idInput.text;
        string pw = pwInput.text;
        if (string.IsNullOrEmpty(id))
        {
            infoText.text = "아이디를 입력하세요.";
            await UniTask.Delay(1000);
            SetButtonInteractable(true);
            return;
        }
        
        if (string.IsNullOrEmpty(pw))
        {
            infoText.text = "비밀번호를 입력하세요.";
            await UniTask.Delay(1000);
            SetButtonInteractable(true);
            return;
        }

        SetButtonInteractable(false);
        infoText.text = "로그인 시도 중...";

        try
        {
            DataSnapshot snapshot = await reference.Child("UserInfo").OrderByChild("ID").EqualTo(id).GetValueAsync();

            if (snapshot.HasChildren) // 생성된 계정이 있는 경우
            {
                foreach (var child in snapshot.Children)
                {
                    string json = child.GetRawJsonValue();
                    var userData = JsonConvert.DeserializeObject<User>(json);
                    var units = JsonConvert.DeserializeObject<Dictionary<string, bool>>(userData.UnitList);

                    if (pw != userData.PW)
                    {
                        infoText.text = "비밀번호가 틀렸습니다.";
                        await UniTask.Delay(1000);
                        ClearText();
                        SetButtonInteractable(true);
                        return;
                    }
                    else
                    {
                        Debug.Log("로그인 성공");
                        infoText.text = "로그인 성공";
                        DataManager.Instance.SetUserData(userData.ID, userData.Gold, units, child.Key);
                        SceneManager.LoadScene(1);
                    }
                }
            }
        }
        catch (Exception e) // 네트워크 통신 에러, 파이어베이스 세팅 문제, 파이어베이스 권한 문제 등
        {
            Debug.LogError(e);
            infoText.text = "오류가 발생하였습니다. 다시 시도해주세요.";
            await UniTask.Delay(1000);
            ClearText();
            SetButtonInteractable(true);
        }
    }
    #endregion
    
    private void SetButtonInteractable(bool isActive)
    {
        createButton.interactable = isActive;
        loginButton.interactable = isActive;
    }

    private void ClearText()
    {
        infoText.text = string.Empty;
        idInput.text = string.Empty;
        pwInput.text = string.Empty;
    }
}