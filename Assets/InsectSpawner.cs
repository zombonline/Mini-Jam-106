using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSpawner : MonoBehaviour
{
    [SerializeField] InsectMover[] insects;

    [SerializeField] Transform[] lanes;
    [SerializeField] float speed = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnInsects), 1f, 1.5f);
    }
    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            SpawnInsects();
        }
    }

    void SpawnInsects()
    {
        var amountToSpawn = Random.Range(0, 4);
        var lane = lanes[Random.Range(0, lanes.Length)];
        List<int> destinationsUsed = new List<int>();

        //set insect y pos to desination y pos
        //set insect x pos as one of two random sides of the screen
        for(int i = 0; i < amountToSpawn; i++)
        {
            int randomDestinationInLane = Random.Range(0, lane.childCount);
            do
            {
                randomDestinationInLane = Random.Range(0, lane.childCount);
            }
            while (destinationsUsed.Contains(randomDestinationInLane));

            destinationsUsed.Add(randomDestinationInLane);

            
            Debug.Log(insects[Random.RandomRange(0, insects.Length)].name + " + " + "Destination: " + lane.name + lane.GetChild(randomDestinationInLane).name);

          InsectMover newInsect = Instantiate(insects[Random.Range(0, insects.Length)], transform.position, Quaternion.identity);
            newInsect.transform.position = new Vector2(
                lane.GetChild(randomDestinationInLane).transform.position.x + RandomPositiveOrNegative() * 10f,
                lane.GetChild(randomDestinationInLane).transform.position.y);

            newInsect.destination = lane.GetChild(randomDestinationInLane).position;
            newInsect.speed = speed;
          
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
