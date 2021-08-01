using System.Collections;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    private bool _coroutine = false;
    private int _currentEnemy = 0;

    void Start()
    {
        Time.timeScale = 1f;
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
        GameObject enemyCopy = Instantiate(DataHolder.enemyList[_currentEnemy - 1]);
        enemyCopy.transform.position = transform.position;
    }

    void Update()
    {
        if (!_coroutine && _currentEnemy != DataHolder.enemyList.Length) 
            StartCoroutine("Respawn", 5f);
    }
}
