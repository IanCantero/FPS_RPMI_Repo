using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem_V
{
    public class Dialogue_UI : MonoBehaviour
    {
        [SerializeField] private RectTransform dialogBox;
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI dialogArea;

        public void ShowDialogBox()
        {
            dialogBox.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        }

        public void HideDialogBox()
        {
            dialogBox.gameObject.SetActive(false);
        }

        public void SetCharacterInfo(Dialogue_Character character)
        {
            if (character == null) return;
            if (characterName != null)
                characterName.text = character.Name;
        }

        public void SetDialogArea(string text)
        {
            dialogArea.text = text;
        }

        public void ClearDialogueArea()
        {
            dialogArea.text = string.Empty;
        }
    }
}