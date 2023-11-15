using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using Helika;

namespace Rawrshak
{
    public class HelikaAsset : MonoBehaviour
    {

        private Helika.EventManager eventManager;

        void Start()
        {
            eventManager = Helika.EventManager.Instance;
            eventManager.Init("4b22e2a34e2c95d9b46668a702ead7", "HelikaUnitySDK", Helika.HelikaBaseURL.Localhost);
        }


        async void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                await eventManager.TestHelikaAPI();
            }
        }
    }
}