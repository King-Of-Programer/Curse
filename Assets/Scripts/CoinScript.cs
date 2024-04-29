using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Animator animator;
    const float minSpawnOffset = 50f;
    float minSpawnDistance = 20f;
    float maxSpawnDistance = 20f;
    float maxHeightFactor = 1.5f;
    float minHeightFactor = 0.7f;
    float initialHeight;
    void Start()
    {
        //animator = GetComponent<Animator>();
        //initialHeight = this.transform.position.y -
        //    Terrain.activeTerrain.SampleHeight(this.transform.position);
        GameState.Subscribe(OnGameStateChange);
    }


    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        
        if (other.gameObject.CompareTag("Player"))
        {
            GameState.Score += GameState.CoinCost;
            
            Destroy(gameObject);

           
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //   
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        animator.SetInteger("State", 1);
    //        GameState.Score += GameState.CoinCost;

    //        if (other.gameObject.CompareTag("Player"))
    //        {
    //            // ”ничтожаем монету
    //            Destroy(gameObject);
    //        }
    //    }

    //}
    public void OnDisappearFinish()
    {
        GameState.Score += GameState.CoinCost;
        Respawn();
        //GameState.AddGameMessage(new() { Text = $"Score {GameState.CoinCost:F1}points" });
        animator.SetInteger("State", 0);
    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChange);
    }
    private void Respawn()
    {
        Vector3 newPosition;
        float distance;
        int lim = 100;
        do
        {
            newPosition = transform.position + Vector3.forward * Random.Range(-minSpawnDistance, maxSpawnDistance) +
            Vector3.left * Random.Range(-minSpawnDistance, maxSpawnDistance);

            newPosition.y = initialHeight + Terrain.activeTerrain.SampleHeight(newPosition);
            distance = Vector3.Distance(newPosition, transform.position);
            lim--;

        } while (lim > 0 && (distance < minSpawnDistance
             || distance > maxSpawnDistance
             || newPosition.x < minSpawnOffset
             || newPosition.x > 1000 - minSpawnOffset
             || newPosition.z < minSpawnOffset
             || newPosition.z > 1000 - minSpawnOffset));


        transform.position = newPosition;
    }
    private void OnGameStateChange(string propertyName)
    {
        if (propertyName == nameof(GameState.CoinCost))
        {
           // GameState.AddGameMessage(new() { Text = $"Coin on new location, new cost {GameState.CoinCost}points" });
           // Respawn();
        }
    }

}
