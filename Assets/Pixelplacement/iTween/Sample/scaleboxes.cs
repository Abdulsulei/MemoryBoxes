using UnityEngine;
using System.Collections;

public class scaleboxes : MonoBehaviour
{
	public float speed;
	void Start(){
		

		iTween.ScaleTo(gameObject, iTween.Hash("x", 2.2f, "y", 2.2f, "islocal",true,"isdelay","0.4f" ,"easeType", iTween.EaseType.easeInOutBounce,"loopType",iTween.LoopType.loop, "time", speed, "onComplete", ""));
	}
}

