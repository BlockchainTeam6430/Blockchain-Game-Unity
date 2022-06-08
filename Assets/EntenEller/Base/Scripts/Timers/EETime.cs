using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.Scenes;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Timers
{
    public static class EETime
    {
        private static readonly List<EETimerData> timers = new List<EETimerData>();
        private static bool init = false;
        private const int delayForAsyncChecking = 10;
        
        [Serializable]
        public class EETimerData
        {
            public string Name = string.Empty;
            public int ComponentID = int.MinValue;
            
            public float CurrentTime = 0f;
            public float FinalTime = 0f;
            
            public bool IsIgnoreSceneSwitching = false;
            public EETimerState TimerState = EETimerState.Update;
            public DateTime LastDateTime = DateTime.Now;
            
            public Action Action;
            public Action OnUpdateEvent;
            public Action OnPauseEvent;
            public Action OnCompleteEvent;
            public Action OnCancelEvent;
            
            public enum EETimerState
            {
                Update,
                Pause,
                Complete,
                Cancel
            }
        }
        
        public static void StartTimer(EETimerData timerData)
        {
            if (timerData.CurrentTime.IsAlmostEqual(timerData.FinalTime))
            {
                timerData.Action.Call();
                return;
            }
            
            if (!init)
            {
                init = true;
                EEDebug.Log("EETimer initialized!");
                EESceneData.ScenesRawFinishedChangesEvent += () =>
                {
                    EEDebug.Log("Cleaning timers!");
                    timers.ForEach(a =>
                    {
                        if (!a.IsIgnoreSceneSwitching) Timing.KillCoroutines(a.ComponentID.ToString());
                    });
                    timers.Clear();
                };
                Timing.RunCoroutine(Loop());
            }

            timerData.Name = timerData.Action.ToUniqueString();

            timers.Add(timerData);
            
            
            RunTimer();

            void RunTimer()
            {
#if UNITY_WEBGL
                Timing.RunCoroutine(Sync(timerData));
#else
                Async(timerData);
#endif
            }
                       
#pragma warning disable 8321
            static IEnumerator<float> Sync(EETimerData timerData)
#pragma warning restore 8321
            {
                while (true)
                {
                    yield return Timing.WaitForOneFrame;
                    if (TimerLoop(timerData)) break;
                }
            }
            
#pragma warning disable 8321
            static async void Async(EETimerData timerData)
#pragma warning restore 8321
            {
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(delayForAsyncChecking);
                        if (TimerLoop(timerData)) return;
                    }
                });
            }

            static bool TimerLoop(EETimerData timerData)
            {
                switch (timerData.TimerState)
                {
                    case EETimerData.EETimerState.Complete:
                        return true;
                    case EETimerData.EETimerState.Pause:
                        return false;
                    case EETimerData.EETimerState.Update:
                        var now = DateTime.Now;
                        var delta = (now - timerData.LastDateTime).Milliseconds * 0.001f;
                        timerData.LastDateTime = now;
                        timerData.CurrentTime = Mathf.MoveTowards(timerData.CurrentTime, timerData.FinalTime, delta);
                        if (timerData.CurrentTime.IsAlmostEqual(timerData.FinalTime))
                        {
                            timerData.TimerState = EETimerData.EETimerState.Complete;
                            return true;
                        }
                        break;
                    case EETimerData.EETimerState.Cancel:
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return false;
            }
            
        }

        private static IEnumerator<float> Loop()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                var i = 0;
                while (i < timers.Count)
                {
                    var timerData = timers[i];
                    switch (timerData.TimerState)
                    {
                        case EETimerData.EETimerState.Update:
                            timerData.OnUpdateEvent.Call();
                            i++;
                            continue;
                        case EETimerData.EETimerState.Pause:
                            timerData.OnPauseEvent.Call();
                            i++;
                            continue;
                        case EETimerData.EETimerState.Complete:
                            timerData.Action.Call();
                            timerData.OnCompleteEvent.Call();
                            timers.Remove(timerData);
                            continue;
                        case EETimerData.EETimerState.Cancel:
                            timerData.OnCancelEvent.Call();
                            timers.Remove(timerData);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
        
        public static void CancelTimer(Action action)
        {
            var timerData = GetTimerDataByAction(action);
            timerData.TimerState = EETimerData.EETimerState.Cancel;
            timers.Remove(timerData);
        }

        public static void CancelTimer(EETimerData timerData)
        {
            CancelTimer(timerData.Action);
        }
        
        public static void StopAllTimersForComponentID(int componentID)
        {
            var list = timers.Where(a => a.ComponentID == componentID).ToList();
            foreach (var timerData in list)
            {
                CancelTimer(timerData.Action);
            }
        }

        public static bool IsTimerExist(Action action)
        {
            var timerData = GetTimerDataByAction(action);
            return timerData != null;
        }

        public static void AddCurrentTime(Action action, int additionalTime)
        {
            var timerData = GetTimerDataByAction(action);
            timerData.CurrentTime += additionalTime;
        }
        
        public static void SetCurrentTime(Action action, int newTime)
        {
            var timerData = GetTimerDataByAction(action);
            timerData.CurrentTime = newTime;
        }
        
        public static void PauseTimer(Action action)
        {
            var timerData = GetTimerDataByAction(action);
            timerData.TimerState = EETimerData.EETimerState.Pause;
        }
        
        public static float GetCurrentTime(Action action)
        {
            var timerData = GetTimerDataByAction(action);
            return timerData.CurrentTime;
        }
        
        public static void ResumeTimer(Action action)
        {
            var timerData = GetTimerDataByAction(action);
            timerData.TimerState = EETimerData.EETimerState.Update;
        }

        private static EETimerData GetTimerDataByAction(Action action)
        {
            return timers.FirstOrDefault(a => a.Name == action.ToUniqueString());
        }
    }
}