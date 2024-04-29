using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencePlacer : MonoBehaviour
{
    [SerializeField]
    public GameObject fencePrefab; // Префаб забора
    [SerializeField]
    public Terrain terrain; // Terrain, вокруг которого нужно разместить забор
    public float fenceHeight = 0f; // Высота забора
    void Start()
    {
        PlaceFence();
    }
    
   
    void Update()
    {
        
    }
    void PlaceFence()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        for (float i = 0; i <= terrainWidth; i += fencePrefab.transform.localScale.x)
        {
            Vector3 fencePosition = new Vector3(i, fenceHeight, 0);
            Instantiate(fencePrefab, fencePosition, Quaternion.Euler(0, 90, 0));
            fencePosition = new Vector3(i, fenceHeight, terrainLength);
            Instantiate(fencePrefab, fencePosition, Quaternion.Euler(0, 90, 0));
        }

        for (float i = 0; i <= terrainLength; i += fencePrefab.transform.localScale.z)
        {
            Vector3 fencePosition = new Vector3(0, fenceHeight, i);
            Instantiate(fencePrefab, fencePosition, Quaternion.identity);
            fencePosition = new Vector3(terrainWidth, fenceHeight, i);
            Instantiate(fencePrefab, fencePosition, Quaternion.identity);
        }
    }


}
