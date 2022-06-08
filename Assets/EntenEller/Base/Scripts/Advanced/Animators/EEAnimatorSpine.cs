using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
#if SPINE
using Spine.Unity;
using Sirenix.OdinInspector;
#endif

namespace Plugins.EntenEller.Base.Scripts.Advanced.Animators
{
    public class EEAnimatorSpine : EEBehaviour
    {
#if SPINE
        public string CurrentAnimation;

        public void Change(string animationName)
        {
            Change(animationName, false);
        }
        
        public void ChangeToLoop(string animationName)
        {
            Change(animationName, true);
        }

        private void Change(string animationName, bool isLoop)
        {
            CurrentAnimation = animationName;
            GetSelf<SkeletonGraphic>().AnimationState.SetAnimation(0, animationName, isLoop);
        }

        public void Pause()
        {
            GetSelf<SkeletonGraphic>().timeScale = 0;
        }

        public void Play()
        {
            GetSelf<SkeletonGraphic>().timeScale = 1;
        }

        public void Restart()
        {
        }
        
#endif
    }
}
