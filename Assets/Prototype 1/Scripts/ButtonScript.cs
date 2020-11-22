using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Sprite Day, Night;
    public Sprite Selected_Sprite;
    public DayChangingScript DCS;
    PlayerController manager;
    Animator animator;
    Button thisButton;
    ColorBlock temp;
    [SerializeField] Color wrongColor;

    private void Start()
    {
      manager = FindObjectOfType<PlayerController>();
      animator = GetComponent<Animator>();
      thisButton = GetComponent<Button>();
      temp = thisButton.colors;
    }
    public void PlayTone()
    {
        AudioSource source = GetComponent<AudioSource>();

        if (source.isPlaying) source.Stop();
        if (!source.isPlaying&&manager.waiting)
        {
            source.Play();
        }       
    }

    public void ShowSelected()
    {
        Button but = GetComponent<Button>();
        but.image.sprite = Selected_Sprite;
        but.Select();
    }
    public void Reset_Tile()
    {
        //to check is deselect called or not
        if (DCS.isDay== true)
        {
            this.GetComponent<Button>().image.sprite = Day;
        }
        else
        {
            this.GetComponent<Button>().image.sprite = Night;
        }
    }
    public void PressedBox()
    {
        Button but = GetComponent<Button>();
        but.image.sprite = Selected_Sprite;
        manager = FindObjectOfType<PlayerController>();
        manager.Recognise(this);
    }
    public void ColorChanger()
    {
        ColorBlock cBlock = thisButton.colors;
        cBlock.pressedColor = wrongColor;
        cBlock.selectedColor = wrongColor;
        thisButton.colors = cBlock;
    }

    public void ResetColors()
    {
        thisButton.colors = temp;
        Reset_Tile();
    }
    public void SwitchOn(bool color)
    {
        if (color)
        {
            ColorBlock on = thisButton.colors;
            on.normalColor = Color.green;
            thisButton.colors = on;
        }
        else
        {
            ColorBlock on = thisButton.colors;
            on.normalColor =wrongColor;
            thisButton.colors = on;
        }
    }


}
