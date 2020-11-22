using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   [SerializeField]Image timerImage;
   [SerializeField] ButtonScript[] buttons;
   [SerializeField]AudioClip wrong, right,timerClip;
   [SerializeField] AudioSource timerSource;
    [SerializeField] GameObject[] crosses;
   


    List<int> toPress = new List<int>();
    List<int> pressed=new List<int>();
   public bool waiting = false;
    int timesPressed=0;
    int correct = 0;
   public int wrongTimes = 0;
    float maxTime=10f;
    AudioSource source;
    Info info;
    bool canRestart = true;
    public bool LoopWorking;

    public DayChangingScript DCS;

    void Start()
    {
        Invoke("NormalExecution", 4f);
    }

    #region GamePlay
    IEnumerator PlayNotes()
    {
        LoopWorking = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
            for (int i = 0; i < buttons.Length; i++)
        {
            if (!GameManager.hasFinishedTutorial)
            {
                AudioSource source = buttons[i].GetComponent<AudioSource>();
                source.Play();
                buttons[i].ShowSelected();
                toPress.Add(i);
                yield return new WaitForSeconds(source.clip.length);
            }
            else
            {
                int rand = Random.Range(0, buttons.Length);
                AudioSource source = buttons[rand].GetComponent<AudioSource>();
                source.Play();
                buttons[rand].ShowSelected();
                toPress.Add(rand);
                yield return new WaitForSeconds(source.clip.length);
            }
        }
        LoopWorking = false;
        waiting = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        float currentTime = maxTime;
        while (waiting && currentTime > 0f)
        {
            currentTime -= 1f;
            timerImage.fillAmount = (currentTime / maxTime);
            if (currentTime==5f)
            {
                if (!timerSource.isPlaying)
                {
                    timerSource.PlayOneShot(timerClip);
                }
            }
            if (currentTime < 1f)
            {
                waiting = false;
                ShowXs(true);
                info.ResetWinStreak();
                Invoke("StartAgain", 2f);
            }
           yield return new WaitForSeconds(1f);
        }
        ShowWon(false);
        source.Stop();
                
    }

    void NormalExecution()
    {
        StartCoroutine(Normal_Execution_Coroutine());
    }
    IEnumerator Normal_Execution_Coroutine()
    {
        while (DCS.Day_Change_Start == true)
        {
            yield return new WaitForSeconds(0.001f);
        }
        timerImage.fillAmount = 0f;
        source = GetComponent<AudioSource>();
        StartCoroutine(PlayNotes());
        info = FindObjectOfType<Info>();
        yield return null;
    }
    public void Recognise(ButtonScript button)
    {
        if (waiting)
        {
            if (timesPressed<toPress.Count)
            {
                switch (button.name)
                {
                    case "1":
                        pressed.Add(1);
                        timesPressed++;
                        break;
                    case "2":
                        pressed.Add(2);
                        timesPressed++;
                        break;
                    case "3":
                        pressed.Add(3);
                        timesPressed++;
                        break;
                    case "4":
                        pressed.Add(4);
                        timesPressed++;
                        break;
                    case "5":
                        pressed.Add(5);
                        timesPressed++;
                        break;
                    case "6":
                        pressed.Add(6);
                        timesPressed++;
                        break;
                    default:
                        break;
                }              
            }
            else
            {
                return;
            }

            CheckIfCorrect(button);
        }
    }


    void CheckIfCorrect(ButtonScript butt)
    {
        //Pressed buttons start at 1 and toPress start at 0 so you have to minus here when checking
        if (pressed[timesPressed - 1]-1 == toPress[timesPressed - 1])
        {
            correct++;            
            if (correct == 6)
            {
              ShowWon(true);
              if (!source.isPlaying)
                {
                    AudioSource[] sources = FindObjectsOfType<AudioSource>();
                    foreach (var item in sources)
                    {
                        item.Stop();
                    }
                    source.PlayOneShot(right);
                }
                info.AddTimesCorrect();
                StopAllCoroutines();
                GameManager.hasFinishedTutorial = true;
                ShowXs(false);
              Invoke("StartAgain",2f);
            }
        }
        else
        {
            butt.ColorChanger();
            waiting = false;
            info.ResetWinStreak();
            ShowXs(true);
            if (!source.isPlaying)
            {
                AudioSource[] sources = FindObjectsOfType<AudioSource>();
                foreach (var item in sources)
                {
                    item.Stop();
                }
                source.PlayOneShot(wrong);
            }
            if (canRestart)
            {
            Invoke("StartAgain", 1.5f);            
            }
        }
        }   


    void StartAgain()
    {
        //Reset Every counter then go on
        StartCoroutine(Start_Coroutine());
    }
    IEnumerator Start_Coroutine()
    {
        while(DCS.Day_Change_Start == true)
        {
            yield return new WaitForSeconds(0.001f);
        }
        timerSource.Stop();
        toPress.Clear();
        pressed.Clear();
        timesPressed = 0;
        correct = 0;
        ResetColors();
        ResetTimer();
        StartCoroutine(PlayNotes());
        yield return null;
    }
    #endregion

    void ResetTimer()
    {
        timerImage.fillAmount = 0;        
    }

    private static void ResetColors()
    {
        ButtonScript[] buttons = FindObjectsOfType<ButtonScript>();
        foreach (ButtonScript button in buttons)
        {
            button.ResetColors();            
        }
    }

    void ShowWon(bool c)
    {

        ButtonScript[] buttons = FindObjectsOfType<ButtonScript>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SwitchOn(c);
        }

    }
    void ShowXs(bool isWrong)
    {
        //If wrong you aad times wrong and Show Crosses accordingly 
        if (isWrong)
        {
            wrongTimes++;
            if (wrongTimes == 1)
            {
                crosses[0].SetActive(true);
            }
            if (wrongTimes == 2)
            {
                crosses[1].SetActive(true);
            }
            if (wrongTimes == 3)
            {
                crosses[2].SetActive(true);
            }
            if (wrongTimes == 4)
            {
                info.Invoke("Showdisgrace", 2f);
                foreach (var cross in crosses)
                {
                    cross.SetActive(false);
                }
                wrongTimes = 0;
                canRestart = false;
            }
        }

        // else just clear screen and reset wrong counter
        else
        {
            foreach (var cross in crosses)
            {
                cross.SetActive(false);
            }
            wrongTimes = 0;
        }

    }
}


