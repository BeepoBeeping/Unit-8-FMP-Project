using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private HealthBarUI healthBar;
    public PlayerScript playerScript;

    public void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    public int maxHealth = 100;
   public int currentHealth {  get; private set; }

   public Stat damage;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage. ");

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Danger" || col.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
        }

        if (col.gameObject.tag == "Floor")
        {
            playerScript.grounded = true;
        }
        else
        {
            playerScript.grounded = false;
        }
    }

    public virtual void Die()
    {
        SceneManager.LoadScene("Scene1");
        Debug.Log(transform.name + " died.");
    }
}
