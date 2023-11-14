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
        current = Instantiate(mapPrefabs[Random.Range(1, mapPrefabs.Count)], mapSpawnPos, Quaternion.identity);
        OnTheMove = true;
        Invoke("StopMovingMap", timeToSlide);
        Destroy(previousMap);
    }

    private void Update()
    {
        if (OnTheMove)
        {
            current.transform.position += (-current.transform.position)*0.05f;
        }
    }

    private void StopMovingMap()
    {
        Debug.Log("map Stop Move ");
        OnTheMove = false;
        current.transform.position = Vector3.zero;

        previousMap = current;
    }

    public Vector3 GetCurrentMapSpawnPoint()
    {
        return current.GetComponent<ScMap>().GetSpawnPoint() - current.transform.position;
    }
}
