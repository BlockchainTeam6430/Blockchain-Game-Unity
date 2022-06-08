using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Project.Scripts.Network;
using UnityEngine;

namespace Project.Scripts.Character
{
    public class CharacterMover : EEBehaviourLoop
    {
        [SerializeField] private float speed;
        public Vector3 pos0 = new Vector3(-8.87f, -5.5f, 0);
        public Vector3 pos1 = new Vector3(8.87f, -1f, 0);
        private bool IsMobileDevice = false;
        private FixedJoystick joystick;
        
#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    static extern bool IsMobile();
#endif
        
        protected override void EEAwake()
        {
            base.EEAwake();
            
            joystick = FindObjectOfType<FixedJoystick>(true);
            if (joystick == null) return;
            
#if !UNITY_EDITOR && UNITY_WEBGL
            IsMobileDevice = IsMobile();
#endif
            
            if (IsMobileDevice) joystick.gameObject.SetActive(true);
        }

        public void Move(Vector3 delta)
        {
            if (!enabled) return;
            
            var pos = GetSelf<Transform>().position;

            pos += delta * (speed * Time.deltaTime);
            
            if (pos.x < pos0.x) pos.x = pos0.x;
            if (pos.x > pos1.x) pos.x = pos1.x;
            if (pos.y < pos0.y) pos.y = pos0.y;
            if (pos.y > pos1.y) pos.y = pos1.y;

            GetSelf<Transform>().position = pos;
        }

        protected override void Loop()
        {
            base.Loop();
            if (!GetSelf<PlayerOwner>().IsMine) return;

            var v2 = Vector2.zero;
            
            if (IsMobileDevice && joystick)
            {
                v2 = new Vector2(joystick.Horizontal, joystick.Vertical);
            }
            if (v2.IsAlmostEqual(Vector2.zero)) v2 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            Move(v2);
        }
    }
}
