using UnityEngine;
using System.Collections;

public class ObstacleTrigger : MonoBehaviour {

	public GameObject obstacle;
	private Color originalMat;
	private Color neighbourMat;

// void OnTriggerEnter(Collider other) {
 //        neighbourMat = Color.yellow;
 //        obstacle.GetComponent<Renderer>().material.SetColor("_Color", neighbourMat);
 //    }

    // void OnTriggerExit(Collider other) {
    //     originalMat = Color.green;
    //     obstacle.GetComponent<Renderer>().material.SetColor("_Color", originalMat);
    // }
}