using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                nextLine();

            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    void startDialogue()
    {
        index = 0;
        StartCoroutine(typeLine());
    }
    IEnumerator typeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void nextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text  = string.Empty;
            StartCoroutine(typeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
}
