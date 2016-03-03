using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

    public int direction = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Slowly rotate the object arond its X axis at 1 degree/second.
        transform.Rotate(Time.deltaTime, 0, 0);

        // ... at the same time as spinning it relative to the global 
        // Y axis at the same speed.
        transform.Rotate(0, Time.deltaTime * direction, 0, Space.World);
    }
}
