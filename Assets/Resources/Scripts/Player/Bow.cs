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

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints = 5;
    public float spaceBetweenPoint;
    private bool _createPoints = false;
    private bool _fireState = false;

    void Start()
    {
        points = new GameObject[numberOfPoints];
        _createPoints = true;
    }

    public void LaunchForceDown() {
        launchForce = 4;
        _fireState = true;
        PlayerController.singleton.Aim();
    }

    public void LaunchForceUp() {
        _fireState = false;
        for (int i = 0; i < numberOfPoints; i++) { points[i].SetActive(false); }
        Invoke(nameof(Shoot), 0.2f);
        PlayerController.singleton.Attack();
    }

    void Update()
    {
        if (_fireState) {
            if (launchForce > 14) return;
            launchForce += 4 * Time.deltaTime;
            Move();
        }
    }

    private void Move()
    {       
        if (_createPoints)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i] = Instantiate(point, shootPoint.position, Quaternion.identity);
            }
            _createPoints = false;
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(true);
            points[i].transform.position = PointPosition(i * spaceBetweenPoint);
        }
    }

    private void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shootPoint.position +
            (Vector2)(transform.right * launchForce * t) + (Physics2D.gravity * (t * t) * 0.5f);
        return position;
    }

}
