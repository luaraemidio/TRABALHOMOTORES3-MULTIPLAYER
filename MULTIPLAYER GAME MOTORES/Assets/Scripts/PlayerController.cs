using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Identificação")]
    [SerializeField] private int playerIndex = 0; // 0 = Player 1, 1 = Player 2

    [Header("Atributos de Movimento")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float speedBoostPerCoin = 1.5f; // Aumento de velocidade por moeda
    [SerializeField] private float rotationSpeed = 10f;

    private float currentSpeed;
    private int coinsCount = 0;
    private Vector2 moveInput;
    private CharacterController characterController;
    private Transform mainCameraTransform;

    public int PlayerIndex => playerIndex;
    public int CoinsCount => coinsCount;

    // Evento Observer (playerIndex, quantidadeMoedas)
    public static event Action<int, int> OnCoinCollected;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        currentSpeed = baseSpeed;
    }

    public void SetupPlayer(int index, Camera playerCamera)
    {
        playerIndex = index;
        if (playerCamera != null)
        {
            mainCameraTransform = playerCamera.transform;
        }
    }

    // Evento chamado pelo Player Input (Message/Event OnMove)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (mainCameraTransform != null)
            {
                targetAngle += mainCameraTransform.eulerAngles.y;
            }

            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Vector3 moveDirection = targetRotation * Vector3.forward;
            characterController.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
        }
    }

    // Chamado ao coletar Moeda
    public void CollectCoin()
    {
        coinsCount++;
        currentSpeed += speedBoostPerCoin; // Deixa o jogador mais rápido

        // Dispara o evento para a GUI
        OnCoinCollected?.Invoke(playerIndex, coinsCount);
    }
}