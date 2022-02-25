using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static AudioClip demo_acceleration;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        demo_acceleration = Resources.Load<AudioClip> ("demoacceleration");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "demodeceleration":
                audioSrc.PlayOneShot (demo_acceleration);
                break;
        }
    }
}
