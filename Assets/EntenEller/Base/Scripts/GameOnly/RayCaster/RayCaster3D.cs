using System.Linq;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.GameOnly.RayCaster
{
    public class RayCaster3D : Raycaster
    {
        [SerializeField] private float maxLength = 10;

        protected override void FindAllHits()
        {
            var hits = Physics.RaycastAll(GetSelf<Transform>().position, GetSelf<Transform>().forward, maxLength);
            foreach (var hit in hits)
            {
                var isIgnoringCollision = Physics.GetIgnoreLayerCollision(gameObject.layer, hit.transform.gameObject.layer);
                if (isIgnoringCollision) continue;
                CurrentCollisions.Add(hit);
            }
            CurrentCollisions = CurrentCollisions.OrderBy(a => a.distance).ToList();
        }
    }
}