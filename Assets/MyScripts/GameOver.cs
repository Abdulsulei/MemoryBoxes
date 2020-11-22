using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public float speed;
    public Text Score;
    public Text High_Score;
    public GameObject Particle;
    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveTo(GameObject.Find("GameOver"), iTween.Hash("y", 181.9f, "isdelay", "1.0f", "islocal", true, "easeType", iTween.EaseType.easeOutBounce, "time", speed, "onComplete", ""));
        int Score_ = PlayerPrefs.GetInt("Score");
        int High_Score_ = PlayerPrefs.GetInt("HighScore");
        High_Score.text = High_Score_.ToString();
        Score.text = Score_.ToString();
        if (High_Score_ <= Score_)
        {
            Particle.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Reset_Scene()
    {
        SceneManager.LoadScene(1);
    }
}
