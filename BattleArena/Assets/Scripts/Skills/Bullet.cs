using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 20f;
    public float lifetime = 2f; // Tempo de vida do projétil
    public float pushForce = 10f; // Força de empurrão

    public ExplosionForce explosionForce;
    public GameObject collisionEffect; // Reference to the collision effect prefab

    void Start() {
        Destroy(gameObject, lifetime);
    }

    void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3);
        foreach (Collider collider in colliders) {
            CharacterController characterController = collider.GetComponent<CharacterController>();
            if (characterController != null) {
                Vector3 explosionDirection = collider.transform.position - transform.position;
                float distance = explosionDirection.magnitude;

                characterController.gameObject.GetComponent<BasicRigidBodyPush>().Push(explosionDirection);
                //explosionDirection.Normalize();
            }
        }

        // Instantiate the collision effect at the collision point
        Instantiate(collisionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
