using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        if (playerPrefab == null || spawnPoints.Length < 2) return;

        // 1. Instancia os dois jogadores a partir do mesmo Prefab
        GameObject p1 = Instantiate(playerPrefab, spawnPoints[0].position, spawnPoints[0].rotation);
        GameObject p2 = Instantiate(playerPrefab, spawnPoints[1].position, spawnPoints[1].rotation);

        PlayerInput inputP1 = p1.GetComponent<PlayerInput>();
        PlayerInput inputP2 = p2.GetComponent<PlayerInput>();

        // 2. Configura a divisão do ecrã (Split Screen)
        Camera camP1 = p1.GetComponentInChildren<Camera>();
        if (camP1 != null) camP1.rect = new Rect(0f, 0f, 0.5f, 1f);

        Camera camP2 = p2.GetComponentInChildren<Camera>();
        if (camP2 != null) camP2.rect = new Rect(0.5f, 0f, 0.5f, 1f);

        p1.GetComponent<PlayerController>()?.SetupPlayer(0, camP1);
        p2.GetComponent<PlayerController>()?.SetupPlayer(1, camP2);

        // 3. Configura o teclado partilhado
        if (Keyboard.current != null)
        {
            // Atribui o esquema P1 ao primeiro jogador
            inputP1.SwitchCurrentControlScheme("KeyboardP1", Keyboard.current);

            // Emparelha o mesmo teclado ao Player 2 e ativa o esquema P2
            InputUser.PerformPairingWithDevice(Keyboard.current, inputP2.user);
            inputP2.SwitchCurrentControlScheme("KeyboardP2", Keyboard.current);
        }
    }
}