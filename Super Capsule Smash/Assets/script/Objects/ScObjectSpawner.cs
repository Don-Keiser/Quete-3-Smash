using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScObjectSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> objectToSpawn = new List<GameObject>();
    [SerializeField] Transform leftBound;
    [SerializeField] Transform rightBound;
    [SerializeField] private float minDelay;

    private bool gameOn;
    private Vector3 spawnPoint;
    private float delay;

    private void Start()
    {
        gameOn = true;
        delay = minDelay + Random.Range(2, 8);
        UIManager.Instance.newRound.AddListener(RoundStarted);
        UIManager.Instance.roundOver.AddListener(RoundOver);
    }

    void Update()
    {
       if (gameOn)
       {
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                delay = minDelay + Random.Range(2, 8);
                SpawnObject();
            }
        }
       
    }

    private void SpawnObject()
    {
        spawnPoint.Set(Random.Range(leftBound.position.x, rightBound.position.x), leftBound.position.y, 0);
        var tempo = Instantiate(objectToSpawn[Random.Range(0,objectToSpawn.Count)], spawnPoint, Quaternion.identity);
        tempo.GetComponent<Rigidbody2D>().AddTorque(Random.Range(100,200));
    }

    private void RoundStarted(int lol)
    {
        delay = minDelay + Random.Range(2, 8);
        gameOn = true;
    }
    private void RoundOver()
    {
        gameOn = false;
    }
}
