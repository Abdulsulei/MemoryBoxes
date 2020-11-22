using UnityEngine;
using System.Collections;

public class rotatebyu : MonoBehaviour
{
	public float speed;
	void Start(){
		

		iTween.RotateTo(gameObject, iTween.Hash("z", -5f,"islocal",true,"easeType", iTween.EaseType.linear,"loopType",iTween.LoopType.pingPong, "time", speed, "onComplete", ""));
	}
}

