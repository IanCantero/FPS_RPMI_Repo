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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
