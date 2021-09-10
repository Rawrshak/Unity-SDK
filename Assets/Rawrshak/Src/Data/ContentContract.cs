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
        private bool isValid;

        public void OnEnable()
        {
            isValid = false;
        }

        public void VerifyContracts()
        {
            // Todo: verify contracts, Use Content.SupportsInterface(0xBF2FD945)
            Debug.LogError("Invalid Contracts.");
        }

        public bool IsValid()
        {
            return isValid;
        }

    }
}