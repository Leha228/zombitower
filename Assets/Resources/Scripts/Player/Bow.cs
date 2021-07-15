
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

    IEnumerator Shoot(float timeSecond) {
        float counter = 0;

        while (counter < timeSecond) {
            counter += Time.deltaTime;
            yield return null;
        }

        Invoke("CreateArrow", 0.2f);
    }

    private void CreateArrow() {
        GameObject newArrow = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }
}
