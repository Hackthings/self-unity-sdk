﻿/**
* Copyright 2016 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.Watson.DeveloperCloud.Widgets;
using IBM.Watson.Self.Gestures;
using System;
using System.Collections;
using UnityEngine;

namespace IBM.Watson.Self.Widgets
{
    public class SelfStatusGesture : Widget, IGesture
    {
        #region Private Data
        [SerializeField]
        private string m_GestureId = "update_status";
        [SerializeField]
        private bool m_Override = true;
		[SerializeField]
		private Output m_Status = new Output( typeof(StatusData), true );
        private string m_InstanceId = Guid.NewGuid().ToString();
        #endregion

        #region MonoBehavior interface
        protected override void Start()
        {
            base.Start();
            GestureManager.Instance.AddGesture(this, m_Override);
        }
        #endregion

        #region Widget interface
        protected override string GetName()
        {
            return "SelfSpeechGesture";
        }
        #endregion

        #region IGesture interface
        public string GetGestureId()
        {
            return m_GestureId;
        }
        //! return an ID unique to this instance
        public string GetInstanceId()
        {
            return m_InstanceId;
        }
        //! Initialize this gesture object, returns false if gesture can't be initialized
        public bool OnStart()
        {
            return true;
        }

        //! Shutdown this gesture object.
        public bool OnStop()
        {
            return true;
        }
        //! Execute this gesture, the provided callback should be invoked when the gesture is complete.
        public bool Execute(OnGestureDone a_Callback, IDictionary a_Params)
        {
            bool bError = false;

            string status = a_Params["status"] as string;
            m_Status.SendData( new StatusData(status));

            if (a_Callback != null)
                a_Callback(this, bError);
            return true;
        }
        //! Abort this gesture, if true is returned then abort succeeded and callback will NOT be invoked.
        public bool Abort()
        {
            return true;
        }
        #endregion
    }
}
