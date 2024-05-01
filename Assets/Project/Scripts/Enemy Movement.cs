using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider healthSlider;
    [SerializeField] private Animator animator;

    internal float health;
    internal int damage;
    internal float speed;
    internal float stoppingDistance;

    internal LayerMask m_Player;
    internal Player player;

    internal float detectionRadius;

    private void Start()
    {
        player = Player.Instance;

        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    private void Move()
    {
        if (!gameObject.activeSelf)
            return;

        Vector3 playerPosition = player.gameObject.transform.position;
        playerPosition.y = transform.position.y;

        Vector3 direction = (playerPosition - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceToPlayer <= detectionRadius)
        {
            if (Vector3.Dot(transform.forward, direction) < 0.9f)
            {
                animator.SetBool("Walk Forward", false);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
                return;
            }

            if (distanceToPlayer > stoppingDistance)
            {
                if (Physics.Raycast(transform.position, direction, out RaycastHit hit, stoppingDistance))
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        animator.SetBool("Walk Forward", false);
                        animator.SetBool("Stab Attack", false);
                        return;
                    }

                transform.position += speed * Time.deltaTime * direction;
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);

                animator.SetBool("Walk Forward", true);
            }
            else
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Stab Attack", true);
            }
        }
        else
        {
            animator.SetBool("Walk Forward", false);
            animator.SetBool("Stab Attack", false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        player.IncreaseScore(1);
    }

    private void Update() => Move();

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    player.TakeDamage(damage);
        //    animator.SetBool("Stab Attack", false);
        //}
        //Debug.Log(m_Player.value);
        //if (other.gameObject.layer == m_Player)
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))

        {
            player.TakeDamage(damage);
            animator.SetBool("Stab Attack", false);
        }
    }
}