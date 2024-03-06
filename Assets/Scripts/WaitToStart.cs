using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitToStart : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waitTime;
    [SerializeField] GameObject panel;
    public bool canPlay =false, stopCount=false;
    public float count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopCount)
        {
            if (count <= 0)
            {
                canPlay = true;
                stopCount = true;
                panel.SetActive(false);
            }
                waitTime.text = ((int)count).ToString();
            
            count -= Time.deltaTime;
        }
        
    }
}
