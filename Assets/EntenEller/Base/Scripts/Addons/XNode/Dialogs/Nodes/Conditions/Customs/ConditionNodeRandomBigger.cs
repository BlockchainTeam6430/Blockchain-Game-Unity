using UnityEngine;

// ReSharper disable once CheckNamespace
namespace NodeSystem.Dialog.ConditionNodes
{
    public class ConditionNodeRandomBigger : ConditionNode
    {
        [SerializeField] private int min = 1;
        [SerializeField] private int max = 100;
        [SerializeField] private int target = 50;
        
        public override bool GetResult()
        {
            return Random.Range(min, max + 1) < target;
        }
    }
}
