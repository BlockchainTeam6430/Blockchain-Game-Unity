using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Scenes;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Tags
{
    public static class EETagUtils
    {
        private static bool isInitialized;
        private static Dictionary<string, HashSet<EETagHolder>> _cache;
        private static Dictionary<string, HashSet<EETagHolder>> cache
        {
            get
            {
                if (_cache != null) return _cache;
                
                if (!isInitialized)
                {
                    isInitialized = true;
                    EESceneData.ScenesRawFinishedChangesEvent += () =>
                    {
                        _cache = null;
                    };
                }
                
                _cache = new Dictionary<string, HashSet<EETagHolder>>();
                foreach (var eeGameObject in EEGameObjectUtils.EEGameObjects)
                {
                    if (eeGameObject == null) continue;
                    var tagHolder = eeGameObject.GetSelf<EETagHolder>();
                    if (tagHolder == null) continue;
                    foreach (var eeTag in tagHolder.EETags)
                    {
                        Add(tagHolder, eeTag);
                    }
                }

                return _cache;
            }
        }

        public static void Spawn(EEGameObject eeGameObject)
        {
            if (eeGameObject.GetSelf<EETagHolder>())
            {
                var tagHolder = eeGameObject.GetSelf<EETagHolder>();
                foreach (var eeTag in tagHolder.EETags)
                {
                    Add(tagHolder, eeTag);
                }
            }
            foreach (var tagHolder in eeGameObject.GetAllChild<EETagHolder>())
            {
                foreach (var eeTag in tagHolder.EETags)
                {
                    Add(tagHolder, eeTag);
                }
            }
        }
        
        public static EETagHolder FindEETagInScenes(string tag)
        {
#if DEBUG
            if (!cache.ContainsKey(tag))
            {
                Debug.LogError("Cannot find EETag = " + tag);
                return null;
            } 
#endif
            return cache[tag].First();
        }
        
        public static HashSet<EETagHolder> FindEETagsInScenes(string tag)
        {
#if DEBUG
            if (!cache.ContainsKey(tag))
            {
                Debug.LogError("Cannot find EETag = " + tag);
                return null;
            } 
#endif
            return cache[tag];
        }

        public static void Add(EETagHolder tagHolder, string tag)
        {
            var result = cache.TryGetValue(tag, out var tempList);
            if (!result)
            {
                tempList = new HashSet<EETagHolder>();
                cache.Add(tag, tempList);
            }
            tempList.Add(tagHolder);
        }
        
        public static void Remove(EETagHolder tagHolder, string tag)
        {
            if (cache.ContainsKey(tag)) cache[tag].Remove(tagHolder);
        }
        
        public static EETagHolder FindEETagInChildren(EEBehaviour behaviour, string tag)
        {
            return behaviour.GetAllChild<EETagHolder>().FirstOrDefault(a => a.IsHavingTag(tag));
        }
        
        public static List<EETagHolder> FindEETagsInChildren(EEBehaviour behaviour, string tag)
        {
            return behaviour.GetAllChild<EETagHolder>().Where(a => a.IsHavingTag(tag)).ToList();
        }
        
        public static EETagHolder FindEETagInParent(EEBehaviour behaviour, string tag)
        {
            return behaviour.GetAllParent<EETagHolder>().FirstOrDefault(a => a.IsHavingTag(tag));
        }
        
        public static List<EETagHolder> FindEETagsInParent(EEBehaviour behaviour, string tag)
        {
            return behaviour.GetAllParent<EETagHolder>().Where(a => a.IsHavingTag(tag)).ToList();
        }
        
        public static EETagHolder FindEETagInNeighbour(EEBehaviour behaviour, string tag)
        {
            return behaviour.GetAllNeighbor<EETagHolder>().FirstOrDefault(a => a.IsHavingTag(tag));
        }
        
        public static List<EETagHolder> FindEETagsInNeighbour(EEBehaviour behaviour, string tag)
        {
            return behaviour.GetAllNeighbor<EETagHolder>().Where(a => a.IsHavingTag(tag)).ToList();
        }
    }
}