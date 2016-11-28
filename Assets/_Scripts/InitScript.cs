using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitScript : MonoBehaviour
{

    // Path Creator Vars
    public GameObject block;
    public GameObject player;
    public GameObject border;
    public Transform goal;
    private int rows = 100;
    private int cols = 100;
    private Vector3 position;
    private Vector3 player_pos;
    private Vector3 border_loc;
    private Vector3 goal_loc = new Vector3(4500.0f, 1.0f, -500.0f);
    private Vector3 goal_near = new Vector3(4800.0f, 1.0f, -500.0f);
    bool init = false;

    // Obstacle Creator Vars
    public GameObject obstacle;
    private int x_low = 0;
    private int x_high = 10000;
    private int z_low = 0;
    private int z_high = -9901;
    static int obstacle_count = 50;
    private Vector3 obstacle_loc;

    // Game State Vars
    public Dictionary<Vector3, float> obstacle_locs = new Dictionary<Vector3, float>();

    // Use this for initialization
    void Start()
    {
        position.Set(0.0f, 0.0f, 0.0f);
        player_pos.Set(0.0f, 1.0f, 0.0f);
        goal.position = goal_loc;
        Instantiate(obstacle, goal_near, Quaternion.identity);
        placeBlock();
        placeBorder();
        placeObstacle();
    }

    void placeBorder()
    {
        for (int j = z_high; j <= z_low; j += 100)
        {
            position.Set(0.0f, 0.0f, 0.0f);
            border_loc.Set(x_low, 1.0f, j);
            GameObject square = (GameObject)Instantiate(border, border_loc, Quaternion.identity);
            border_loc.Set(x_high, 1.0f, j);
            square = (GameObject)Instantiate(border, border_loc, Quaternion.identity);
        }
        for (int i = x_low; i <= x_high; i += 100)
        {
            position.Set(0.0f, 0.0f, 0.0f);
            border_loc.Set(i, 1.0f, z_low);
            GameObject square = (GameObject)Instantiate(border, border_loc, Quaternion.identity);
            border_loc.Set(i, 1.0f, z_high);
            square = (GameObject)Instantiate(border, border_loc, Quaternion.identity);
        }
    }

    void placeObstacle()
    {
        for (int i = 0; i < obstacle_count; i++)
        {
            obstacle_loc.Set(Random.Range(x_low, x_high), 100.0f, Random.Range(z_low, z_high));
            GameObject square = (GameObject)Instantiate(obstacle, obstacle_loc, Quaternion.identity);
            square.name = "Obstacle_" + i;
            obstacle_locs.Add(square.transform.position, euclideanDistance(square.transform.position, goal_loc));
        }
    }

    void placeBlock()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                position.Set(position.x + 100.0f, 0.0f, position.z);
                player_pos.Set(position.x + 0.0f, 1.0f, position.z);
                if (init == false)
                {
                    player.transform.position = player_pos;
                    init = true;
                }
                GameObject square = (GameObject)Instantiate(block, position, Quaternion.identity);
                square.name = "block_(" + i + "," + j +")";
            }
            position.Set(0.0f, 0.0f, position.z - 100.0f);
        }
    }

    float euclideanDistance(Vector3 from, Vector3 to){
        float distance = Mathf.Sqrt(Mathf.Pow(to.x-from.x,2)+Mathf.Pow(to.z-from.z,2));
        return distance;
    }

    float manhattanDistance(Vector3 from, Vector3 to)
    {
        float distance = Mathf.Abs(to.x - from.x) + Mathf.Abs(to.z - from.z);
        return distance;
    }

    public Dictionary<Vector3,float> getObstacleInfo()
    {
        return obstacle_locs;
    }
}