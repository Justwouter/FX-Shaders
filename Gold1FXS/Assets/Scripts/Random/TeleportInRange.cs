using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TeleportInRange : MonoBehaviour {
    private bool inRange = false;
    private GameObject source;
    [SerializeField] private GameObject target;
    [SerializeField] private bool automaticTeleport;

    // Update is called once per frame
    void Update() {
        if ((Input.GetKeyDown(KeyCode.E) || automaticTeleport) && inRange) {
            source.transform.position = target.transform.position;
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        inRange = true;
        source = other.transform.parent.gameObject;
    }

    private void OnTriggerExit(Collider other) {
        inRange = false;
        source = null;

    }
}
