using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSpawner : MonoBehaviour
{
    [SerializeField] InsectMover[] insects;

    [SerializeField] Transform[] lanes;
    [SerializeField] float beginSpawnCooldown = 2f;
    float spawnCooldown;
    [SerializeField] float beginSpeed = 1f;
    float speed;
    bool spawning = false;

    public void ResetSpawner()
    {
        speed = beginSpeed;
        spawnCooldown = beginSpawnCooldown;
        spawning = true;
        StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());

    }

    IEnumerator SpawnCoroutine()
    {
        while(spawning)
        {
            SpawnInsects();
            yield return new WaitForSeconds(spawnCooldown);
            speed += 0.2f;
            spawnCooldown -= 0.05f;
            if(spawnCooldown < 1f)
            {
                spawnCooldown = 1f;
            }
        }
    }

    void SpawnInsects()
    {
        if(!spawning || FindObjectOfType<Clock>().timeRemaining < 2f)
        {
            return;
        }
        var amountToSpawn = Random.Range(1, 5);
        Debug.Log(amountToSpawn);
        var lane = lanes[Random.Range(0, lanes.Length)];
        List<int> destinationsUsed = new List<int>();

        for(int i = 0; i < amountToSpawn; i++)
        {
            int randomDestinationInLane = Random.Range(0, lane.childCount);
            do
            {
                randomDestinationInLane = Random.Range(0, lane.childCount);
            }
            while (destinationsUsed.Contains(randomDestinationInLane));

            destinationsUsed.Add(randomDestinationInLane);

            
          InsectMover newInsect = Instantiate(insects[Random.Range(0, insects.Length)], transform.position, Quaternion.identity);
            newInsect.transform.position = new Vector2(
                lane.GetChild(randomDestinationInLane).transform.position.x + RandomPositiveOrNegative() * 10f,
                lane.GetChild(randomDestinationInLane).transform.position.y);

            newInsect.destination = lane.GetChild(randomDestinationInLane).position;
            newInsect.speed = speed;
            Destroy(newInsect, 10f);
        }
    }

    int RandomPositiveOrNegative()
    {
        int randomChance = Random.Range(0, 100);
        if(randomChance > 50)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
