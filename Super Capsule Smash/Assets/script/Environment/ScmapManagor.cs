using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScmapManagor : MonoBehaviour
{
    [SerializeField] List<GameObject> mapPrefabs = new List<GameObject>();
    [SerializeField] GameObject previousMap;
    [SerializeField] Vector3 mapSpawnPos;
    private GameObject current;
    private List<Vector3> mapRespawnPoint = new List<Vector3>();
    private bool OnTheMove;

    private void Start()
    {
        UIManager.Instance.newRound.AddListener(LoadNextMap);
    }

    private void LoadNextMap(int timeToSlide)
    {
        current = mapPrefabs[mapPrefabs.Count - 1];
        Instantiate(current, mapSpawnPos, Quaternion.identity);
        OnTheMove = true;
        Invoke("StopMovingMap", timeToSlide);
    }

    private void Update()
    {
        if (OnTheMove)
        {
            current.transform.position = Vector3.Lerp(current.transform.position, Vector3.zero,0.1f);
            previousMap.transform.position = Vector3.Lerp(current.transform.position, -mapSpawnPos, 0.1f);
        }
    }

    private void StopMovingMap()
    {
        Debug.Log("map Stop Move ");
        OnTheMove = false;
        current.transform.position = Vector3.zero;
        Destroy(previousMap );
        previousMap = current;
    }
}
