using UnityEngine;

namespace DialogueSystem_V
{
    public class Dialogue_Trigger : MonoBehaviour
    {
        [SerializeField] private Dialogue_Round dialogue;

        [ContextMenu("Trigger Dialogue")]
        public void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }

        private bool playerInRange = false;

        private void Update()
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
                playerInRange = true;
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.CompareTag("Player"))
                playerInRange = false;
        }
    }
}
