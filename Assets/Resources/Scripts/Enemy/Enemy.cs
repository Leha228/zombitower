using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Enemy : MonoBehaviour
{
    public static Enemy singleton { get; private set; }

    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset slow, death;
    public float speed;
    public GameObject enemy;
    private List<string> _collisions;
    private bool _death = false;

    void Awake() { singleton = this; }

    void Start()
    {
        _collisions = new List<string> {"arrow(Clone)", "peak(Clone)"};
    }

    void Update()
    {
        transform.position = 
            new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    private void DestroyEnemy() {
        enemy.SetActive(false);
        Destroy(enemy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_collisions.Contains(collision.collider.name)) return;
        
        if (!_death) 
        {
            skeletonAnimation.state.SetAnimation(0, slow, true);
            speed = speed / 2;
            _death = true;
        } 
        else 
        {
            speed = 0;
            skeletonAnimation.state.SetAnimation(0, death, false);
            Invoke("DestroyEnemy", 0.7f);
        }
    }
}
