using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightArrow : MonoBehaviour
{
    Vector2 vectorMove = Vector2.zero;
    GameObject targetBullet = null;
    void Start()
    {
    }

    void Update()
    {
        if (targetBullet != null)
        {
            vectorMove = targetBullet.transform.localPosition - transform.localPosition;
            if (Vector2.SqrMagnitude(vectorMove) > 0.1)
            {
                GetComponent<Rigidbody2D>().velocity = vectorMove.normalized * 16f;
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
    }
    public void aim(GameObject bullet)
    {
        gameObject.SetActive(true);
        targetBullet = bullet;
    }
}
