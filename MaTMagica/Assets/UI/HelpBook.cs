using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBook : MonoBehaviour
{
    [SerializeField]
    public Canvas HelpBookCanvas;

    private bool isShown = false;
    void Start()
    {
        
    }

    public void Hide()
    {
        HelpBookCanvas.enabled = false;
        Time.timeScale = 1;
        isShown = false;
    }

    void Show()
    {
        HelpBookCanvas.enabled = true;
        Time.timeScale = 0;
        isShown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && Time.timeScale != 0 )
        {
            Show();
        }
        else if (Input.GetKeyDown(KeyCode.T) && isShown)
        {
            Hide();
        }
    }
}
