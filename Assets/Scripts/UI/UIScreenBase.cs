using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace SkiFree2.UI
{
    public abstract class UIScreenBase : MonoBehaviour
    {
        protected UIManagerBase _uiManager;

        [Inject]
        public void Construct(UIManagerBase mainSceneUIManager)
        {
            _uiManager = mainSceneUIManager;
        }

        public virtual Task Show()
        {
            gameObject.SetActive(true);
            return Task.CompletedTask;
        }

        public virtual Task Hide()
        {
            gameObject.SetActive(false);
            return Task.CompletedTask;
        }
    }
}
