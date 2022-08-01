using UnityEngine;
using TMPro;

namespace Game
{
    public class ChanceWindow : Window, IShowWindow
    {
        [SerializeField] private ChanceController _chanceController;
        [SerializeField] private TextMeshProUGUI _message;
        private void OnEnable()
        {
            Show();
        }
        public override void Show()
        {
            _chanceController.Generate();
        }
        public void SetText(string textToPlace)
        {
            _message.text = textToPlace;
        }
    }
}
