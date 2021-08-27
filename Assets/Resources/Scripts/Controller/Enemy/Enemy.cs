using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
using System;

public class Enemy : MonoBehaviour
{
    public static Enemy singleton { get; private set; }

    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset slow, death;
    public Image healthBar;
    public GameObject hp;

    public float speed;
    public GameObject enemy;
    public int live;
    public List<string> collisions;

    private Transform shootPointLimit;
    private GameObject _progress;
    private bool shootBool = true;
    private float partLive;

    void Awake() { singleton = this; }

    void Start()
    {
        shootPointLimit = GameObject.Find("shootPointLimit").transform;
        _progress = GameObject.Find("ButtonProgress");
        healthBar.fillAmount = 1f;
        partLive = (live / TowerModel.singleton.damage[UserModel.singleton.GetActiveTower()]);
        partLive = 100 / partLive;
        partLive = partLive / 100;
    }

    void Update()
    {
        if (shootBool && Mathf.Round(transform.position.x) == Mathf.Round(shootPointLimit.transform.position.x)) {
            shootBool = false;
            PlayerController.singleton.Attack();
        }

        transform.position =
            new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    public void DeathAnimation() {
        speed = 0;
        skeletonAnimation.state.SetAnimation(0, death, false);
        hp.SetActive(false);

        Invoke("DestroyEnemy", 0.7f);
    }

    private void DestroyEnemy() {
        EventManager.singleton.SetProgress();
        _progress.GetComponentInChildren<Text>().text = EventManager.singleton.GetProgress().ToString() + "%";
        enemy.SetActive(false);
        Destroy(enemy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collisions.Contains(collision.collider.name)) return;

        if (collision.collider.name == collisions[0]) {
            // замедление хотьбы
            skeletonAnimation.state.SetAnimation(0, slow, true);
            speed = speed / 2;
        } else if (collision.collider.name == collisions[2]) {
            // анимация атаки
            speed = 0;
        } else {
            live -= TowerModel.singleton.damage[UserModel.singleton.GetActiveTower()];

            healthBar.fillAmount = healthBar.fillAmount - partLive;

            if (live <= 0) DeathAnimation();
        }
    }
}
