using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject bul, head;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(Vector2.SqrMagnitude(head.transform.position-bul.transform.position));
    }
}
