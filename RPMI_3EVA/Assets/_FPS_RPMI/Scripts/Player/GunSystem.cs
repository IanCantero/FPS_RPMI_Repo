using UnityEngine;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{

    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam; //Ref si disparamos desde el centro de la camara
    [SerializeField] Transform shootPoint; //Ref si disparamos desde la punta del cañon
    [SerializeField] LayerMask impactLayer; //Layer con la que el raycast interactúa
    RaycastHit hit; //Almacén de informacion de los objetos a los que el raycast puede impactar


    [Header("Weapon Parameters")]
    [SerializeField] int damage = 10; //Daño por bala
    [SerializeField] float range = 100f; //Distancia de disparo
    [SerializeField] float spread = 0f; //Radio de dispersion del arma
    [SerializeField] float shootingCooldown = 0.2f; //Tiempo de disparo
    [SerializeField] float reloadTime = 1.5f; //Tiempo de recarga en segs
    [SerializeField] bool allowButtonHold = false; //Si el disparo se ejecuta por click o por mantener


    [Header("Bullet Management")]
    [SerializeField] int ammoSize = 30; //Cantidad de balas por cargador
    [SerializeField] int bulletsPerTap; //Balas que dispara por click
    int bulletsLeft; //Cantidad de balas dentro del cargador actual

    [Header("FeedbackReferences")]
    [SerializeField] GameObject impactEffect; //Ref al VFX de impacto de balas

    [Header("Dev - Gun State Bools")]
    [SerializeField] bool shooting; //Indica si estamos disparando
    [SerializeField] bool canShoot; //Indica si podemos disparar en x momento del juego
    [SerializeField] bool reloading; //Indica si estamos recargando

    #endregion

    private void Awake()
    {
        bulletsLeft = ammoSize; //Cargador lleno al iniciar partida
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        //MOST IMPORTANT METHOD
        //DISPARO POR RAYCAST

        //Almacenar la direccion de disparo y modificarla si hay spread
        Vector3 direction = fpsCam.transform.forward; //Rayo hacia adelante
        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);

        //DECLARACION DE RAYCAST
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, impactLayer))
        {
            //Aqui puedo codear todos los efectos que quierp en mi interaccion
            Debug.Log(hit.collider.name);
        }
    }

    #region Input Methods

    public void OnShoot(InputAction.CallbackContext context)
    {

    }
    public void OnReload(InputAction.CallbackContext context)
    {

    }
    #endregion
}
