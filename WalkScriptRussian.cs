using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkScriptRussian : MonoBehaviour
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
        transform.Translate(Input.GetAxis("Horizontal") * 14f * Time.deltaTime, 0f, 0f);

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
        if (transform.position.x <= -111f && transform.position.x >= -116f && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene("Hallway1");
        }
        if (transform.position.x <= -52f && transform.position.x >= -59f && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene("RussianDollPuzzle");
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
