using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PasstroughBallMover : MonoBehaviour {
    [SerializeField] private float circleDiameter = 5f;
    [SerializeField] private float movementSpeed = 2f;

    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
    }

    void Update() {

        float angle = Time.time * movementSpeed;
        float y = Mathf.Cos(angle) * (circleDiameter / 2);
        float x = Mathf.Sin(angle) * (circleDiameter / 2);
        float z = Mathf.Tan(angle) * (circleDiameter / 2);

        transform.position = initialPosition + new Vector3(x, y, z);
    }

}
