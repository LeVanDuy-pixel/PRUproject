using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTimes : MonoBehaviour
{
    private float[] firstBeatTimes;
    private float[] lastBeatTimes;
    public float[] resultBeatTimes;

    
    private float n = 23.23f;
    void Start()
    {
        firstBeatTimes = new float[] {/*0.2f,*/ 0.41f, /*0.65f,*/0.9f,/*1.1f,*/1.37f
                                    ,/*1.7f,*/2.3f,/*2.55f,*/2.8f,/*3.05f,*/3.3f
                                    ,/*3.65f,*/4.25f,/*4.5f,*/4.7f,/*4.95f,*/5.2f
                                    ,/*5.55f,*/6.15f,/*6.4f,*/6.65f,/*6.9f,*/7.1f
                                    ,7.35f,/*7.6f,*/7.8f,/*8f,*/8.3f/*,8.55f*/
                                    ,8.8f,/*9.05f,*/9.3f,9.45f,9.75f,10f
                                    ,10.25f,10.4f,10.75f,10.995f,11.235f,11.35f
                                    ,11.7f,11.9f,12.18f,12.42f,12.66f,12.9f
                                    ,13.15f,13.3f,13.63f,13.87f,14.03f,14.11f
                                    ,14.27f,14.55f,14.81f,15.08f,15.24f,15.55f
                                    ,15.78f,15.12f,16.25f,16.5f,16.75f,17f
                                    ,17.16f,17.47f,17.71f,17.95f,18.12f,18.43f
                                    ,18.68f,18.92f,19.08f,19.39f,19.64f,19.87f
                                    ,20.12f,20.35f,20.59f,20.83f,21f,21.32f
                                    ,21.56f,21.79f,22.02f,22.27f,22.44f,22.67f,22.9f};
        lastBeatTimes = new float[148];

        for (int i = 0; i < 147; i++)
        {
            lastBeatTimes[i] = n;
            n += 0.25f;
        }
        int aLen = firstBeatTimes.Length, bLen = lastBeatTimes.Length;
        resultBeatTimes = new float[aLen+bLen];
        System.Array.Copy(firstBeatTimes,0,resultBeatTimes,0,aLen);
        System.Array.Copy(lastBeatTimes,0,resultBeatTimes,aLen,bLen);
        Debug.Log(resultBeatTimes.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
