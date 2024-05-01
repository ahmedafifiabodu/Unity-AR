using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private int health = 100;
    [SerializeField] private int score = 0;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject touchEffect;

    private PlayerInput input;
    private UI _UI;

    private int currentHealth;
    private int currentScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        input = new();
        input.Enable();
    }

    private void Start()
    {
        _UI = UI.Instance;

        currentHealth = health;
        currentScore = score;

        _UI.UpdateHealth(currentHealth);
        _UI.UpdateScore(currentScore);

        input.Player.Touch.performed += ctx => Attack();
    }

    private void Attack()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = _camera.ScreenPointToRay(touchPosition);

            int layerMask = ~LayerMask.GetMask("IgnoreRaycast");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                float distanceFromCamera = 10f;
                Vector3 worldPosition = _camera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, distanceFromCamera));

                GameObject effectInstance = Instantiate(touchEffect, worldPosition, Quaternion.identity);

                StartCoroutine(DestroyEffectAfterTime(effectInstance, 1f));

                if (hit.collider.TryGetComponent<EnemyMovement>(out var enemy))
                {
                    enemy.TakeDamage(10);
                }
            }
        }
    }

    private IEnumerator DestroyEffectAfterTime(GameObject effectInstance, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(effectInstance);
    }

    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;
        _UI.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die() => UI.Instance.GameOver();

    internal void IncreaseScore(int increment)
    {
        currentScore += increment;
        _UI.UpdateScore(currentScore);
    }

    internal void UpdatePlayerData()
    {
        currentHealth = health;
        currentScore = score;

        _UI.UpdateHealth(currentHealth);
        _UI.UpdateScore(currentScore);
    }
}