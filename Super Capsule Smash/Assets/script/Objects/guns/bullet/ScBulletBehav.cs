using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBulletBehav : MonoBehaviour
{
    [SerializeField] int dammage;
    [SerializeField] ParticleSystem hitPart;
    private Vector3 myDirection;
    private float mySpeed;
    private Transform myTrans;
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer rbSprite;
    private bool doOnce;
    private bool isDestroyed;

    private void Start()
    {
        myTrans = transform;
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        doOnce = true;
    }

    private void FixedUpdate()
    {
        if (!isDestroyed)
            myTrans.position = myTrans.position + (myDirection * mySpeed);

        /*if (doOnce)
        {
            doOnce = false;
            rb.AddForce(myDirection*mySpeed * 150,ForceMode2D.Impulse);
            rb.freezeRotation = true;
        }*/

        if (Mathf.Abs(myTrans.position.x) > 24 || Mathf.Abs(myTrans.position.y) > 17)
            Destroy(gameObject);
    }

    public void SetUpBullet(Vector2 direction, float speed)
    {
        myDirection = direction;
        mySpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            var tempo = collision.gameObject;
            
            if (tempo.layer == 9)
            {
                tempo.GetComponent<ScDammage>().GetDammage(dammage, 1, myDirection);
                HitSomething();
            }

            if (tempo.layer == 6)
            {
                HitSomething();
            }

            if (tempo.layer == 16)
            {
                myDirection = -myDirection;
            }
        }
    }

    private void HitSomething()
    {
        isDestroyed = true;
        if (rbSprite != null)
            rbSprite.enabled = false;

        hitPart.Play();
        Invoke("DestroyBullet", hitPart.main.duration);
        if (col != null)
            col.enabled = false;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
