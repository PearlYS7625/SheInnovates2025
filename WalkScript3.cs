using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkScript3 : MonoBehaviour
{

    Vector3 characterScale;
    float characterScaleX;
    private string PendingPreviousScene;

    void Start()
    {
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
    }

    void Update()
    {

        // Move the Character:
        transform.Translate(Input.GetAxis("Horizontal") * 5f * Time.deltaTime, 0f, 0f);

        // Flip the Character:
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = -characterScaleX;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = characterScaleX;
        }
        transform.localScale = characterScale;

        // Move to new room when player reaches an x bound
        if (transform.position.x <= -7f)
        {
            LoadScene("Entrance");
        }
        if (transform.position.x <= -1f && transform.position.x >= -5f && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene("GreekRoom");
        }
        if (transform.position.x <= 24f && transform.position.x >= 20f && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene("ClosingScene");
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

