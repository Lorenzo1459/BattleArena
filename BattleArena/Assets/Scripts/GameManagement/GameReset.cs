using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour {
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        // Verifica se o objeto colidido está na camada "Reset"
        if (hit.gameObject.layer == LayerMask.NameToLayer("Reset")) {
            RestartGame();
        }
    }

    void RestartGame() {
        // Carrega a cena atual novamente
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
