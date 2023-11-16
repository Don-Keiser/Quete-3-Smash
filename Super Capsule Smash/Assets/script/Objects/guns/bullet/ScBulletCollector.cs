using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBulletCollector : MonoBehaviour
{
    public static ScBulletCollector Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }

    }

    public void GetNewBullet()
    {

    }
}
