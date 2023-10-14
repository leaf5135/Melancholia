using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;
    public Lane lane;
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
        lane = GetComponentInParent<Lane>();
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));


        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            if (lane.direction == 0) {
                print("left");
                transform.localPosition = Vector2.Lerp(Vector2.right * lane.noteSpawnX, Vector2.right * lane.noteDespawnX, t);
            }
            else if (lane.direction == 1) {
                print("up");
                transform.localPosition = Vector2.Lerp(Vector2.down * lane.noteSpawnY, Vector2.down * lane.noteDespawnY, t);
            }
            else if (lane.direction == 2) {
                print("down");
                transform.localPosition = Vector2.Lerp(Vector2.up * lane.noteSpawnY, Vector2.up * lane.noteDespawnY, t);
            }
            else if (lane.direction == 3) {
                print("right");
                transform.localPosition = Vector2.Lerp(Vector2.left * lane.noteSpawnX, Vector2.left * lane.noteDespawnX, t);
            }
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
