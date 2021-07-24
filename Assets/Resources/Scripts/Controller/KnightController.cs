using System.Collections;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    
    private void Start() {
        StartCoroutine("ReplaceAnimation", 1.2f);
    }

    IEnumerator ReplaceAnimation(float timeSecond) {
        float counter = 0;

        while (counter < timeSecond) {
            counter += Time.deltaTime;
            yield return null;
        }

        KnightModel.singleton.SetAnimation(KnightModel.singleton.walk, true);
    }

    private void Update() {
        transform.position = 
            new Vector2(transform.position.x + KnightModel.singleton.speed * Time.deltaTime, transform.position.y);
    }

    private void Kill(Collision2D other) => other.gameObject.GetComponent<Enemy>().DeathAnimation();

    private void OnCollisionEnter2D(Collision2D other) {
        if (!KnightModel.singleton.collisions.Contains(other.gameObject.name)) return;
        KnightModel.singleton.speed = 0;
        KnightModel.singleton.SetAnimation(KnightModel.singleton.attack, true);
        other.gameObject.GetComponent<Enemy>().DeathAnimation();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!KnightModel.singleton.collisions.Contains(other.gameObject.name)) return;
        KnightModel.singleton.speed = 1.5f;
        KnightModel.singleton.SetAnimation(KnightModel.singleton.walk, true);
    }
}
