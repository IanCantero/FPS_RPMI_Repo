using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField] private AudioClip pickupSound;

    void Update()
    {
        gameObject.transform.Rotate(0, 50 * Time.deltaTime, 0); // Rota el objeto para hacerlo más visible
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          if (pickupSound != null) 
            {

                AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);

            }
            
            Debug.Log("Objeto recogido: " + gameObject.name);
            Destroy(gameObject); // Destruye el objeto después de recogerlo
        }
    }
}
