using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMap : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoint = new List<Transform>();
    Vector3 posToReturn;


    public Vector3 GetSpawnPoint()
    {
        posToReturn = spawnPoint[0].position;
        spawnPoint.Remove(spawnPoint[0]);
        return posToReturn;
    }
}
