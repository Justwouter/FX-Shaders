
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float groundDrag;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;

    public Transform Orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();

        // Prevents player falling over
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Calc the movement direction (so you walk where you look)
        moveDirection = (Orientation.forward * verticalInput) + (Orientation.right * horizontalInput);
        SpeedControl();
    }

    void FixedUpdate() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, groundLayer);
        // Debug.DrawRay(transform.position, Vector3.down, Color.red, 20);

        // Apply drag and force
        rb.drag = isGrounded ? groundDrag : 0;
        rb.AddForce(moveDirection.normalized * (movementSpeed * 10f), ForceMode.Force);
    }

    private void SpeedControl() {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // y = 0, cause no height

        if (flatVelocity.magnitude <= movementSpeed) // exceeds the limit?
            return;

        Vector3 maxMovementSpeed = flatVelocity.normalized * movementSpeed; // Calculate what max velocity should be
        rb.velocity = new Vector3(maxMovementSpeed.x, rb.velocity.y, maxMovementSpeed.z); // Apply the max velocity
    }
}
