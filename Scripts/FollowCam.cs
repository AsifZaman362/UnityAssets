using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCam : MonoBehaviour {

	[SerializeField]
	private float followSpeed = 0.3f;
	[SerializeField]
	private Vector3 endPos1, endPos2;

	private GameObject target;

	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	

	void FixedUpdate () {
		Vector3 target_pos = new Vector3 (target.transform.position.x, transform.position.y, -10);
		if(target_pos.x<endPos1.x){
			//target = endPos1;
		}
		else if(target_pos.x>endPos2.x){
			//transform.position = endPos2;
		}
		else{
			transform.position = Vector3.MoveTowards(transform.position,target_pos,followSpeed);
		}
	}

}
