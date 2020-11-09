using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    
    private Camera camera;
    private float targetZoom;

    [SerializeField]
    private float zoomFactor = 3;

    [SerializeField]
    private float zoomLerpSpeed = 10;

    [SerializeField]
    private float minZoom = 4.5f;

    [SerializeField]
    private float maxZoom = 15f;

    private float yVelocity = 0.0f;

    [SerializeField]
    private float panSensitivity = 1;

    [SerializeField]
    private float panRange = 10f;

    [SerializeField]
    private TileBase beachTile;

    private int leftMostBeachTile = 500;
    private int rightMostBeachTile = 500;
    private int topMostBeachTile = 500;
    private int bottomMostBeachTile = 500;

    void Start()
    {
        camera = Camera.main;
        targetZoom = camera.orthographicSize;  

        CalculateCameraBounds();      
    }

    void Update()
    {
        // Zoom
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, targetZoom, ref yVelocity, Time.deltaTime * zoomLerpSpeed);

        // Pan
        if (Input.GetMouseButton(1)) {            
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            float xCamera = camera.transform.position.x;
            float yCamera = camera.transform.position.y;

            xCamera = Mathf.Clamp(xCamera, 450, 550);
            yCamera = Mathf.Clamp(yCamera, 450, 550);
        
            // Jank way of clmaping the camera panning
            if (((camera.transform.position.x - x) > leftMostBeachTile-panRange) && ((camera.transform.position.x - x) < rightMostBeachTile+panRange) && ((camera.transform.position.y - y) > bottomMostBeachTile-panRange) && ((camera.transform.position.y - y) < topMostBeachTile+panRange)) 
            {
                camera.transform.Translate(new Vector3(-x, -y, 0) * panSensitivity);
            }
            
        }
    }

    public void CalculateCameraBounds()
    {
        Tilemap tilemap = GlobalVariables.Variables.tileMaps[0];

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == beachTile) {
                    if (x < leftMostBeachTile)
                        leftMostBeachTile = x;
                    
                    if (x > rightMostBeachTile)
                        rightMostBeachTile = x;

                    if (y < bottomMostBeachTile)
                        bottomMostBeachTile = y;

                    if (y > topMostBeachTile)
                        topMostBeachTile = y;
                }
            }
        }        
    }
}
