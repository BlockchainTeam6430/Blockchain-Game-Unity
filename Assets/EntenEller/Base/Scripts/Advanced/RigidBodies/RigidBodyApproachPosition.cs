using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.SimpleActions;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.RigidBodies
{
    public class RigidBodyApproachPosition : TransformApproachPosition
    {
        protected override void GlobalMove()
        {
            GetSelf<Rigidbody>().position = Position.Current;
        }

        protected override void LocalMove()
        {
            throw new System.NotImplementedException();
        }
    }
}
