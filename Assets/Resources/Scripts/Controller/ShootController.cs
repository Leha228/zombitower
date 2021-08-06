using UnityEngine;
using Spine.Unity;
using Spine;

public class ShootController : MonoBehaviour
{
    public static ShootController singleton { get; private set; }

    public GameObject peak;
    public GameObject point;
    GameObject[] points;
    public float launchForce = 4f;
    public int damage;
    public int numberOfPoints = 5;
    public float spaceBetweenPoint;

    private bool _createPoints = false;
    private bool _fireState = false;
    private string _currentState = "idle";

    private void Awake() { singleton = this; }
   
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

        for (int i = 0; i < numberOfPoints; i++) 
            points[i].SetActive(false); 

        _currentState = "attack";
        TowerModel.singleton.SetAnimation(TowerModel.singleton.attack[UserModel.singleton.GetActiveTower()], false);
        Shoot();
    }
    
    void Update()
    {
        if (_fireState) 
        {
            if (launchForce > 16) return;
            if (_currentState != "aim")
            {
                _currentState = "aim";
                TowerModel.singleton.SetAnimation(TowerModel.singleton.aiming[UserModel.singleton.GetActiveTower()], false);
            }
            
            launchForce += 4 * Time.deltaTime;
            Move();
        } 
        else 
            if (_currentState != "idle")
            {
                _currentState = "idle";
                TowerModel.singleton.SetAnimation(TowerModel.singleton.idle[UserModel.singleton.GetActiveTower()], true);
            }
    }

    private void Move()
    {       
        if (_createPoints)
        {
            for (int i = 0; i < numberOfPoints; i++) 
                points[i] = Instantiate(point, transform.position, Quaternion.identity);

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
