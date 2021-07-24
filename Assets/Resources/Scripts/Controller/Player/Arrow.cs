using UnityEngine;
using System.Collections.Generic;

public class Arrow : MonoBehaviour
{

    Rigidbody2D rb;
    private bool _hasHit;
    public GameObject arrow;
    
    private List<string> _collisions;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _collisions = new List<string> {"zombie_great", "samurai"};
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
        if (!_collisions.Contains(collision.collider.name)) Destroy(arrow);

        _hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

}
