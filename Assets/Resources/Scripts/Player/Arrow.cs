using UnityEngine;
using System;

public class Arrow : MonoBehaviour
{

    Rigidbody2D rb;
    private bool _hasHit;
    public GameObject arrow;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_hasHit)
        {
            var dir = rb.velocity;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "samurai") Destroy(arrow);

        _hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

}
