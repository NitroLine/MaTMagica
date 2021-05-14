using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Diolog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public string[] Phrases = {""};
    [SerializeField]
    public Texture[] Images = new Texture[0];
    [SerializeField]
    public int[] WhenImageChange = { 0 };
    [SerializeField]
    public string[] Names = {""};
    [SerializeField]
    public int[] WhenNamesChange = { 0 };
    [SerializeField]
    public Texture[] Backgrounds = new Texture[0];
    [SerializeField]
    public int[] WhenBackgroundChange = { 0 };
    [SerializeField] public Text TextPlace;
    [SerializeField] public RawImage ImagePlace;
    [SerializeField] public Text NamePlace;
    [SerializeField] public RawImage BackgroundPlace;
    [SerializeField] public Canvas NextUI;
    [SerializeField] public bool isStarted;

    private int curPhrase = -1;
    private int curImage = -1;
    private int curName = -1;
    private int curBackground = -1;
    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = isStarted;
        NextUI.enabled = !isStarted;
        ImagePlace.color = ImagePlace.texture == null ? new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
        BackgroundPlace.color = BackgroundPlace.texture == null ? new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted && Input.GetMouseButtonUp(0))
            NextPhrase();
    }

    public void StartDialog()
    {
        curPhrase = -1;
        curImage = -1;
        curName = -1;
        curBackground = -1;
        TextPlace.text = "";
        isStarted = true;
        gameObject.GetComponent<Canvas>().enabled = true;
        NextUI.enabled = false;
        ImagePlace.color = ImagePlace.texture == null ? new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
        BackgroundPlace.color = BackgroundPlace.texture == null ? new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);

    }

    public void NextPhrase()
    {
        curPhrase += 1;
        if (curPhrase >= Phrases.Length)
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            NextUI.enabled = true;
            isStarted = false;
            return;
        }

        if (WhenImageChange.Contains(curPhrase))
        {
            curImage++;
            if (curImage < Images.Length)
                ImagePlace.texture = Images[curImage];
            ImagePlace.color = ImagePlace.texture == null ? new Color(0,0,0,0) : new Color(255, 255, 255, 1);
        }

        if (WhenBackgroundChange.Contains(curPhrase))
        {
            curBackground++;
            if (curBackground < Backgrounds.Length)
                BackgroundPlace.texture = Backgrounds[curBackground];
            BackgroundPlace.color = BackgroundPlace.texture == null ? new Color(0, 0, 0, 0) : new Color(255, 255, 255, 1);
        }

        if (WhenNamesChange.Contains(curPhrase))
        {
            curName++;
            if (curName < Names.Length)
                NamePlace.text = Names[curName];
        }

        TextPlace.text = Phrases[curPhrase];
    }

    
}