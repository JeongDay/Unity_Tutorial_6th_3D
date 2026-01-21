using Cysharp.Threading.Tasks;
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
        createButton.onClick.AddListener(() => CreateUserData().Forget());
        loginButton.onClick.AddListener(() => LoginUserData().Forget());
    }

    private async UniTaskVoid CreateUserData()
    {
        
    }
    
    private async UniTaskVoid LoginUserData()
    {
        
    }
}