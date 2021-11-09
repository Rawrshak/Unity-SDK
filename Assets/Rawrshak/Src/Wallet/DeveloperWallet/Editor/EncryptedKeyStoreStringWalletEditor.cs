using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Rawrshak
{
    [CustomEditor(typeof(EncryptedKeyStoreStringWallet), true)]
    [CanEditMultipleObjects]
    public class EncryptedKeyStoreStringWalletEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            // Apply changes to the serializedProperty - always do this at the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }
    }
}