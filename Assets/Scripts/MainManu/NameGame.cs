using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameGame : MonoBehaviour
{
    public Animator animator;


    public void StartGame()
    {
        animator.Play("FadeIn");
    }

}
