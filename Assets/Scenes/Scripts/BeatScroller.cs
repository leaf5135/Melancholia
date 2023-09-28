using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    public float bpm;
    public bool hasStarted;
    public int direction;
    // Start is called before the first frame update
    void Start()
    {
        bpm = bpm / 60f; //how fast you move per second
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted) {
            if (Input.anyKeyDown) {
                hasStarted = true;
            }
        } else {
            if (direction == 0) {
                transform.position -= new Vector3(0f, bpm * Time.deltaTime, 0f);
            } else if (direction == 1) {
                transform.position += new Vector3(0f, bpm * Time.deltaTime, 0f);
            } else if (direction == 2) {
                transform.position -= new Vector3(bpm * Time.deltaTime, 0f, 0f);
            } else if (direction == 3) {
                transform.position += new Vector3(bpm * Time.deltaTime, 0f, 0f);
            }
        }
    }
}
