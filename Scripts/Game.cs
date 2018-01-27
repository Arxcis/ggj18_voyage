using UnityEngine.SceneManagement;
using UnityEngine;

public class Game
    : MonoBehaviour
{
    private GameObject helpMenu;
    private GameObject pauseMenu;
    private bool paused = false;

    AudioManager audioManager;

    static bool initialized = false;

    string[] scenes = new string[] { "splash", "level_1_intro", "level_1", "level_2_intro",  "level_2", "outro"};
    AudioManager.MusicTrack[] tracks = new AudioManager.MusicTrack[]
    {
        AudioManager.MusicTrack.Menu,
        AudioManager.MusicTrack.Level_1,
        AudioManager.MusicTrack.Level_1,
        AudioManager.MusicTrack.Level_1,
        AudioManager.MusicTrack.Level_1,
        AudioManager.MusicTrack.Level_1,
    };


    static int currScene = 0;

    static float sceneTimeout = 0.0f;

    float time = 0.0f;

    // Use this for initialization
    void Start()
    {
        helpMenu = GameObject.FindGameObjectWithTag("help-menu");
        pauseMenu = GameObject.FindGameObjectWithTag("pause-menu");

        helpMenu.SetActive(false);
        pauseMenu.SetActive(false);

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayMusic(tracks[currScene]);

        if (!initialized)
        {
            initialized = true;
            Debug.Log("Initialized");
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
            Debug.Log("key R");

            // Load current scene
            // @doc https://answers.unity.com/questions/802253/how-to-restart-scene-properly.html - 27.01.18
            //Application.LoadLevel(Application.loadedLevel);

            // Load specific scene
            // @doc https://answers.unity.com/questions/1286832/restarting-scene.html - 27.01.18
            /*
            SceneManager.LoadScene("YourScene");
            Debug.Log("Scene Restarted");
            */

            currScene--;
            NextScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("key ESC");
        }

        //
        // TOGGLE HELP MENU
        //
        if (Input.GetKeyDown(KeyCode.Escape) && !helpMenu.activeSelf)
        {
            // @doc https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html- 27.01.18
            Debug.Log("Showing menu");
            helpMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && helpMenu.activeSelf)
        {
            Debug.Log("Hiding menu");
            helpMenu.SetActive(false);
        }

        //
        // TOGGLE PAUSE
        //
        if (Input.GetKeyDown(KeyCode.P) && !paused)
        {
            paused = true;
            // @doc - https://answers.unity.com/answers/1231035/view.html - 27.01.2018

            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("PAUSING GAME");
        }
        else if (Input.GetKeyDown(KeyCode.P) && paused)
        {

            paused = false;

            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("RUNNING GAME");
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
        GotoScene("game_over");
    }

    public void NextScene()
    {
        Debug.Log("NextScene!");
        currScene++;
        sceneTimeout = 3.0f;
        GotoScene(scenes[currScene]);
    }

    public void GotoScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void CheckForNextScene()
    {
        sceneTimeout -= Time.deltaTime;
        // For Splash screen to start the game
        if (currScene == 0 && Input.GetKeyUp("space"))
        {
            NextScene();
            return;
        }

        // All scenes that require interactions happens to be divisable by 2.
        if (currScene % 2 != 0 && sceneTimeout <= 0.0f)
        {
            NextScene();
        }
    }
}
