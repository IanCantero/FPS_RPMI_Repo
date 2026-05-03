using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // ✅ AÑADIDO

namespace DialogueSystem_V
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        [SerializeField] private Dialogue_UI dialogueUI;
        [SerializeField] private float typingSpeed = 0.05f;
        private Queue<Dialogue_Turn> dialogueTurnsQueue;

        public bool IsDialogInProgress { get; private set; } = false;

        private void Awake()
        {
            Instance = this;
            dialogueUI.HideDialogBox();
        }

        public void StartDialogue(Dialogue_Round dialogue)
        {
            if (IsDialogInProgress)
            {
                Debug.LogWarning("Dialogue already in progress");
                return;
            }

            Debug.Log("Iniciando dialogo..."); 
            Debug.Log($"Turns: {dialogue.Dialogue_Turns.Count}"); 

            IsDialogInProgress = true;
            dialogueTurnsQueue = new Queue<Dialogue_Turn>(dialogue.Dialogue_Turns);
            StartCoroutine(DialogueCoroutine());
        }

        private IEnumerator DialogueCoroutine()
        {
            dialogueUI.gameObject.SetActive(true);
            dialogueUI.transform.parent.gameObject.SetActive(true);
            dialogueUI.ShowDialogBox();

            while (dialogueTurnsQueue.Count > 0)
            {
                var currentTurn = dialogueTurnsQueue.Dequeue();

                dialogueUI.SetCharacterInfo(currentTurn.Character);
                dialogueUI.ClearDialogueArea();
                dialogueUI.SetDialogArea(currentTurn.Dialogue_Line);

                yield return new WaitForSeconds(0.3f); 
                yield return new WaitUntil(() => Keyboard.current.eKey.wasPressedThisFrame);
                yield return null;
            }

            dialogueUI.HideDialogBox();
            IsDialogInProgress = false;
        }
    }
    
    
}