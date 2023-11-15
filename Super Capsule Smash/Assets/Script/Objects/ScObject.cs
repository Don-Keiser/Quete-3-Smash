using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScObject : MonoBehaviour
{
    private GameObject player;
    private GameObject playerHand;
    [SerializeField] private Rigidbody2D rb;

    private bool isAdded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsAdded())
        {
            gameObject.layer = LayerMask.NameToLayer("grabbedObject");
            player = collision.gameObject;
            SetAdded(true);
            Grab();
        }
    }

    private void Grab()
    {
        playerHand = player.transform.GetChild(3).GetChild(0).gameObject;
        this.gameObject.transform.parent = playerHand.transform;
        rb.isKinematic = true;
        this.gameObject.transform.position = new Vector3(playerHand.transform.position.x, playerHand.transform.position.y, playerHand.transform.position.z);
        this.gameObject.transform.rotation = playerHand.transform.rotation;
    }


    public void SetAdded(bool hasBeenAdded)
    {
        isAdded = hasBeenAdded;
    }

    public bool IsAdded()
    {
        return isAdded;
    }
}
