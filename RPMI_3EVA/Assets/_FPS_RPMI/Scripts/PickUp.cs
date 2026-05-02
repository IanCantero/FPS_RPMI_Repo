using UnityEngine;

public class PickUp : MonoBehaviour
{

    void Update()
    {
        gameObject.transform.Rotate(0, 50 * Time.deltaTime, 0); // Rota el objeto para hacerlo más visible
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Objeto recogido: " + gameObject.name);
            Destroy(gameObject); // Destruye el objeto después de recogerlo
        }
    }
}
