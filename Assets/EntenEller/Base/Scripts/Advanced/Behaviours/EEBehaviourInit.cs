using System.ComponentModel;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Scenes;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Behaviours
{
    public abstract class EEBehaviourInit : EEBehaviourValues
    {
        private static bool isQuit;
        private bool needToAwake, needToEnable;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void Awake()
        {
            if (!EESceneData.IsLoading) EEAwake();
            else needToAwake = true;
        }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void OnEnable()
        {
            if (!EESceneData.IsLoading) EEEnable();
            else needToEnable = true;
        }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual void OnDisable()
        {
            if (!isQuit) EEDisable();
        }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void OnDestroy()
        {
            if (isQuit) return;
            var obj = GetComponent<EEGameObject>();
            if (obj.IsNotNull())
            {
                EEGameObjectUtils.Remove(obj);
            }
            EEDestroy();
        }
        
        private void OnApplicationQuit()
        {
            isQuit = true;
        }

        public void SceneAwake()
        {
            if (!needToAwake) return;
            needToAwake = false;
            EEAwake();
        }

        public void SceneEnable()
        {
            if (!needToEnable) return;
            needToEnable = false;
            EEEnable();
        }
        
        protected virtual void EEAwake () {}
        protected virtual void EEDestroy () {}
        protected virtual void EEEnable () {}
        protected virtual void EEDisable () {}
    }
}
