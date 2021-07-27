using System.Collections;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
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
        GameObject enemyCopy = Instantiate(DataHolder.listEnemy[_currentEnemy - 1]);
        enemyCopy.transform.position = transform.position;
    }

    void Update()
    {
        if (!_coroutine && _currentEnemy != DataHolder.listEnemy.Length) 
            StartCoroutine("Respawn", 5f);
    }
}
