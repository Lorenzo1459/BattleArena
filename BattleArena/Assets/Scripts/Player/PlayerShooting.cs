using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    public GameObject projectilePrefab; // O prefab do projétil
    public Transform firePoint; // Ponto de onde o projétil será disparado
    public float projectileSpeed = 10f; // Velocidade do projétil

    void Update() {
        // Verifica se o jogador pressiona a tecla de disparo (ex: espaço)
        if (Input.GetButtonDown("Fire1")) // Fire1 é geralmente o botão esquerdo do mouse ou Ctrl
        {
            Shoot();
        }
    }

    void Shoot() {
        // Instantiate the bullet at the fire point
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Calculate the direction based on the mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set distance from camera
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = (worldPosition - firePoint.position).normalized;

        // Set the bullet's forward direction
        bullet.transform.forward = -direction;

        // If using your Bullet script to move it
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        // If you need to pass any direction or other values to the bullet, do it here
    }
}
