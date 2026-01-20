using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss
{
    public string bossName;
    public int hp;
    public float dmg;

    public Boss(string bossName, int hp, float dmg)
    {
        this.bossName = bossName;
        this.hp = hp;
        this.dmg = dmg;
    }
}

public class StudyUniTask : MonoBehaviour
{
    private Boss boss;
    
    void Start()
    {
        boss = new Boss("보스 몬스터", 100, 10);
        
        BackgroundJob().Forget(); // 알아서 동작하는 기능
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boss.hp -= 5;
            Debug.Log("보스에게 5 데미지 적용");
        }
    }
    
    private async UniTaskVoid BackgroundJob()
    {
        await UniTask.WaitUntil(() => boss.hp < 75);
        Debug.Log($"{boss.bossName}의 체력이 75%이하로 되면서 페이즈1으로 돌입하였습니다.");
        
        await UniTask.WaitUntil(() => boss.hp < 50);
        Debug.Log($"{boss.bossName}의 체력이 50%이하로 되면서 페이즈2으로 돌입하였습니다.");
        
        await UniTask.WaitUntil(() => boss.hp < 25);
        boss.dmg *= 2f;
        Debug.Log($"{boss.bossName}의 체력이 25%이하로 되면서 페이즈3 상태로 광폭화가 진행되었습니다. -> 보스 데미지 : {boss.dmg}");
        
        await UniTask.WaitUntil(() => boss.hp <= 0);
        Debug.Log($"{boss.bossName}을 처치하였습니다.");
        // 옵저버 패턴으로 보스 몬스터 처치 이벤트 전달
    }
}

// public class StudySpawn : MonoBehaviour
// {
//     [SerializeField] private GameObject monsterPrefab;
//     private List<GameObject> monsters = new List<GameObject>();
//
//     private int stageIndex = 0;
//     private int monsterAmount = 100;
//     
//     void Start()
//     {
//         for (int i = 0; i < monsterAmount; i++)
//         {
//             var monsterObj = Instantiate(monsterPrefab);
//             monsters.Add(monsterObj);
//         }
//         
//         StageClear().Forget(); // 알아서 동작하는 기능
//     }
//
//     private async UniTaskVoid StageClear()
//     {
//         // 몬스터 처치시 Destory 실행
//         await UniTask.WaitUntil(() => monsters.Count <= 0);
//
//         Debug.Log("모든 몬스터를 처치하였습니다.");
//         stageIndex++;
//         SceneManager.LoadScene(stageIndex); // 스테이지 클리어시 , 다음 스테이지 진행
//     }
// }