using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.LowLevel;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] Sentences;
    private int Index = 0;
    public int space = 0;
    public float DialogueSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && space < 3)
        {
            NextSentence();
            space++;
        } else if (space == 3)
        {
            DialogueText.text = "";
        }
    }

    void NextSentence()
    {
        if (Index <= Sentences.Length - 1)
        {
            DialogueText.text = "";
            StartCoroutine(WriteScentence());
        }
    }

    IEnumerator WriteScentence()
    {
        foreach (char Character in Sentences[Index].ToCharArray()) 
        {
            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
        Index++;
    }
}
