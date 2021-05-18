using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
            MakePause();
    }
    void MakePause()
    {
        Time.timeScale = 0;
        gameObject.GetComponent<Canvas>().enabled = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    } 
}
