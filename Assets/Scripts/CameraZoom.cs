using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Camera cam;
    Rigidbody2D player;
    bool zoomIn;
    [Range(1, 10)]
    public float zoomSize;
    [Range(0.01f, 0.1f)]
    public float zoomSpeed;
    [Range(1, 3)]
    public float waitTime;
    float waitCounter;

    private void Awake()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void ZoomIn()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomSize, zoomSpeed);
    }

    void ZoomOut()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, zoomSpeed);
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(player.velocity.magnitude) < 8)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter > waitTime)
            {
                zoomIn = true;
            }
        }
        else
        {
            zoomIn = false;
            waitCounter = 0;
        }

        if (zoomIn)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }
}
