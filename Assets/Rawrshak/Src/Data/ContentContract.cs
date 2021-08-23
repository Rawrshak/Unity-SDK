using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="ContentContract", menuName="Rawrshak/Create Content Contract Object")]
    public class ContentContract : ScriptableObject
    {
        public string contract;
        public string systemRegistry;
        private bool isValid;

        public void OnEnable()
        {
            isValid = false;
        }

        public void VerifyContracts()
        {
            // Todo: verify contracts
            Debug.LogError("Invalid Contracts.");
        }

        public bool IsValid()
        {
            return isValid;
        }

    }
}