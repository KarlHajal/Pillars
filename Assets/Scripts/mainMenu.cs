using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class mainMenu : MonoBehaviour {

    public string playString;

    public void Play()
    {
        SceneManager.LoadScene(playString);
        Time.timeScale = 1;
    }
		
    public void QuitGame()
    {
        Application.Quit();
    }
}
