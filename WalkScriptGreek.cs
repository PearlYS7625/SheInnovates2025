
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkScriptGreek : MonoBehaviour
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
        transform.Translate(Input.GetAxis("Horizontal") * 9f * Time.deltaTime, 0f, 0f);

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
        if (transform.position.x <= -10f && transform.position.x >= -17f && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene("Hallway2");
        }
        if (transform.position.x <= 20f && transform.position.x >= 16f && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene("WoodenBoxPuzzle");
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

