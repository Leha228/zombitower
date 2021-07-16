using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public GameObject player;
    public float launchForce = 4;
    public Transform shootPoint;

    private bool _coroutine = false;

    private void Update()
    {
        if (_coroutine) return;
        
        _coroutine = true;
        Invoke("CreateArrow", 5f);
    }

    private void CreateArrow() {
        if (Enemy.singleton == null) return;

        launchForce = Vector2.Distance(transform.position, Enemy.singleton.transform.position); 

        /*Debug.Log(Mathf.Round(launchForce));
        if (Mathf.Round(launchForce) > 19) {
             _coroutine = false; 
             return;
        }*/

        GameObject newArrow = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * (launchForce - 1f);

        _coroutine = false;
    }
}
