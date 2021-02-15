using UnityEngine;
using System.Collections;

public class DynamicMover : MonoBehaviour {

    // Dynamic Obstacles
    public GameObject dynamic_obstacle;
    public Vector3 dynamic_start;
    public Vector3 dynamic_end;
    private float startLerp = 0.0f;
    private float lerpIncrement = 0.01f;
    private bool this_way = false;

	// Use this for initialization
	void Start () {
		dynamic_obstacle.transform.position = dynamic_start;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(dynamic_obstacle.transform.position, dynamic_start) == 0 && this_way == false){
			this_way = true;
			startLerp = 0.0f;
    		lerpIncrement = 0.01f;
		}else if(Vector3.Distance(dynamic_obstacle.transform.position, dynamic_end) == 0 && this_way == true){
			this_way = false;
			startLerp = 0.0f;
    		lerpIncrement = 0.01f;
		}

		if(this_way){
			dynamic_obstacle.transform.position = Vector3.Lerp(dynamic_start, dynamic_end, startLerp);
			startLerp += lerpIncrement;
		}else if(!this_way){
			dynamic_obstacle.transform.position = Vector3.Lerp(dynamic_end, dynamic_start, startLerp);
			startLerp += lerpIncrement;
		}
	}
}