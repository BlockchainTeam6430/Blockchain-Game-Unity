using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using BestHTTP;
using MEC;
using Plugins.EntenEller.Base.Scripts.Cache;
using Plugins.EntenEller.Base.Scripts.Timers;

namespace Plugins.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTP : EEBehaviour
    {
        [SerializeField] private string mainURL;
        [SerializeField] public string url;
        [SerializeField] private bool isRepeatOnError;
        [SerializeField] private HTTPMethods Method = HTTPMethods.Get;
        [SerializeField] private bool isCached;
        
        private HTTPRequest request;
        public EESuperAction StartSuperAction, RepeatSuperAction, SuccessSuperAction, FailSuperAction, AlwaysSuperAction;
        public Action<HTTPResponse> SuccessEvent;
        public Action<HTTPRequest> NewRequestEvent;

        private static bool isInit;
        private static List<CompletedHTTP> completed = new List<CompletedHTTP>();
        
        protected override void EEAwake()
        {
            base.EEAwake();
            
            if (!isInit)
            {
                isInit = true;
                HTTPManager.Setup();
                Timing.RunCoroutineSingleton(Loop(), ComponentID, SingletonBehavior.Overwrite);
            }

            static IEnumerator<float> Loop()
            {
                while (true)
                {
                    yield return Timing.WaitForOneFrame;
                    while (completed.Count > 0)
                    {
                        var a = completed[0];
                        if (a.Response != null)
                        {
                            a.Source.SuccessEvent.Call(a.Response);
                            a.Source.SuccessSuperAction.Call(a.Source.ComponentID);
                        }
                        else
                        {
                            a.Source.FailSuperAction.Call(a.Source.ComponentID);
                            if (!a.Source.isRepeatOnError) continue;
                            a.Source.RepeatSuperAction.Call(a.Source.ComponentID);
                            a.Source.Call();
                        }
                        a.Source.AlwaysSuperAction.Call();
                        completed.RemoveAt(0);
                    }
                }
            }
        }

        public void Call()
        {
            if (isCached && GlobalCache.Get(url) != null)
            {
                var data = (CompletedHTTP) GlobalCache.Get(url);
                data.Source = this;
                completed.Add(data);
                return;
            }
            #if UNITY_WEBGL
                request = new HTTPRequest(new Uri(mainURL + url), Method, Done);
                request.Send();
            #else
                request = new HTTPRequest(new Uri(mainURL + url), Method);
                Async();
            #endif
            NewRequestEvent.Call(request);
            StartSuperAction.Call(ComponentID);
        }
#if !UNITY_WEBGL      
        private async void Async()
        {
            await Task.Run(async () =>
            {
                try
                {
                    var response = await request.GetHTTPResponseAsync();
                    var completedHTTP = new CompletedHTTP
                    {
                        Source = this,
                        Request = request,
                        Response = response
                    };
                    completed.Add(completedHTTP);
                    if (isCached) GlobalCache.Set(url, completedHTTP);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    completed.Add(new CompletedHTTP
                    {
                        Source = this,
                        Request = request,
                        Response = null
                    });
                }
            });
        }
#endif       
        private void Done(HTTPRequest originalRequest, HTTPResponse response)
        {
            if (originalRequest.State == HTTPRequestStates.Error) Debug.LogError("Request Finished with Error! " + (originalRequest.Exception != null ? (originalRequest.Exception.Message + "\n" + originalRequest.Exception.StackTrace) : "No Exception"));
            var completedHTTP = new CompletedHTTP
            {
                Source = this,
                Request = originalRequest,
                Response = response
            };
            completed.Add(completedHTTP);
            if (isCached) GlobalCache.Set(url, completedHTTP);
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            Stop();
        }

        public void Stop()
        {
            if (request == null) return;
            request.Abort();
            request.Dispose();
        }

        public void ChangeURL(string url)
        {
            this.url = url;
        }

        public struct CompletedHTTP
        {
            public EEHTTP Source;
            public HTTPRequest Request;
            public HTTPResponse Response;
        }
    }
}
