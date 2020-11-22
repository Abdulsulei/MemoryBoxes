using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{
	public float speed;
	void Start(){
		
		iTween.MoveTo(gameObject, iTween.Hash("x", -1256f,"islocal",true, "easeType", iTween.EaseType.linear,"loopType",iTween.LoopType.loop, "time", speed, "onComplete", ""));
	}
}

