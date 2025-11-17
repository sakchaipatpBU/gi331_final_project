using System.Collections;
using UnityEngine;

public class Icon : MonoBehaviour
{

    public float time;
    private void Start()
    {
        Destroy(gameObject, time);
    }

    
}
