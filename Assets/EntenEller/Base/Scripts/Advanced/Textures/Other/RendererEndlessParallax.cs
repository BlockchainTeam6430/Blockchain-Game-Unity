using System.Collections;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Textures.Other
{
    public class RendererEndlessParallax : EEBehaviour
    {
        [SerializeField] private float speed = 1;

        protected override void EEEnable()
        {
            StartCoroutine(Scroll());
            
            IEnumerator Scroll()
            {
                var offset = Vector2.zero;
                while (true)
                {
                    yield return null;
                    offset.x = Time.time * speed;
                    GetSelf<Renderer>().material.mainTextureOffset = offset;
                }
            }
        }
    }
}