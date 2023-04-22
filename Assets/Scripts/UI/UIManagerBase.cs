using UnityEngine;

namespace SkiFree2.UI
{
    public abstract class UIManagerBase : MonoBehaviour
    {
        [SerializeField] private UIScreenBase firstScreen;

        private UIScreenBase _activeScreen;

        private void Awake()
        {
            ShowScreen(firstScreen);
        }

        public async void ShowScreen(UIScreenBase screen)
        {
            if (_activeScreen != null)
                await _activeScreen.Hide();

            await screen.Show();
            _activeScreen = screen;
        }
    }
}
