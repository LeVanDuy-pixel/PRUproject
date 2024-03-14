using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI warning;
    GameController _gameController;

    public float num;
    private int t;
    private int harder = 0;
    void Start()
    {
        _gameController = GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameController.isFinish && !_gameController.isOver)
        {
            if(num <= 53 && harder==0)
            {
                warning.text = "More Bullet!";
                warning.gameObject.SetActive(true);
                StartCoroutine(HideWarningText());
            }
            if (num <= 38)
            {
                if (harder == 1)
                {
                    warning.text = "Speed Up!!";
                    warning.gameObject.SetActive(true);
                    StartCoroutine(HideWarningText());
                }
                Bullet bullet = FindObjectOfType<Bullet>();
                bullet.speed = 6;                            
                PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
                playerMovement.moveSpeed = 6.5f;
            }
            if (num <= 15 && harder == 2)
            {
                warning.text = "Try To Survive!!!";
                warning.gameObject.SetActive(true);
                StartCoroutine(HideWarningText());
            }                
            
            if (num <= 0)
            {
                time.text = "Congratulation!";
                _gameController.isFinish = true;
            }
            if(num>=0 && num<=60)
            {
                t = (int)num;
                time.text = (t.ToString());
            }
            num -= Time.deltaTime;

            
        }
    }
    IEnumerator HideWarningText()
    {
        harder++;
        yield return new WaitForSeconds(1.5f);
        warning.gameObject.SetActive(false);
        
    }
}
