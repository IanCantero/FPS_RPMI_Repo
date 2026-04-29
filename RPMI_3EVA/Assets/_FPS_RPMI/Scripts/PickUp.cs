using UnityEngine;

public class PickUp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Objeto recogido: " + gameObject.name);
            Destroy(gameObject); // Destruye el objeto después de recogerlo
        }
    }
}
