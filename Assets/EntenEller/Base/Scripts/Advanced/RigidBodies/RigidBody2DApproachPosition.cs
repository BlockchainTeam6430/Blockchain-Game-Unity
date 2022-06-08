using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.SimpleActions;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.RigidBodies
{
    public class RigidBody2DApproachPosition : TransformApproachPosition
    {
        protected override void GlobalMove()
        {
            GetSelf<Rigidbody2D>().position = Position.Current;
        }

        protected override void LocalMove()
        {
            throw new System.NotImplementedException();
        }
    }
}
