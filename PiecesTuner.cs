using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesTuner : MonoBehaviour
{
    private Player _player;

    private void Awake() {
        _player = FindObjectOfType<Player>();
    }

    private void Update() {
        FollowPlayer();
    }

    private void FollowPlayer() {
        transform.position = _player.transform.position;
        transform.rotation = _player.transform.rotation;
    }

    public void Explode() {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders) {
            collider.enabled = true;
        }

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in rigidbodies) {
            body.isKinematic = false;
            body.AddForce(Vector3.up * 10f);
        }
    }
}
