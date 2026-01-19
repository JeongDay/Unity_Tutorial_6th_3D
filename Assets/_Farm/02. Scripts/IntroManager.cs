using Farm;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField idInput;
    [SerializeField] private TMP_InputField pwInput;

    [SerializeField] private Button createButton;
    [SerializeField] private Button loginButton;

    void Start()
    {
        createButton.onClick.AddListener(() =>
        {
            // 회원가입 기능
        });
        
        loginButton.onClick.AddListener(() =>
        {
            // 로그인 기능
            DataManager.Instance.UserID = idInput.text;
            SceneManager.LoadScene(1);
        });
    }
}