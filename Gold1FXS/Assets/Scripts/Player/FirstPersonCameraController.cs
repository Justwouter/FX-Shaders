

using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour {

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    public Transform Orientation;

    private float rotationX;
    private float rotationY;

    void Start() {
        // Making the cursor locked to the screen and no longer visible.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;
        rotationX -= mouseY;

        // Making sure we can't go beyond 90 degrees up and down
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
