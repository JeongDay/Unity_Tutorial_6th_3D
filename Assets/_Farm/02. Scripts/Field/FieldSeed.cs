using UnityEngine;
using UnityEngine.EventSystems;

public class FieldSeed : IField
{
    private Camera mainCamera;
    public GameObject selectCrop;

    public FieldSeed()
    {
        mainCamera = Camera.main;
    }
    
    public void FieldAction()
    {
        if (Input.GetMouseButtonDown(0) && selectCrop)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, 1 << 15))
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                int tileX = tile.arrayPos.x;
                int tileY = tile.arrayPos.y;

                tile.CreateCrop(selectCrop);
            }
        }
    }
}