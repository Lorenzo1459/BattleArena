using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 20f;
    public float lifetime = 2f; // Tempo de vida do projétil
    public float pushForce = 10f; // Força de empurrão

    void Start() {
        Destroy(gameObject, lifetime);
    }

    void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        // Aplica uma força de empurrão ao objeto colidido usando CharacterController
        CharacterController targetController = other.GetComponent<CharacterController>();
        if (targetController != null) {
            Vector3 direction = other.transform.position - transform.position;
            direction.y = 0; // Remove a força na direção vertical, se necessário
            direction.Normalize();

            // Aplica um empurrão manualmente movendo o CharacterController
            StartCoroutine(PushCharacter(targetController, direction * pushForce));
        }

        // Destroi o projétil após a colisão
        Destroy(gameObject);
    }

    System.Collections.IEnumerator PushCharacter(CharacterController controller, Vector3 force) {
        float pushDuration = 0.1f; // Duração do empurrão
        float timer = 0f;

        while (timer < pushDuration) {
            controller.Move(force * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
