using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour {
    public float explosionRadius = 5f;
    public float explosionForce = 10f;

    public void Explode() {
        // Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        // foreach (Collider collider in colliders) {
        //     CharacterController characterController = collider.GetComponent<CharacterController>();
        //     if (characterController != null) {
        //         Debug.Log(characterController.gameObject.name );
        //         Vector3 explosionDirection = collider.transform.position - transform.position;
        //         float distance = explosionDirection.magnitude;
        //         explosionDirection.Normalize();

        //         // Calculate force based on distance
        //         float force = Mathf.Lerp(explosionForce, 0, distance / explosionRadius);
        //         // StartCoroutine(ApplyExplosionForce(characterController, explosionDirection * force));
        //     }
        // }
    }

    // IEnumerator ApplyExplosionForce(CharacterController characterController, Vector3 force) {
    //     float duration = 0.5f; // Duration of the push effect
    //     float elapsedTime = 0f;

    //     while (elapsedTime < duration) {
    //         characterController.Move(force * Time.deltaTime);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    // }
}

