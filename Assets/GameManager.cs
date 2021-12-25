using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pref;
    public GameObject colorChangerPref;
    public Queue<GameObject> circlesQueue;
    public Queue<GameObject> colorChangerQueue;
    public List<GameObject> activeColorChangers;
    public List<GameObject> activeCircles;
    public float distance = 0.1f;

    public SpriteRenderer player;

    public Color[] colors;

    public int queueSize = 3;
    // Start is called before the first frame update
    void Start()
    {
        activeCircles = new List<GameObject>();
        activeColorChangers = new List<GameObject>();
        circlesQueue = new Queue<GameObject>();
        colorChangerQueue = new Queue<GameObject>();

        for (int i = 0; i < queueSize; i++)
        {
            GameObject circle = Instantiate(pref, transform);
            circle.name = "circle " + i;
            GameObject colorChanger = Instantiate(colorChangerPref, transform);
            colorChanger.name = "colorChanger " + i;
            circle.SetActive(false);
            colorChanger.SetActive(false);
            circlesQueue.Enqueue(circle);
            colorChangerQueue.Enqueue(colorChanger);
        }        
    }

    public void CreateCircles()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject circle = circlesQueue.Dequeue();
            circle.SetActive(true);

            if (activeCircles.Count != 0)
            {
                circle.transform.position = new Vector3(circle.transform.position.x,
                activeCircles[activeCircles.Count - 1].transform.position.y + distance * 2,
                circle.transform.position.z);
                activeCircles.Add(circle);
            }
            else
            {
                circle.transform.position = new Vector3(0f, 0f, 0f);
                activeCircles.Add(circle);
            }

            SetCircleColors();

            GameObject colorChanger = colorChangerQueue.Dequeue();
            colorChanger.SetActive(true);
            colorChanger.transform.position = new Vector3(colorChanger.transform.position.x,
                activeCircles[activeCircles.Count - 1].transform.position.y + distance, colorChanger.transform.position.z);
            activeColorChangers.Add(colorChanger);
            SetColorChangerColor();
        }
    }
    public void InstantiateCircle()
    {
        GameObject obj = activeCircles[0];
        obj.SetActive(false);
        circlesQueue.Enqueue(obj);
        activeCircles.Remove(obj);
        obj = circlesQueue.Dequeue();
        obj.SetActive(true);
        obj.transform.position = new Vector3(obj.transform.position.x,
                activeCircles[0].transform.position.y + distance * 2,
                obj.transform.position.z);
        activeCircles.Insert(activeCircles.Count, obj);
        SetCircleColors();
    }

    public void InstantiateColorChanger()
    {
        GameObject obj = activeColorChangers[0];
        obj.SetActive(false);
        colorChangerQueue.Enqueue(obj);
        activeColorChangers.RemoveAt(0);
        obj = colorChangerQueue.Dequeue();
        obj.SetActive(true);
        obj.transform.position = new Vector3(obj.transform.position.x,
            activeColorChangers[0].transform.position.y + distance * 2, 
            obj.transform.position.z);        
        activeColorChangers.Insert(activeColorChangers.Count, obj);
        SetColorChangerColor();
    }


    public void SetPlayerColor()
    {
        player.color = colors[Random.Range(0, colors.Length)];
    }

    public void SetColorChangerColor()
    {
        activeColorChangers[activeColorChangers.Count - 1].GetComponent<SpriteRenderer>().color = 
            colors[Random.Range(0, colors.Length)];
    }

    public void SetCircleColors()
    {
        Transform circle = activeCircles[0].transform;
        for (int i = 0; i < 4; i++)
        {
            circle.GetChild(i).GetComponent<SpriteRenderer>().color = colors[i];
        }
    }
}
