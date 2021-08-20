using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Rawrshak
{
    [CustomEditor(typeof(EncryptedKeyStoreFileWallet), true)]
    [CanEditMultipleObjects]
    public class EncryptedKeyStoreFileWalletEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            // Apply changes to the serializedProperty - always do this at the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }
    }
}