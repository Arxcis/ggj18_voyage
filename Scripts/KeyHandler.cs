using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHandler : MonoBehaviour {

    private GameObject helpMenu;
    private bool paused = false;


    // Use this for initialization
    void Start () {
        helpMenu = GameObject.FindGameObjectWithTag("help-menu");
        helpMenu.SetActive(false);
    }
    
    // Update is called once per frame
    void Update () {
        
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("key Q");

            // @doc https://docs.unity3d.com/ScriptReference/Application.Quit.html - 27.01
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("key R");

            // Load current scene
            // @doc https://answers.unity.com/questions/802253/how-to-restart-scene-properly.html - 27.01.18
            Application.LoadLevel(Application.loadedLevel);

            // Load specific scene
            // @doc https://answers.unity.com/questions/1286832/restarting-scene.html - 27.01.18
            /*
            SceneManager.LoadScene("YourScene");
            Debug.Log("Scene Restarted");
            */
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("key ESC");
        }

        //
        // TOGGLE HELP MENU
        //
        if (Input.GetKeyDown(KeyCode.Escape) && !helpMenu.activeSelf) {
            // @doc https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html- 27.01.18
            Debug.Log("Showing menu");
            helpMenu.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Escape) && helpMenu.activeSelf){
            Debug.Log("Hiding menu");
            helpMenu.SetActive(false);
        }

        //
        // TOGGLE PAUSE
        //
        if (Input.GetKeyDown(KeyCode.P) && !paused) {
            paused = true;
            // @doc - https://answers.unity.com/answers/1231035/view.html - 27.01.2018
            Time.timeScale = 0;
            Debug.Log("PAUSING GAME");
        } else if (Input.GetKeyDown(KeyCode.P) && paused) {
            paused = false; 
            Time.timeScale = 1;
            Debug.Log("RUNNING GAME");            
        }
        /*
        if (Input.GetKey(KeyCode.Keypad1)) {
            Debug.Log("key 1");
        }
        if (Input.GetKey(KeyCode.Keypad2)) {
            Debug.Log("key 2");
        }
        */
    }
}
