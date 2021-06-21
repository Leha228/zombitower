using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using System;

public class Arrow : MonoBehaviour
{

    Rigidbody2D rb;
    private bool hasHit;
    public GameObject arrow;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        rotate();
    }

    private void rotate()
    {
        if (!hasHit)
        {
            var dir = rb.velocity;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "samurai") Destroy(arrow);

        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

}
