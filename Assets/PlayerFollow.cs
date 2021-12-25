using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform player;
    int back_counter = 1;
    public int max_size = 4;
    public Transform back_parent;

    public Sprite[] backs;
    public List<GameObject> backsList;
    GameObject back;
    public GameObject backPref;

    void Start()
    {
        backsList = new List<GameObject>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.position.y < transform.position.y - Camera.main.orthographicSize)
        {
            Debug.Log("GAME OVER");
            player.GetComponent<Player>().GameOver();
        }

        if (player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
        if (backsList.Count > 0)
        {
            if (transform.position.y > backsList[backsList.Count - 1].transform.position.y)
            {
                back_counter++;
                back = Instantiate(backPref, back_parent);
                if (back_counter >= max_size)
                {
                    back.AddComponent<SpriteRenderer>().sprite = backs[backs.Length - 1];
                    back.transform.position = new Vector3(back.transform.position.x,
                        back.transform.position.y + Camera.main.orthographicSize * 2 * (back_counter - 1),
                        back.transform.position.z);
                }
                else
                {
                    back.AddComponent<SpriteRenderer>().sprite = backs[1];
                    back.transform.position = new Vector3(back.transform.position.x,
                        back.transform.position.y + Camera.main.orthographicSize * 2 * (back_counter - 1),
                        back.transform.position.z);
                    Debug.Log(back.transform.position);
                }
                backsList.Add(back);

                if (transform.position.y > backsList[0].transform.position.y + Camera.main.orthographicSize * 2)
                {
                    Destroy(backsList[0]);
                    backsList.RemoveAt(0);
                }
            }
        }
        else
        {
            back_counter++;
            Debug.Log(back_counter);
            back = Instantiate(backPref, back_parent);
            if (back_counter == 2)
            {
                back.AddComponent<SpriteRenderer>().sprite = backs[0];
                back.transform.position = new Vector3(back.transform.position.x,
                    back.transform.position.y + Camera.main.orthographicSize * back_counter,
                    back.transform.position.z);
            }
            backsList.Add(back);
        }
    }
}
