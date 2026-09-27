using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users; // Necessário para emparelhar o teclado nos dois jogadores

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
        if (playerPrefab == null || spawnPoints.Length < 2)
        {
            Debug.LogError("PlayerSpawner: Certifique-se de atribuir o Player Prefab e pelo menos 2 Spawn Points no Inspector!");
            return;
        }

        // 1. Instancia Player 1 a partir do Prefab
        GameObject p1 = Instantiate(playerPrefab, spawnPoints[0].position, spawnPoints[0].rotation);
        PlayerInput inputP1 = p1.GetComponent<PlayerInput>();
        PlayerController controllerP1 = p1.GetComponent<PlayerController>();

        // 2. Instancia Player 2 a partir do MESMO Prefab
        GameObject p2 = Instantiate(playerPrefab, spawnPoints[1].position, spawnPoints[1].rotation);
        PlayerInput inputP2 = p2.GetComponent<PlayerInput>();
        PlayerController controllerP2 = p2.GetComponent<PlayerController>();

        // 3. Configura a Câmera do Player 1 (Metade Esquerda da tela)
        Camera camP1 = p1.GetComponentInChildren<Camera>();
        if (camP1 != null)
        {
            camP1.rect = new Rect(0f, 0f, 0.5f, 1f);
            p1.GetComponentInChildren<CameraFollow>()?.SetTarget(p1.transform);
        }

        // 4. Configura a Câmera do Player 2 (Metade Direita da tela)
        Camera camP2 = p2.GetComponentInChildren<Camera>();
        if (camP2 != null)
        {
            camP2.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            p2.GetComponentInChildren<CameraFollow>()?.SetTarget(p2.transform);
        }

        // Passa o índice do jogador para os scripts
        controllerP1.SetupPlayer(0, camP1);
        controllerP2.SetupPlayer(1, camP2);

        // 5. Configuração dos Controles (Compartilhamento de Teclado ou Gamepads)
        if (Gamepad.all.Count >= 2)
        {
            // Dois gamepads conectados
            inputP1.SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
            inputP2.SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
        }
        else if (Keyboard.current != null)
        {
            // Força a desvinculação para permitir o compartilhamento
            inputP1.user.UnpairDevices();
            inputP2.user.UnpairDevices();

            // Emparelha o mesmo Teclado para AMBOS os jogadores
            InputUser.PerformPairingWithDevice(Keyboard.current, inputP1.user);
            InputUser.PerformPairingWithDevice(Keyboard.current, inputP2.user);

            // Aplica os Control Schemes correspondentes
            inputP1.SwitchCurrentControlScheme("KeyboardP1", Keyboard.current);
            inputP2.SwitchCurrentControlScheme("KeyboardP2", Keyboard.current);
        }
    }
}