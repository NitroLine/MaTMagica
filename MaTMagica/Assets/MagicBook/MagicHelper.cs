using System.Collections;
using System.Collections.Generic;
using Assets.MAGIC;
using UnityEngine;
using System.Linq;


public class MagicHelper : MonoBehaviour
{
    // Start is called before the first frame update

    public MagicBook magicBook;
    public UI_updater uiUpdater;
    void Start()
    {
        
    }

    public int BinarySearch(KeyCombination combination, KeyCode item)
    {
        var temp = combination.Combinations;
        temp.Sort();
        var left = 0;
        var right = temp.Count -1;
        if (right < 0)
            return 0;
        while (left < right)
        {
            var mid = (left + right) / 2;
            var x = temp[mid];
            if (x == item)
            {
                combination.Combinations.Remove(x);
                return 1;
            }

            if (item < x)
                right = mid;
            else
                left = mid + 1;
        }

        if (temp[left] != item) return 0;
        combination.Combinations.Remove(item);
        return 1;
    }



    // Update is called once per frame
    public void UpdateHelp()
    {
        var curCodes = magicBook.pressedCodes;
        if (curCodes.Count == 0) return;
        var possibleMagiks = magicBook
            .KeyCombinationsToMagik
            .Keys
            .ToDictionary(x => x, y => MagicBook.GetComb(y.Combinations.ToArray()));
        var likes = new Dictionary<KeyCombination, int>();
        foreach (var key in curCodes)
        {
            foreach (var pair in possibleMagiks)
            {
                if (likes.ContainsKey(pair.Key))
                    likes[pair.Key] += BinarySearch(pair.Value, key);
                else
                    likes[pair.Key] = BinarySearch(pair.Value, key);
            }
        }
       
        var help = likes
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key.Combinations.Count)
            .FirstOrDefault()
            .Key;
        uiUpdater.ClearHelpCanvas();
        if (help.Combinations.Count != curCodes.Count + possibleMagiks[help].Combinations.Count)
            return;
        uiUpdater.PutHelpName(magicBook.KeyCombinationsToMagik[help].Name);
        foreach (var key in curCodes)
        {
            uiUpdater.AddImageOnButton(key,true);   
        }
        foreach (var key in possibleMagiks[help].Combinations)
        {
            uiUpdater.AddImageOnButton(key, true,true);
        }
    }
}
