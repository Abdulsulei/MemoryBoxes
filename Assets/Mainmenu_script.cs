using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mainmenu_script : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        iTween.MoveTo(GameObject.Find("Boxes_Images"), iTween.Hash("y", 150f, "isdelay", "1.0f", "islocal", true, "easeType", iTween.EaseType.easeOutBounce, "time", speed, "onComplete", ""));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
