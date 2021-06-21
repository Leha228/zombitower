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
    private bool createPoints = false;
    private bool fireState = false;
    private PlayerController playerController;

    void Start()
    {
        points = new GameObject[numberOfPoints];
        playerController = player.GetComponent<PlayerController>();
        createPoints = true;
    }

    public void launchForceDown() {
        launchForce = 4;
        fireState = true;
        playerController.aim();
    }

    [Obsolete]
    public void launchForceUp() {
        fireState = false;
        for (int i = 0; i < numberOfPoints; i++) { points[i].active = false; }
        Invoke(nameof(shoot), 0.2f);
        playerController.attack();
    }

    [Obsolete]
    void Update()
    {
        if (fireState) {
            if (launchForce > 14) return;
            launchForce += 4 * Time.deltaTime;
            move();
        }
    }

    [Obsolete]
    private void move()
    {       
        if (createPoints)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i] = Instantiate(point, shootPoint.position, Quaternion.identity);
            }
            createPoints = false;
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].active = true;
            points[i].transform.position = pointPosition(i * spaceBetweenPoint);
        }
    }

    private void shoot()
    {
        GameObject newArrow = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    Vector2 pointPosition(float t)
    {
        Vector2 position = (Vector2)shootPoint.position +
            (Vector2)(transform.right * launchForce * t) + (Physics2D.gravity * (t * t) * 0.5f);
        return position;
    }

}
