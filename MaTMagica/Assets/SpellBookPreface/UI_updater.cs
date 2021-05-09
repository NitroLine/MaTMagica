using System;
using System.Collections;
using System.Collections.Generic;
using Assets.MAGIC;
using UnityEngine;
using UnityEngine.UI;

public class UI_updater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public GameObject iceImage;
    public GameObject leafImage;
    public GameObject ballImage;
    public GameObject shieldImage;
    public GameObject windImage;
    public GameObject iceText;
    public GameObject leafText;
    public GameObject stoneText;
    public GameObject shieldText;
    public GameObject windText;

    private float currentPosition;
    private Stack<GameObject> images = new Stack<GameObject>();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void ClearCanvas()
    {
        while (images.Count > 0)
        {
            Destroy(images.Pop());
        }
        currentPosition = 0;
    }

    public void AddImageOnButton(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.Q:
                AddImage(ballImage);
                break;
            case KeyCode.E:
                AddImage(leafImage);
                break;
            case KeyCode.Alpha1:
                AddImage(iceImage);
                break;
            case KeyCode.Alpha2:
                AddImage(shieldImage);
                break;
            case KeyCode.Alpha3:
                AddImage(windImage);
                break;
        }
    }

    public void UpdateRuneCount(Dictionary<Rune, int> runeDict)
    {
        foreach (var runePair in runeDict)
        {
            switch (runePair.Key)
            {
                case Rune.Ice:
                    iceText.GetComponent<Text>().text = runePair.Value.ToString();
                    break;
                case Rune.Stone:
                    stoneText.GetComponent<Text>().text = runePair.Value.ToString();
                    break;
                case Rune.Wind:
                    windText.GetComponent<Text>().text = runePair.Value.ToString();
                    break;
                case Rune.Leaf:
                    leafText.GetComponent<Text>().text = runePair.Value.ToString();
                    break;
                case Rune.Shield:
                    shieldText.GetComponent<Text>().text = runePair.Value.ToString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void AddImage(GameObject image)
    {
        var delta = Instantiate(image) as GameObject;
        delta.transform.SetParent(canvas.transform);
        var rect = delta.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-60*currentPosition-30,15);
        rect.anchorMin = new Vector2(1, 0);
        rect.anchorMax = new Vector2(1, 0);
        rect.pivot = new Vector2(1, 0);
        currentPosition++;
        images.Push(delta);
    }
}
