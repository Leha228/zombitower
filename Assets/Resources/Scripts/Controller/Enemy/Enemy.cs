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
    public int live;

    private Transform shootPointLimit;
    private List<string> _collisions;
    private bool shootBool = true;

    void Awake() { singleton = this; }

    void Start()
    {
        _collisions = new List<string> {
            "arrow(Clone)", 
            "arrow 1(Clone)", 
            "knight(Clone)"
        };
        shootPointLimit = GameObject.Find("shootPointLimit").transform;
    }

    void Update()
    {
        //if (!singleton.isActiveAndEnabled) DestroyEnemy();
        if (shootBool && Mathf.Round(transform.position.x) == Mathf.Round(shootPointLimit.transform.position.x)) {
            shootBool = false;
            Bow.singleton.bowEvent.Invoke();
        }

        transform.position = 
            new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    public void DeathAnimation() {
        speed = 0;
        skeletonAnimation.state.SetAnimation(0, death, false);
        Invoke("DestroyEnemy", 0.7f);
    }

    private void DestroyEnemy() {
        EventManager.singleton.SetProgress();
        enemy.SetActive(false);
        Destroy(enemy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_collisions.Contains(collision.collider.name)) return;
        
        if (collision.collider.name == _collisions[0]) {
            // замедление хотьбы
            skeletonAnimation.state.SetAnimation(0, slow, true);
            speed = speed / 2;
        } else if (collision.collider.name == _collisions[2]) { 
            // анимация атаки
            speed = 0; 
        } else {
            live -= ShootController.singleton.damage;
            if (live <= 0) {
                DeathAnimation();
            }
        }
    }
}