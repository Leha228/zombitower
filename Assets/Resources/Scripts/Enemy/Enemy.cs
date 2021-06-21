using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject enemy;
    private Object enemyRef;
    private Object respawnPointPref;
    private GameObject respawnPoint;

    void Start()
    {
        enemyRef = Resources.Load("Prefabs/enemyPrefab");
        respawnPointPref = Resources.Load("Prefabs/respawnPoint");
        respawnPoint = (GameObject)Instantiate(respawnPointPref);
    }

    void Update()
    {
        run();
    }

    private void run()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    private void respawn() 
    {
        GameObject enemyCopy = (GameObject)Instantiate(enemyRef);
        enemyCopy.transform.position = respawnPoint.transform.position;

        Destroy(enemy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name != "arrow(Clone)") return;
        
        enemy.SetActive(false);
        Invoke(nameof(respawn), 1f);
    }
}
