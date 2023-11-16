

using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public Transform CameraPosition;

    // Update is called once per frame
    void Update() {
        transform.position = CameraPosition.position;
    }
}
