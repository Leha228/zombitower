using UnityEngine;
using UnityEngine.Events;

public class Bow : MonoBehaviour
{
    public static Bow singleton { get; private set; }
    public UnityEvent bowEvent;

    public GameObject arrow;
    public GameObject player;
    public float launchForce = 4;
    public Transform shootPoint;
    public Transform shootPointLimit;

    void Awake() { singleton = this; }

    private void Start() {
        bowEvent = new UnityEvent();
        bowEvent.AddListener(CreateArrow);
    }

    private void CreateArrow() {
        Debug.Log("Shoot");

        launchForce = Vector2.Distance(transform.position, shootPointLimit.transform.position); 
        
        GameObject newArrow = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * (launchForce - 3f);
    }
}
