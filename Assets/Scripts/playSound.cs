using UnityEngine;
using System.Collections;

public class playSound : MonoBehaviour {

    public AudioClip[] sounds;
    public static playSound p;

    void Start()
    {
        p = this;
    }

    public void Play(int clip)
    {
        audio.clip = sounds[clip];
        audio.Play ();
    }

    public void loopPlay(int clip, bool on)
    {
        if (on)
        {
            audio.loop = true;
            audio.clip = sounds[clip];
            audio.Play();
        }

        if (!on)
        {
            audio.loop = false;
        }

    }


}
