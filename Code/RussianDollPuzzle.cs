using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class LeftRightPuzzle : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public GameObject[] arrowIndicators; // Array of arrow GameObjects
    public GameObject openDoll; // Object to reveal on success
    public GameObject closedDoll;
    public GameObject coin;
    public TextMeshProUGUI dollText;
    private string PendingPreviousScene;

    private List<string> playerInput = new List<string>();
    private string[] correctSequence = { "Right", "Left", "Left", "Right" }; // Change as needed
    private int inputIndex = 0;

    void Start()
    {
        leftButton.onClick.AddListener(() => RegisterInput("Left"));
        rightButton.onClick.AddListener(() => RegisterInput("Right"));
        ResetArrows();
        closedDoll.SetActive(true);
        openDoll.SetActive(false); // Hide Russian Doll at start
    }

    void RegisterInput(string direction)
    {
        if (inputIndex >= 4) return; // Prevent extra inputs

        playerInput.Add(direction);
        UpdateArrowIndicators(direction, inputIndex);
        inputIndex++;

        if (inputIndex == 4) CheckSolution();
    }

    void UpdateArrowIndicators(string direction, int index)
    {
        if (index < arrowIndicators.Length)
        {
            arrowIndicators[index].SetActive(true);
            if (direction == "Left")
            {
                arrowIndicators[index].transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                arrowIndicators[index].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void CheckSolution()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playerInput[i] != correctSequence[i])
            {
                StartCoroutine(ResetPuzzle());
                return;
            }
        }
        OpenRussianDoll();
    }

    async void OpenRussianDoll()
    {
        openDoll.SetActive(true);
        coin.SetActive(true);
        closedDoll.SetActive(false);
        await Task.Delay(2000);
        LoadScene("RussianRoom");
    }

    IEnumerator ResetPuzzle()
    {
        // Display "Wrong Answer..." text
        dollText.text = "Not Quite...";
        dollText.color = Color.white;

        // Wait for 2 seconds
        yield return new WaitForSeconds(1f);

        // Clear the feedback text and reset the puzzle
        dollText.text = "";
        playerInput.Clear();
        inputIndex = 0;
        ResetArrows();
    }

    void ResetArrows()
    {
        foreach (GameObject arrow in arrowIndicators)
        {
            arrow.SetActive(false);
            arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void LoadScene(string SceneNameToLoad)
    {
        PendingPreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += ActivatorAndUnloader;
        SceneManager.LoadScene(SceneNameToLoad, LoadSceneMode.Additive);
    }

    void ActivatorAndUnloader(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= ActivatorAndUnloader;
        SceneManager.SetActiveScene(scene);
        SceneManager.UnloadSceneAsync(PendingPreviousScene);
    }
}
