using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuUI : MonoBehaviour
{
    public Animator animator;
    public string sceneManager;
    public float Sec;

    public void StartGame()
    {
        StartCoroutine(FadAndLoad());
    }

    IEnumerator FadAndLoad()
    {
        animator.SetTrigger("Fade");

        yield return new WaitForSeconds(Sec);

        SceneManager.LoadScene(sceneManager);
    }
}
