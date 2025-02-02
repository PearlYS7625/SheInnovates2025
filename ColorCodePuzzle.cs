using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorCodePuzzle : MonoBehaviour
{
    private List<Color> correctSequence = new List<Color>
    {
        Color.green, Color.blue, Color.red, Color.blue, Color.yellow
    };

    private List<Color> playerSequence = new List<Color>();

    public List<Image> colorSlots; // Drag the slots into this from the Inspector
    public List<Button> colorButtons; // Drag the buttons into this from the Inspector
    public GameObject boxToUnlock; // Closed box GameObject
    public GameObject openBox; // Open box GameObject
    public TextMeshProUGUI feedbackText; // Drag the FeedbackText object here
    public GameObject coin;

    void Start()
    {
        ResetPuzzle();
    }

    public void AddColor(string colorName)
    {
        Color color = Color.white; // Default color
        switch (colorName.ToLower())
        {
            case "red":
                color = Color.red;
                break;
            case "blue":
                color = Color.blue;
                break;
            case "green":
                color = Color.green;
                break;
            case "yellow":
                color = Color.yellow;
                break;
        }

        if (playerSequence.Count < correctSequence.Count)
        {
            playerSequence.Add(color);
            UpdateColorSlots();

            if (playerSequence.Count == correctSequence.Count)
            {
                CheckSolution();
            }
        }
    }

    void UpdateColorSlots()
    {
        for (int i = 0; i < playerSequence.Count; i++)
        {
            colorSlots[i].color = playerSequence[i];
        }
    }

    void CheckSolution()
    {
        bool isCorrect = true;

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("Correct Code!");
            OpenBox();
        }
        else
        {
            StartCoroutine(ShowWrongAnswer());
        }
    }

    IEnumerator ShowWrongAnswer()
    {
        // Display "Wrong Answer..." text
        feedbackText.text = "Not Quite...";
        feedbackText.color = Color.white;

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Clear the feedback text and reset the puzzle
        feedbackText.text = "";
        ResetPuzzle();
    }

    void ResetPuzzle()
    {
        playerSequence.Clear();
        foreach (Image slot in colorSlots)
        {
            slot.color = Color.white;
        }
    }

    void OpenBox()
    {
        // Hide the closed box and show the open box
        boxToUnlock.SetActive(false);
        openBox.SetActive(true);
        coin.SetActive(true);

        // Hide buttons and color slots
        foreach (Button button in colorButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (Image slot in colorSlots)
        {
            slot.gameObject.SetActive(false);
        }
    }
}
