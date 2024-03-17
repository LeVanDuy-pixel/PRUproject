using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startingSceneTransition;
    [SerializeField]
    private GameObject endingSceneTransition;

    private void Start()
    {
        startingSceneTransition.SetActive(true);
        //FunctionTimer.Create(DisableStartingSceneTransition, 5f);
    }

    private void DisableStartingSceneTransition()
    {
        startingSceneTransition.SetActive(true);
    }
}
