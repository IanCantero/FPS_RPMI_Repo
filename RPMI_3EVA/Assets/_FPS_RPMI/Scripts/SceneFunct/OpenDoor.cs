using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Open");
            GetComponent<Collider>().enabled = false;
        }
    }
}