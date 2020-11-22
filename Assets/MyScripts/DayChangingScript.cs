using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayChangingScript : MonoBehaviour
{
    public Image BG;
    public Sprite Day, Night;
   public bool isDay = true;
    public GameObject[] Buttons;
    public float Time;
    public Sprite Selected;
    public Image Timer;
    public Sprite Day_Time, NightTime;
    public PlayerController PC;
    public bool Day_Change_Start = false;
    // Start is called before the first frame update
    void Start()
    {
        //Starting Coroutine for Night and Day shift
        StartCoroutine(Day_Night_Change_Coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Day_Night_Change_Coroutine()
    {
        while (true)
        {
            Day_Change_Start = false;
            yield return new WaitForSeconds(Time);
            while (PC.LoopWorking)
            {
                  yield return new WaitForSeconds(0.01f);  
            }
            float x = 255;
            Day_Change_Start = true;
            //Fading out old Background 
            while (x >= 0.1f)
            {
                x -= 20.0f;
                float Alpha = x / 255.0f;
                //Changing Alpha
                Color C2 = new Color(1f, 1f, 1f, Alpha);
                BG.color = C2;
                yield return new WaitForSeconds(0.01f);
            }
            //Back drop is changed
            if (isDay == true)
            {
                BG.sprite = Night;
                Timer.sprite = NightTime;
            }
            else if (isDay == false)
            {
                Timer.sprite = Day_Time;
                BG.sprite = Day;
            }
            //fading In New Background
            float Val = 0;
            Reset_Buttons();
            Color C = new Color(1f, 1f, 1f, 0f);
            BG.color = C;
            while (Val <= 255)
            {
                Val += 10.0f;
                float Alpha = Val / 255.0f;
                Color C1 = new Color(1f, 1f, 1f, Alpha);
                BG.color = C1;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    public void Reset_Buttons()
    {
        if (isDay == true)
        {
            isDay = false;
        }
        else
        {
            isDay = true;
        }
        for (int i = 0; i < Buttons.Length; i++)
        {
            if(Buttons[i].GetComponent<Button>().image != Selected)
            {
                Buttons[i].GetComponent<ButtonScript>().ResetColors();
            }
        }
    }
}
