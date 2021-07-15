using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy singleton { get; private set; }

    public float speed;
    public GameObject enemy;
    private Object enemyRef;
    private GameObject respawnPoint;

    private List<string> _collisions;

    void Awake() { singleton = this; }

    void Start()
    {
        _collisions = new List<string> {"arrow(Clone)", "peak(Clone)"};

        enemyRef = Resources.Load("Prefabs/enemyPrefab");
        respawnPoint = GameObject.Find("respawnPoint");
        StartCoroutine(Respawn(5f));
    }

    IEnumerator Respawn(float timeSecond) {
        float counter = 0;

        while (counter < timeSecond) {
            counter += Time.deltaTime;
            yield return null;
        }

        GameObject enemyCopy = (GameObject)Instantiate(enemyRef);
        enemyCopy.transform.position = respawnPoint.transform.position;
    }

    void Update()
    {
        run();
    }

    private void run()
    {
        transform.position = 
            new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_collisions.Contains(collision.collider.name)) return;
        
        enemy.SetActive(false);
        Destroy(enemy);
    }
}
