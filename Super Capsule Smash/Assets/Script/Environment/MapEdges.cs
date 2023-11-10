using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdges : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player out of map");
        }
        else if (collision.gameObject.CompareTag("Objects")) 
        {
            Destroy(collision.gameObject);
            Debug.Log("Object out of map");
        }
        else
        {
            Debug.Log("Other out of map");
        }
    }
}
