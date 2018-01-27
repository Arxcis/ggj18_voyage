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
        Level_1,
        None,
    };

	public enum SfxTrack
    {
        ButtonPress,
    }

    public void Awake()
    {
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
                var val = Resources.Load<AudioClip>(value.ToString());
                if (!val)
                {
                    Debug.LogWarningFormat("Could not load file {0}", value.ToString());
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
