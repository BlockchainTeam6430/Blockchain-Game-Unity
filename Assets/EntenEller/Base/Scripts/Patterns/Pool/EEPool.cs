using System;
using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Advanced.Scenes;
using Plugins.EntenEller.Base.Scripts.Advanced.Spawns;

namespace Plugins.EntenEller.Base.Scripts.Patterns.Pool
{
    public static class EEPool
    {
        private static Dictionary<EEPoolObject, EEPoolObject[]> _pool;
        private static Dictionary<EEPoolObject, EEPoolObject[]> pool 
        {
            get
            {
                if (_pool != null) return _pool;
                _pool = new Dictionary<EEPoolObject, EEPoolObject[]>();
                EESceneData.ScenesRawFinishedChangesEvent += () =>
                {
                    _pool.Clear();
                };
                return _pool;
            }
        }

        public static EEPoolObject GetAvailablePoolObject(EEPoolObject origin)
        {
            CheckOrigin(origin);
            var array = pool[origin];
            for (var j = 0; j < array.Length; j++)
            {
                var poolObject = array[j];
                if (poolObject.IsNull())
                {
                    return Create(origin, array, j);
                }
                if (poolObject.IsActive) continue;
                return poolObject;
            }
            
            throw new Exception("Cant create or find free pool object! " + origin.Origin);
        }

        private static void CheckOrigin(EEPoolObject origin)
        {
            if (!pool.ContainsKey(origin)) pool.Add(origin, new EEPoolObject[4096]);
        }
        
        private static EEPoolObject Create(EEPoolObject origin, EEPoolObject[] array, int j)
        {
            var obj = SpawnUtils.Spawn(origin);
            array[j] = obj;
            obj.Origin = origin;
            obj.J = j;
            return obj;
        }

        public static int GetJByPoolObject (EEPoolObject poolObject)
        {
            var array = pool[poolObject.Origin];
            return Array.IndexOf(array, poolObject);
        }

        public static EEPoolObject GetPoolObjectByJ(EEPoolObject origin, int j)
        {
            CheckOrigin(origin);
            var array = pool[origin];
            var obj = array[j];
            if (obj.IsNull())
            {
                obj = Create(origin, array, j);
            }
            return obj;
        }
    }
}