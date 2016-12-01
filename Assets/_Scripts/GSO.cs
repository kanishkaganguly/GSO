using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GSO : MonoBehaviour
{
	#region Fields
	[SerializeField]
	private PlayerMove player;
	[SerializeField]
	private GameObject init;

	[SerializeField]
	public Text run_txt;
	private float runtime = 0;
	[SerializeField]
	public Text rad_txt;
	[SerializeField]
	public Text way_txt;

	//private Collider[] neighbours;
	private Collider[] goals;
    Dictionary<Vector3, float> obstacleInfo;

    private int lowerObstacleThreshold = 7;
    private int higherObstacleThreshold = 13;
    private int obstacleCount = 0;
    private float radius = 100.0f;
    private float adaptiveRadius = 10.0f;
    // private float maxRadius = 2000.0f;
    // private float minRadius = 100.0f;

    private int x_low = 0;
    private int x_high = 10000;
    private int z_low = 0;
    private int z_high = -9901;

	#endregion

	#region Properties
	public PlayerMove Player {
		get {
			return player;
		}

		set {
			player = value;
		}
	}
	public GameObject Init {
		get {
			return init;
		}

		set {
			init = value;
		}
	}
	#endregion

	#region MonoBehaviour
	// Use this for initialization
	void Start(){
        obstacleInfo = Init.GetComponent<InitScript>().getObstacleInfo();
        Player.moveTo(new Vector3(Random.Range(x_low,x_high), 1.0f, Random.Range(z_low,z_high)));
        runtime = (float)System.Math.Round(Time.time, 2);
        run_txt.text="Runtime: " + runtime + " s";
        rad_txt.text = "Radius: " + radius;
        way_txt.text = "Waypoints: " + obstacleCount;
    }

    void FixedUpdate(){
        Debug.Log("BOOM");
    }

    void Update(){
        if (!GoalCheck()){
            NeighbourCheck();
            runtime = (float)System.Math.Round(Time.time, 2);
            run_txt.text = "Runtime: " + runtime + " s";
            rad_txt.text = "Radius: " + radius;
            way_txt.text = "Waypoints: " + obstacleCount;
        }
    }
    
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Player.transform.position, radius);
    }
	#endregion

	#region ClassMethods
	bool GoalCheck(){
        //float goal_radius = 500.0f;
        float goal_radius = 800.0f;
        Vector3 goal_origin = Player.transform.position;
        goals = Physics.OverlapSphere(goal_origin, goal_radius);
        foreach(Collider goal in goals){
            if(goal.name == "Goal"){
                Player.LocalPlanner(goal.transform.position);
                return true;
            }
        }
        return false;
    }

    void NeighbourCheck(){
        float pos = 0;
        Vector3 origin = Player.transform.position;
        Vector3 min_vector = origin;
        float min = float.MaxValue;
        obstacleCount = 0;

        Collider[] neighbours = Physics.OverlapSphere(origin, radius);
        foreach (Collider neighbour in neighbours){
            if(neighbour.CompareTag("waypoint")){
                neighbour.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                obstacleCount++;
            }
        }

        if (obstacleCount <= lowerObstacleThreshold)
        {
            radius += adaptiveRadius;
        }
        else if (obstacleCount >= higherObstacleThreshold)
        {
            radius -= adaptiveRadius;
        }

        foreach (Collider neighbour in neighbours)
        {
            if (obstacleInfo.TryGetValue(neighbour.transform.position, out pos))
            {
                if (pos < min)
                {
                    min = pos;
                    min_vector = neighbour.transform.position;
                }
            }
        }
        Player.LocalPlanner(min_vector);
    }
	#endregion
}