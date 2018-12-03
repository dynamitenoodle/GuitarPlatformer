using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {
    public enum Obstacles
    {
        net,
        umbrella,
        seaGull
    }
    // attributes
    GameObject player, seagull, net, umbrella;

    public Dictionary<float, Obstacles> completed;
    public Dictionary<float, Obstacles> ObstacleDictionary;
    float bpm;
    public float maxLength = 60;
    float Timer;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        bpm = player.GetComponent<Player>().bpm;
        Timer = player.GetComponent<Player>().Timer;

        ObstacleDictionary = new Dictionary<float, Obstacles>();
        completed = new Dictionary<float, Obstacles>();
        System.Random R = new System.Random();
        for (int i = 0; i < maxLength / 60 * bpm; i++)
        {
            float time = (i / maxLength / 60 * bpm) * maxLength;
            Obstacles entry = (Obstacles)R.Next(0, 5);
            ObstacleDictionary.Add(time, entry);
        }
        seagull = Resources.Load<GameObject>("Prefabs/seagull");
        net = Resources.Load<GameObject>("Prefabs/net");
        umbrella = Resources.Load<GameObject>("Prefabs/umbrella");
    }

    // Update is called once per frame
    void Update () {
        Timer = player.GetComponent<Player>().Timer;
        foreach (float key in ObstacleDictionary.Keys)
        {
            if (Timer - maxLength < .2f && Timer - maxLength > 0f)
            {
                Timer = 0;
                completed = new Dictionary<float, Obstacles>();
            }
            if (Timer % maxLength < key + 1 && Timer % maxLength > key - 1)//same second as the time for the obstacle
            {
                float xSpawn = 10f;

                if (!completed.ContainsKey(key) || completed.ContainsKey(key) && completed[key] != ObstacleDictionary[key])
                {//have not instantiated this key value yet
                    GameObject temp;
                    switch (ObstacleDictionary[key])
                    {
                        case Obstacles.net:
                            temp = Instantiate(net, new Vector3(xSpawn, net.transform.position.y, 0), Quaternion.Euler(Vector3.zero));
                            temp.GetComponent<Obstacle>().bpm = bpm;
                            temp.GetComponent<Obstacle>().direction = new Vector3(-1, 0, 0);
                            completed.Add(key, ObstacleDictionary[key]);
                            break;
                        case Obstacles.umbrella:
                            temp = Instantiate(umbrella, new Vector3(xSpawn, umbrella.transform.position.y, 0), Quaternion.Euler(Vector3.zero));
                            temp.GetComponent<Obstacle>().bpm = bpm;
                            temp.GetComponent<Obstacle>().direction = new Vector3(-1, 0, 0);
                            completed.Add(key, ObstacleDictionary[key]);
                            break;
                        case Obstacles.seaGull:
                            temp = Instantiate(seagull, new Vector3(xSpawn, seagull.transform.position.y, 0), Quaternion.Euler(Vector3.zero));
                            temp.GetComponent<Obstacle>().bpm = bpm;
                            temp.GetComponent<Obstacle>().direction = (player.transform.position - temp.transform.position).normalized;
                            completed.Add(key, ObstacleDictionary[key]);
                            break;
                        default:
                            break;
                    }

                }
            }
        }
        player.GetComponent<Player>().Timer = Timer;
    }
}
