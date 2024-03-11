using UnityEngine;
using UnityEditor;
using Monopoly.Server;

namespace Monopoly.EditorUI {
    [CustomEditor (typeof (GameHostMediator))]
    public class GameActivityMediatorEditor : Editor {

        #region Properties

        private SerializedProperty _isRemote;
        private SerializedProperty _remoteGameRuleHost;
        private SerializedProperty _localGameRuleHost;

        #endregion

        #region Unity Internal Methods

        private void OnEnable () {
            _isRemote = serializedObject.FindProperty ("_isRemote");
            _remoteGameRuleHost = serializedObject.FindProperty ("_remoteGameRuleHost");
            _localGameRuleHost = serializedObject.FindProperty ("_localGameRuleHost");
        }

        public override void OnInspectorGUI () {
            serializedObject.UpdateIfRequiredOrScript ();

            EditorGUILayout.LabelField ("Board Info", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField (_isRemote, new GUIContent ("Is Using Remote Host"));
            EditorGUILayout.HelpBox ("WARNING: NEVER Modify this value in the Inspector during Play Mode", MessageType.Warning);

            EditorGUILayout.Space (10);

            EditorGUILayout.LabelField ("Wired Objects", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField (_remoteGameRuleHost, new GUIContent ("Remote Host Object"));
            EditorGUILayout.PropertyField (_localGameRuleHost, new GUIContent ("Local Host Object"));

            serializedObject.ApplyModifiedProperties ();
        }

        #endregion

        #region Methods

        #endregion

    }
}