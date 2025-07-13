using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (GameManager.instance.lost == true)
        {
            audioSource.Stop();
        }    
        
    }

}
