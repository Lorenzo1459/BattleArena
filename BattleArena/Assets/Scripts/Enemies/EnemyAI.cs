using UnityEngine;

public class EnemyAI : MonoBehaviour {
    //Atirar
    public float detectionRange = 10f; // Alcance de detecção
    public float shootingRange = 8f; // Alcance de tiro
    public GameObject bulletPrefab; // Prefab do projétil
    public Transform firePoint; // Ponto de onde os projéteis são disparados
    public float fireRate = 1f; // Taxa de disparo (balas por segundo)

    private float fireCooldown;
    private Transform target; // Alvo atual

    //Movimentar
    public float walkSpeed = 3f; // Velocidade de caminhada
    public float jumpForce = 5f; // Força do pulo
    public float gravity = -9.81f; // Força da gravidade
    public Collider walkableArea; // Box Collider representando a área de movimento
    
    private CharacterController controller;
    private Vector3 direction;
    private Vector3 velocity;
    private Vector3 areaCenter;
    private Vector3 areaSize;
    private bool isGrounded;
    
    void Start() {
        controller = GetComponent<CharacterController>();
        areaCenter = walkableArea.bounds.center;
        areaSize = walkableArea.bounds.size;
        SetRandomDirection();
    }
    
    void Update() {
        // Verifica se o inimigo está no chão
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f; // Mantém o inimigo no chão
        }

        // Move o inimigo na direção atual
        Vector3 newPosition = transform.position + direction * walkSpeed * Time.deltaTime;
        if (IsWithinWalkableArea(newPosition)) {
            controller.Move(direction * walkSpeed * Time.deltaTime);
        } else {
            SetRandomDirection();
        }

        // Aplica gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Muda a direção quando atinge os limites da área
        if (!IsWithinWalkableArea(transform.position)) {
            SetRandomDirection();
        }

        // Faz o inimigo pular aleatoriamente
        if (isGrounded && Random.Range(0, 100) < 2) // Ajuste a chance de pular
        {
            Jump();
        }


        // Encontra o alvo mais próximo (jogador ou outro inimigo)
        target = FindClosestTarget();

        if (target != null) {
            float distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (distanceToTarget <= detectionRange) {
                // O inimigo detecta o alvo e pode se mover ou rotacionar em direção a ele
                transform.LookAt(target);

                if (distanceToTarget <= shootingRange && fireCooldown <= 0) {
                    Shoot();
                    fireCooldown = 1f / fireRate; // Reseta o cooldown do tiro
                }
            }
        }

        if (fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
        }
    }

    bool IsWithinWalkableArea(Vector3 position) {
        Vector3 localPosition = position - walkableArea.bounds.center;
        return Mathf.Abs(localPosition.x) <= walkableArea.bounds.extents.x &&
               Mathf.Abs(localPosition.y) <= walkableArea.bounds.extents.y &&
               Mathf.Abs(localPosition.z) <= walkableArea.bounds.extents.z;
    }

    void SetRandomDirection() {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        direction = new Vector3(randomX, 0, randomZ).normalized;
    }

    void Jump() {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    Transform FindClosestTarget() {
        Transform closestTarget = null;
        float closestDistance = detectionRange;

        // Procura pelo jogador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer < closestDistance) {
                closestDistance = distanceToPlayer;
                closestTarget = player.transform;
            }
        }

        // Procura pelos inimigos
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            if (enemy.transform != this.transform) // Ignora a si mesmo
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                if (distanceToEnemy < closestDistance) {
                    closestDistance = distanceToEnemy;
                    closestTarget = enemy.transform;
                }
            }
        }

        return closestTarget;
    }

    void Shoot() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}