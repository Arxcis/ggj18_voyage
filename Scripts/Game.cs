﻿using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game
    : MonoBehaviour
{
    private GameObject helpMenu;

    static AudioManager audioManager;
    static Game instance;

    static bool initialized = false;

    string[] scenes = new string[] { "splash", "level_1", "level_2_intro", "level_2", "outro" };
    AudioManager.MusicTrack[] tracks = new AudioManager.MusicTrack[]
    {
        AudioManager.MusicTrack.Menu,
        AudioManager.MusicTrack.Level_1,
        AudioManager.MusicTrack.Level_1,
        AudioManager.MusicTrack.Level_1,
        AudioManager.MusicTrack.Happyending,
    };


    static int currScene = 0;

    static float sceneTimeout = 0.0f;

    float time = 0.0f;

    // Use this for initialization
    void Start()
    {
        if (!initialized)
        {
            instance = this;
            audioManager = gameObject.AddComponent<AudioManager>();
            audioManager.PlayMusic(tracks[currScene]);

            initialized = true;
            Debug.Log("Initialized");

            DontDestroyOnLoad(gameObject);
        }

        if (this != instance)
        {
            instance.helpMenu = this.helpMenu;
            //Debug.Log("Destroying");
            Destroy(this);
        }

    }

    void Awake()
    {
        helpMenu = GameObject.FindGameObjectWithTag("help-menu");
        try
        {
            helpMenu.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Could not setActive help and pausemenu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("key Q");

            // @doc https://docs.unity3d.com/ScriptReference/Application.Quit.html - 27.01
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currScene--;
            NextScene();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "game_over")
            {
                currScene--;
                NextScene();
            }
        }

        //
        // TOGGLE HELP MENU
        //
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!helpMenu)
            {
                helpMenu = GameObject.FindGameObjectWithTag("help-menu");
            }
            bool active = helpMenu.activeSelf;
            helpMenu.SetActive(!active);

            audioManager.PlaySoundEffect(AudioManager.SfxTrack.Pause);
            if (!active)
            {
               Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        //
        // Check if map finished
        //
        // Change this to change how high the level is
        CheckForNextScene();

        time += Time.deltaTime;
        //Debug.Log(string.Format("Time: {0}, Y: {1}", time, playerObject.transform.position.y));
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        //audioManager.PlayMusic(AudioManager.MusicTrack.Gameover);
        GotoScene("game_over");
    }

    public void NextScene()
    {
        Debug.Log("NextScene!");
        currScene++;
        sceneTimeout = 3.0f;
        audioManager.PlayMusic(tracks[currScene]);
        GotoScene(scenes[currScene]);
    }

    public void GotoScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public bool IsLevel2()
    {
        return currScene == 3;
    }

    private void CheckForNextScene()
    {
        sceneTimeout -= Time.deltaTime;
        // For Splash screen to start the game
        if (currScene == 0 && Input.GetKeyUp("space"))
        {
            audioManager.PlaySoundEffect(AudioManager.SfxTrack.Begin);
            NextScene();
            return;
        }

        // All scenes that require interactions happens to be divisable by 2.
        if (currScene == 2 && sceneTimeout <= 0.0f)
        {
            NextScene();
        }
    }
}
