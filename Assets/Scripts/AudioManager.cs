using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    public bool loop;
    
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour {
    public static AudioManager instance = null;

    [SerializeField] private Sound[] sounds;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;

            s.source.playOnAwake = false;
            s.source.bypassEffects = true;
            s.source.bypassListenerEffects = true;
            s.source.bypassReverbZones = true;
        }

        Play("BGM");
    }

    public Sound Play(string name) {
        Sound s = System.Array.Find<Sound>(sounds, sound => sound.name.Equals(name));
        if (s == null || s.source == null) {
            Debug.LogWarning($"Sound {name} does not exist!");
        } else {
            s?.source.Play();
        }
        return s;
    }

    // TODO: Add options for volume/SFX adjustment and disabling?
    Sound Play(int i) {
        Sound s = sounds[i];
        if (s == null || s.source == null) {
            Debug.LogWarning($"Sound {name} does not exist!");
        } else {
            s?.source.Play();
        }
        return s;
    }
}