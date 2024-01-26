using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public Image selectedSkinImage;
    public TextMeshProUGUI selectedSkinText;
    public List<Sprite> skinsSprites = new();
    public List<string> skinsNames = new();

    int selectedSkin = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("DinoNum", 0);
        PlayerPrefs.Save();
    }

    public void NextSkin()
    {
        selectedSkin++;
        if (selectedSkin == skinsSprites.Count)
        {
            selectedSkin = 0;
        }
        selectedSkinImage.sprite = skinsSprites[selectedSkin];
        selectedSkinText.SetText(skinsNames[selectedSkin]);

        PlayerPrefs.SetInt("DinoNum", selectedSkin);
        PlayerPrefs.Save();
    }

    public void PreviousSkin()
    {
        selectedSkin--;
        if (selectedSkin < 0)
        {
            selectedSkin = skinsSprites.Count - 1;
        }
        selectedSkinImage.sprite = skinsSprites[selectedSkin];
        selectedSkinText.SetText(skinsNames[selectedSkin]);

        PlayerPrefs.SetInt("DinoNum", selectedSkin);
        PlayerPrefs.Save();
    }
}
