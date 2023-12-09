using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;

public class UIManger : MonoBehaviour {
    [SerializeField] private Light mainLight;
    
    [SerializeField, Range(0,1)] private float speed;
    private bool pointerDown;
    private float distance;
    private float timer;

    private void Update() {
        // if(Input.GetKeyDown(KeyCode.D)){
        //     Rotate(10);
        // }
        // if(Input.GetKeyDown(KeyCode.A)){
        //     Rotate(-10);
        // }
        if (pointerDown && Time.time - timer > speed) {
            timer = Time.time;
            mainLight.transform.Rotate(new(mainLight.transform.rotation.x, distance, mainLight.transform.rotation.z));
        }


    }
    public void Rotate(float distance) {
        this.distance = distance;
        pointerDown = true;
    }

    public void PointerUp() {
        distance = 0.0f;
        pointerDown = false;
    }
}
