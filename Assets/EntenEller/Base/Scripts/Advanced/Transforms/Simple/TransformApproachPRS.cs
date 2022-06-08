using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class TransformApproachPRS : EEBehaviour
    {
        public void SetTarget(Transform ts)
        {
            GetSelf<TransformApproachPosition>().SetTarget(ts);
            GetSelf<TransformApproachRotation>().SetTarget(ts);
            GetSelf<TransformApproachScale>().SetTarget(ts);
        }

        public void SetTarget(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GetSelf<TransformApproachPosition>().Position.SetTarget(position);
            GetSelf<TransformApproachRotation>().Rotation.SetTarget(rotation);
            GetSelf<TransformApproachScale>().Scale.SetTarget(scale);
        }

        public void SetApproachStyles(Approach positionApproach, Approach rotationApproach, Approach scaleApproach)
        {
            GetSelf<TransformApproachPosition>().Position.Approach = positionApproach;
            GetSelf<TransformApproachRotation>().Rotation.Approach = rotationApproach;
            GetSelf<TransformApproachScale>().Scale.Approach = scaleApproach;
        }
    }
}
