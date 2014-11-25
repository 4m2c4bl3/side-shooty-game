using UnityEngine;
using System.Collections;

public class playSound : MonoBehaviour {

    public AudioClip[] sounds;
    public AudioSource sfx;
    public AudioSource bgm;
    public static playSound p;
    string lastLevel;

    void Start()
    {          
       DontDestroyOnLoad(transform.gameObject);
       p = this;
    }

    public void Play(int clip)
    {
        sfx.clip = sounds[clip];
        sfx.Play ();
    }

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName != lastLevel)
        {
            bgMusic();
        }
        if (Application.loadedLevelName == "game")
        {
            gameObject.transform.position = Character.mainChar.gameObject.transform.position;
        }
    }

    public void bgMusic ()
    {
        if (Application.loadedLevelName == "title" || Application.loadedLevelName == "over")
        {
            bgm.clip = sounds[8];
            bgm.Play();
            lastLevel = Application.loadedLevelName;
            
        }
        if (Application.loadedLevelName == "game")
        {
                bgm.clip = sounds[10];
                bgm.Play();
                lastLevel = Application.loadedLevelName;

            }


    }

    public void loopPlay(int clip, bool on)
    {
        if (on)
        {
            sfx.loop = true;
            sfx.clip = sounds[clip];
            sfx.Play();
        }

        if (!on)
        {
            sfx.loop = false;
        }

    }


}
