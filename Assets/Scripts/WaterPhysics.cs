using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WaterPhysics : MonoBehaviour {

    RigidbodyFirstPersonController player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player = collision.collider.GetComponent<RigidbodyFirstPersonController>();
            player.movementSettings.ForwardSpeed = 2;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player = collision.collider.GetComponent<RigidbodyFirstPersonController>();
            player.movementSettings.ForwardSpeed = 8;
        }
    }
}
