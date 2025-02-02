using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Vector3 characterScale;
    float characterScaleX;
    private string PendingPreviousScene;
    Animator animator;
    Rigidbody2D rb;


    void Start()
    {
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("xVelocity", (Math.Abs(rb.linearVelocity.x)));

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
        if (transform.position.x >= 14f)
        {
            LoadScene("Hallway2");
        }
        else if (transform.position.x <= -12f)
        {
            LoadScene("Hallway1");
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

