using Assets.Scripts.Global;
using UnityEngine;

public class LightArrow : MonoBehaviour
{
    Vector2 vectorMove = Vector2.zero;
    public GameObject targetBullet = null, targetLock;
    float moveSpeed = 20f;
    void Start()
    {
    }

    void Update()
    {
        if (targetBullet != null)
        {
            vectorMove = targetBullet.transform.position - transform.position;
            if (Vector2.SqrMagnitude(vectorMove) > 0.5)
            {
                GetComponent<Rigidbody2D>().velocity = vectorMove.normalized * moveSpeed;
                float angle = -Vector2.SignedAngle(vectorMove, Vector2.right);
                angle = (angle < 0) ? 360 + angle : angle;
                transform.localRotation = Quaternion.Euler(0, 0, angle - 40);
            }
            else
            {
                Destroy(targetBullet);
                Destroy(gameObject);
            }
        }
        if (Vector2.SqrMagnitude(transform.position) > 136)
        {
            Destroy(gameObject);
        }
    }
    public void aim(GameObject bullet)
    {
        gameObject.SetActive(true);
        targetBullet = bullet;
        Instantiate(targetLock, bullet.transform.position + new Vector3(0.3f, 0), Quaternion.identity).transform.parent = bullet.transform;
    }
}
