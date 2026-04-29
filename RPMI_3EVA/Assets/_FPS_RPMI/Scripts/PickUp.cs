using UnityEngine;

public class PickUp : MonoBehaviour
{
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 50); // Rota el objeto alrededor del eje Y
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
