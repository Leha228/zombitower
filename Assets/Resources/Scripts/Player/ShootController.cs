using UnityEngine;
using Spine.Unity;
using Spine;

public class ShootController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset idle, attacking, aiming;

    public GameObject peak;
    public float launchForce = 4f;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints = 5;
    public float spaceBetweenPoint;
    private bool _createPoints = false;
    private bool _fireState = false;
    private string _currentState = "idle";
   
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
            
        SetAnimation("attack", attacking, false);
        Shoot();
    }
    
    void Update()
    {
        if (_fireState) 
        {
            if (launchForce > 16) return;
            if (_currentState != "aim") SetAnimation("aim", aiming, false);
            launchForce += 4 * Time.deltaTime;
            Move();
        } 
        else 
            if (_currentState != "idle") SetAnimation("idle", idle, true);
    }

    private void SetAnimation(string name, AnimationReferenceAsset animation, bool loop) {
        _currentState = name;
        skeletonAnimation.state.SetAnimation(0, animation, loop);
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
