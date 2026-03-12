using UnityEngine;
using UnityEngine.AI; //lIBRERIA DE NAVMESH
public class EenmyAIBase : MonoBehaviour
{
    #region General Variable
    [Header("AI Configuration")]
    [SerializeField] NavMeshAgent agent; // Ref al cerebro del agente
    [SerializeField] Transform target; //Ref al target a perseguir
    [SerializeField] LayerMask targetLayer; //Define layer del target(detecciones) 
    [SerializeField] LayerMask groundLayer; //Define layer del suelo (Evita ir a zonas sin Suelo)

    [Header("Patroling Stats")]
    [SerializeField] float walkPointRange = 10f; //Radio máximo para determinar puntos a perseguir
    Vector3 walkPoint; //Posicion del punto random a perseguir
    bool walkPointSet; //Hay punto a perseguir generado?

    [Header("Attacking Stats")]
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] GameObject projectile; //Ref a la bala física del enemigo
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootSpeedY; //Fuerza de disparo hacia arriba
    [SerializeField] float shootSpeedz = 10f; //Fuerza de disparo hacia delante(siempre esta)
    bool alreadyAttacked; //Debuguer para no stackear ataques

    [Header("States & Detection")]
    [SerializeField] float sightRange = 8f; //Radio del detector de persecución
    [SerializeField] float attackRange = 2f; //Radio del detector de ataque
    [SerializeField] bool targetInSightRange; //Determina que podemos perseguir al target
    [SerializeField] bool targetInAttackRange; //Determina que podemos atacar al target

    [Header("Stuck Detection")]
    [SerializeField] float stuckCheckTime = 2f; //Tiempo que el agente espera estando quieto antes de darse cuenta de que esta stucked
    [SerializeField] float stuckThreshold = 0.1f; //Margen de detección de stuck
    [SerializeField] float maxStuckDuration = 3f; //Tiempo maximo  de stuck

    float stuckTimer; //Reloj que cuenta el tiempo de stuck
    float lastCheckTime; //Tiempo de chequeo previo de stuck
    Vector3 lastPosition; //Posicion del ultimo walkpoint perseguido



    #endregion
    private void Awake()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastPosition = transform.position;
        lastCheckTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyStateUpdater();
    }

    void EnemyStateUpdater()
    {
        //Metodo que se encarga de gestionar el cambio de estados del enemigo

        //1-Cambio de estados de los bools
        //Detectamos si los targets están en visión
        Collider[] hits = Physics.OverlapSphere(transform.position, sightRange, targetLayer);
        targetInSightRange = hits.Length > 0;
        //Si están en visión, detectamos si estan en rango de ataque
        if (targetInSightRange)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            targetInAttackRange = distance <= attackRange;
        }
        else
        {
            targetInAttackRange = false;
        }

        //2- Cambio de estados segun booleanos
        if (!targetInSightRange && !targetInAttackRange)
        {
            Patroling();
        }
        else if (targetInSightRange && !targetInAttackRange)
        {
            ChaseTarget();
        }
        else if (targetInSightRange && targetInAttackRange)
        {
            AttackTarget();
        }

    }
    
    void Patroling()
    {
        Debug.Log("Ando patrullando miloko");
    }

    void ChaseTarget()
    {
        //Accion que le dice al agente que persiga al target
        agent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        //1- Hacer que el agente se quede quieto (se persigue a si mismo)
        agent.SetDestination(transform.position);

        //2- Aplicar una rotacion suavizada para que el agente mire al target antes de atacar
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, agent.angularSpeed * Time.deltaTime);
        }

        //3- Se ataca, solo si no se está atacando
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * shootSpeedz, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) return; //Si estamos jugando en build no se ejecuta

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
