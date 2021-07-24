using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{

    public int quantityEnemy;
    public GameObject enemyRef;

    private bool _coroutine = false;
    private int _currentEnemy = 0;

    void Start()
    {
        StartCoroutine("Respawn", 1f);
    }

    IEnumerator Respawn(float timeSecond) {
        float counter = 0;
        _coroutine = true;
        _currentEnemy += 1;

        while (counter < timeSecond) {
            counter += Time.deltaTime;
            yield return null;
        }

        _coroutine = false;
        GameObject enemyCopy = Instantiate(enemyRef);
        enemyCopy.transform.position = transform.position;
    }

    void Update()
    {
        if (!_coroutine && _currentEnemy != quantityEnemy) 
            StartCoroutine("Respawn", 5f);
    }
}
