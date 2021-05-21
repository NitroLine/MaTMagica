using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Diolog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public string[] phrases = {""};
    [SerializeField]
    public Texture[] images = new Texture[0];
    [SerializeField]
    public int[] whenImageChange = { 0 };
    [SerializeField]
    public string[] names = {""};
    [SerializeField]
    public int[] whenNamesChange = { 0 };
    [SerializeField]
    public Texture[] backgrounds = new Texture[0];
    [SerializeField]
    public int[] whenBackgroundChange = { 0 };
    [SerializeField] 
    public Text textPlace;
    [SerializeField] 
    public RawImage imagePlace;
    [SerializeField] 
    public Text namePlace;
    [SerializeField] 
    public RawImage backgroundPlace;
    [SerializeField] 
    public Canvas nextUI;
    [SerializeField] 
    public bool isStarted;

    public bool IsEnded => curPhrase >= phrases.Length;
    private int curPhrase = -1;
    private int curImage = -1;
    private int curName = -1;
    private int curBackground = -1;
    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = isStarted;
        if (isStarted)
            nextUI.enabled = false;
        imagePlace.color = imagePlace.texture == null ? 
            new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
        backgroundPlace.color = backgroundPlace.texture == null ? 
            new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
        if (isStarted)
            StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted && (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)))
            NextPhrase();
    }

    public void StartDialog()
    {
        curPhrase = -1;
        curImage = -1;
        curName = -1;
        curBackground = -1;
        textPlace.text = "";
        isStarted = true;
        gameObject.GetComponent<Canvas>().enabled = true;
        nextUI.enabled = false;
        imagePlace.color = imagePlace.texture == null ? 
            new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
        backgroundPlace.color = backgroundPlace.texture == null ? 
            new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
        Time.timeScale = 0;
    }

    public void NextPhrase()
    {
        if (!isStarted) return;
        curPhrase += 1;
        if (IsEnded)
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            nextUI.enabled = true;
            isStarted = false;
            Time.timeScale = 1;
            return;
        }

        if (whenImageChange.Contains(curPhrase))
        {
            curImage++;
            if (curImage < images.Length)
                imagePlace.texture = images[curImage];
            imagePlace.color = imagePlace.texture == null ? 
                new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
        }

        if (whenBackgroundChange.Contains(curPhrase))
        {
            curBackground++;
            if (curBackground < backgrounds.Length)
                backgroundPlace.texture = backgrounds[curBackground];
            backgroundPlace.color = backgroundPlace.texture == null ? 
                    new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
        }

        if (whenNamesChange.Contains(curPhrase))
        {
            curName++;
            if (curName < names.Length)
                namePlace.text = names[curName];
        }
        
        textPlace.text = phrases[curPhrase];
    }
}
