using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{

    public GameObject enemy;
    public GameObject fakeEnemy;
    public GameObject player;
    public GameObject bodyPartEffect;
    public GameObject bloodEffect;
    bool isTriggered=false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")&&!isTriggered)
        {
            isTriggered = true;
            player.SetActive(false);
            fakeEnemy.SetActive(true);
            StartCoroutine(SpawnEnemy());
            bodyPartEffect.SetActive(true);
            
        }
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(5f);
        player.SetActive(true);
        Debug.Log("CalledPlayer");
        yield return new WaitForSeconds(0.1f);
        fakeEnemy.SetActive(false);
        enemy.SetActive(true);
        Destroy(bodyPartEffect);
        Destroy(fakeEnemy);
        bloodEffect.SetActive(true);
    }
}
