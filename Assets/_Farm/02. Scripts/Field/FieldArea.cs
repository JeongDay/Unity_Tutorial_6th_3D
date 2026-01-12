using UnityEngine;

public class FieldArea : MonoBehaviour, ITriggerEvent
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2Int fieldSize = new Vector2Int(10, 10);
    private float tileSize = 2f;

    private Camera mainCamera;
    private GameObject[,] tileArray;
    private GameObject currCrop;

    void Start()
    {
        mainCamera = Camera.main;
        tileArray = new GameObject[fieldSize.x, fieldSize.y];
        
        CreateField();
    }
    
    public void InteractionEnter()
    {
        CameraManager.OnChangedCamera("Player", "Field");
    }

    public void InteractionExit()
    {
        CameraManager.OnChangedCamera("Field", "Player");
    }

    private void CreateField()
    {
        float offsetX = (fieldSize.x - 1) * tileSize / 2f;
        float offsetY = (fieldSize.y - 1) * tileSize / 2f;

        for (int i = 0; i < fieldSize.x; i++)
        {
            for (int j = 0; j < fieldSize.y; j++)
            {
                float posX = transform.position.x + i * tileSize - offsetX;
                float posZ = transform.position.z + j * tileSize - offsetY;

                GameObject tileObj = Instantiate(tilePrefab, transform); // transform 스케일 1

                tileObj.layer = 15; // Field Layer를 15로 설정

                tileObj.name = $"Tile_{i}_{j}";
                tileObj.transform.position = new Vector3(posX, 0, posZ);
                tileArray[i, j] = tileObj;

                tileObj.GetComponent<Tile>().arrayPos = new Vector2Int(i, j);
            }
        }
    }
}