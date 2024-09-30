using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 20f;
    public float lifetime = 2f; // Tempo de vida do proj�til
    public float pushForce = 10f; // For�a de empurr�o



    public ExplosionForce explosionForce;

    void Start() {
        Destroy(gameObject, lifetime);
    }

    void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
        foreach (Collider collider in colliders)
        {
            CharacterController characterController = collider.GetComponent<CharacterController>();
            if (characterController != null)
            {
                Vector3 explosionDirection = collider.transform.position - transform.position;
                float distance = explosionDirection.magnitude;

                characterController.gameObject.GetComponent<BasicRigidBodyPush>().Push(explosionDirection, distance);
                //explosionDirection.Normalize();
            }
        }
        Destroy(gameObject);
    }

    // void OnTriggerEnter(Collider other) {
    //     // Aplica uma for�a de empurr�o ao objeto colidido usando CharacterController
    //     CharacterController targetController = other.GetComponent<CharacterController>();
    //     if (targetController != null) {
    //         Vector3 direction = other.transform.position - transform.position;
    //         direction.y = 0; // Remove a for�a na dire��o vertical, se necess�rio
    //         direction.Normalize();

    //         // Aplica um empurr�o manualmente movendo o CharacterController
    //         StartCoroutine(PushCharacter(targetController, direction * pushForce));
    //     }

    //     // Destroi o proj�til ap�s a colis�o
    //     Destroy(gameObject);
    // }

    // System.Collections.IEnumerator PushCharacter(CharacterController controller, Vector3 force) {
    //     float pushDuration = 0.1f; // Dura��o do empurr�o
    //     float timer = 0f;

    //     while (timer < pushDuration) {
    //         controller.Move(force * Time.deltaTime);
    //         timer += Time.deltaTime;
    //         yield return null;
    //     }
    // }
}
