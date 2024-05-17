using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        Debug.Log("Starting Menu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToNextLevel()
    {
        animator.SetTrigger("FadeOut");
    }
}
