using UnityEngine;
using System.Collections;

public class scale : MonoBehaviour
{
	public float speed;
	void Start(){
		
		iTween.ScaleTo(gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "islocal",true ,"easeType", iTween.EaseType.linear,"loopType",iTween.LoopType.pingPong, "time", speed, "onComplete", ""));
	}
}

