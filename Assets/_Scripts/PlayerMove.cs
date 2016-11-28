using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public GameObject player;
    private Collider[] dynamic;
    private Vector3 prev_to = Vector3.zero;

    public void moveTo(Vector3 to)
    {       
        player.transform.position += to;
    }

    public void LocalPlanner(Vector3 to)
    {
        Vector3 origin = player.transform.position;
        float radius = 400.0f;
        int dynamic_count = 0;

        dynamic = Physics.OverlapSphere(origin, radius);
        foreach (Collider dynamic_obstacle in dynamic)
        {
            if (dynamic_obstacle.tag == "dynamic")
            {
                dynamic_count += 1;
            }
        }

        if (dynamic_count == 0)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, to, 50.0f);
            Debug.DrawLine(player.transform.position, to, Color.black, 100.0f);
        }
        else
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, to, -20.0f);
            Debug.DrawLine(to,player.transform.position, Color.red, 100.0f);
        }
    }
}