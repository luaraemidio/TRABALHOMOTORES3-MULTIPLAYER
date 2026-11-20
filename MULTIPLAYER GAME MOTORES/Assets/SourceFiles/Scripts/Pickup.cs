using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Effects")]
    public GameObject particleEffectPrefab;

    [Header("Motion Settings")]
    public float rotationSpeed = 100f;
    public float bobbingAmount = 0.1f;
    public float bobbingSpeed = 1f;

    private Vector3 startPosition;
    private float timer;
    private bool coletada;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        timer += Time.deltaTime * bobbingSpeed;
        float newY = startPosition.y + Mathf.Sin(timer) * bobbingAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (coletada)
            return;

        // Procura o robô mesmo quando o collider está em um objeto filho.
        Transform jogador = other.transform;

        while (jogador != null &&
               jogador.name != "PlayerRobot" &&
               jogador.name != "PlayerRobot (1)")
        {
            jogador = jogador.parent;
        }

        if (jogador == null)
            return;

        if (!other.CompareTag("Player") && !jogador.CompareTag("Player"))
            return;

        coletada = true;

        PlayerCollectibleCounts counts = jogador.GetComponent<PlayerCollectibleCounts>();

        if (counts != null)
        {
            counts.AddStar();
        }
        else
        {
            Debug.LogWarning("Adicione PlayerCollectibleCounts ao robô.", jogador);
        }
        WinnerManager.Instance.StarCollected();

        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}