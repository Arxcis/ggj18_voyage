using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
    : MonoBehaviour
{
	public enum MusicTrack
    {
        Menu,
        GameOver,
        Victory,
        None,
    };

	public enum SfxTrack
    {
        ButtonPress,
    }

    public void Awake()
    {
        Debug.Log("In awake");
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.mute = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

        musicTracks = new Dictionary<MusicTrack, AudioClip>();
        foreach (MusicTrack value in Enum.GetValues(typeof(MusicTrack)))
        {
            if (value != MusicTrack.None)
            {
                Debug.Log("Loading track");
                var val = Resources.Load<AudioClip>(value.ToString());
                if (!val)
                {
                    Debug.Log("Fuck");
                }
                musicTracks[value] = val;
            }
        }

        sfxTracks = new Dictionary<SfxTrack, AudioClip>();
        foreach (SfxTrack value in Enum.GetValues(typeof(SfxTrack)))
        {
            sfxTracks[value] = Resources.Load<AudioClip>(value.ToString());
        }

    }

	public void PlayMusic(MusicTrack track)
    {
        if (track != currentMusicTrack)
        {
            Debug.Log("Play Music");
            currentMusicTrack = track;

            musicSource.Stop();
            musicSource.clip = musicTracks[track];
            musicSource.Play();
        }
    }

	public void PlaySoundEffect(SfxTrack track)
    {
        sfxSource.PlayOneShot(sfxTracks[track]);
    }

    public bool MuteMusic
    {
        set
        {
            musicSource.mute = value;
        }
        get
        {
            return musicSource.mute;
        }
    }

    public bool MuteSfx
    {
        set
        {
            sfxSource.mute = value;
        }
        get
        {
            return sfxSource.mute;
        }
    }

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private Dictionary<SfxTrack, AudioClip> sfxTracks;
    private MusicTrack currentMusicTrack = MusicTrack.None;

    private Dictionary<MusicTrack, AudioClip> musicTracks;
}
