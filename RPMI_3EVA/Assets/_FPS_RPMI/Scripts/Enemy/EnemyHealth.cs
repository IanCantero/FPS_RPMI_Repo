using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health system configuratiom")]
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    [Header("Feedback Comfiguration")]
    [SerializeField] Material damagedMat; //Mat de feedback de dañado
    [SerializeField] SkinnedMeshRenderer enemyRend; //Ref al renderer del enemy
    [SerializeField] GameObject deathVFX; //Ref al sistema de partículas de muerte
    Material baseMat; //Ref al modelo base del enemigo



    private void Awake()
    {
        enemyRend = GetComponentInChildren<SkinnedMeshRenderer>(); //Obtener el renderer del enemigo
        health = maxHealth;
        baseMat = enemyRend.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            health = 0; 
            deathVFX.SetActive(true); //Se enciende el VFX
            deathVFX.transform.position =  transform.position; //Se hace tp al player
            gameObject.SetActive(false); //Se apaga el enmigo y "muere"
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; //Quito vida
        enemyRend.material = damagedMat; //Cambiar mat base por dañado
        Invoke(nameof(ResetEnemyMat), 0.1f); //Llamar al reseteo del mat con .1 secs de espera
    }

    void ResetEnemyMat()
    {
        enemyRend.material = baseMat; //Cambia el mat al base
    }
}
