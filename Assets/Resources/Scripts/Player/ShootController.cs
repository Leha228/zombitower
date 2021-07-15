using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{

    public GameObject peak;
    public float launchForce = 4f;

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
    }

    public void LaunchForceUp() {
        _fireState = false;
        for (int i = 0; i < numberOfPoints; i++) { points[i].SetActive(false); }
        Shoot();
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
                points[i] = Instantiate(point, transform.position, Quaternion.identity);
            }
            _createPoints = false;
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(true);
            points[i].transform.position = PointPosition(i * spaceBetweenPoint);
        }
    }

    private void Shoot() {
        GameObject newArrow = Instantiate(peak, transform.position, transform.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)transform.position +
            (Vector2)(transform.right * launchForce * t) + (Physics2D.gravity * (t * t) * 0.5f);
        return position;
    }
}
