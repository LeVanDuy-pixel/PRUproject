using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI time;
    public float num;
    private int t;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (num<=0)
        {
            time.text = "Congratulation!";
        }
        else
        {
        num -= Time.deltaTime;
        t = (int)num;
        time.text = (t.ToString());
        }
       
    }
}
