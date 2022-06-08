using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables
{
    public class EEVariableFinder : EEBehaviourLoop
    {
        [SerializeField] private EEGameObjectFinder target;
        public List<VariableInfo> VariablesInfo = new List<VariableInfo>();

        protected override void EEAwake()
        {
            base.EEAwake();
            Prepare();
        }

        private void Prepare()
        {
            foreach (var variable in VariablesInfo)
            {
                foreach (var obj in target.GetAll(this))
                {
                    foreach (var comp in obj.GetComponents(typeof(MonoBehaviour)))
                    {
                        var type = comp.GetType();
                        while (type != null)
                        {
                            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                            MemberInfo memberInfo = type.GetField(variable.VariableName, bindingFlags);
                            if (memberInfo == null) memberInfo = type.GetProperty(variable.VariableName, bindingFlags);
                            if (memberInfo == null)
                            {
                                type = type.BaseType;
                                continue;
                            }
                            var data = new VariableData
                            {
                                Component = comp,
                                MemberInfo = memberInfo,
                            };
                            data.Value = data.MemberInfo.GetMemberValue(comp);
                            variable.Variables.Add(data);
                            break;
                        }
                    }
                }
            }
            #if DEBUG
            if (!VariablesInfo.First().Variables.Any()) EEException.Call(this, "No variables found!");
            #endif
        }
        
        protected override void Loop()
        {
            base.Loop();
            var wasChanged = false;
            foreach (var variableInfo in VariablesInfo)
            {
                foreach (var variable in variableInfo.Variables)
                {
                    variable.Value = variable.MemberInfo.GetMemberValue(variable.Component);
                    if (variable.Value != variable.ValueOld) wasChanged = true;
                }
            }
            if (wasChanged) Change();
        }
        
        protected virtual void Change() {}
        
        [Serializable]
        public class VariableInfo 
        {
            public string VariableName;
            [ReadOnly] public List<VariableData> Variables = new List<VariableData>();
        }
        
        [Serializable]
        public class VariableData
        {
            [ReadOnly] public Component Component;
            public MemberInfo MemberInfo;
            public object Value;
            public object ValueOld;
        }
    }
}
