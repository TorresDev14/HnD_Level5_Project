using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{
    public float health = 50f;
    public AudioSource scream;

    public void takeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            scream.Play();
            Defeated();
        }
    }

    void Defeated()
    {

        Destroy(gameObject);
    }
}
