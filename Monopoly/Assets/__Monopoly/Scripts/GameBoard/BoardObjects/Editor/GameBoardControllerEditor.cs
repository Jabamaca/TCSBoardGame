using UnityEngine;
using UnityEditor;
using Monopoly.Gameplay;

namespace Monopoly.EditorUI {
    [CustomEditor (typeof (GameBoardController))]
    public class GameBoardControllerEditor : Editor {

        #region Properties

        private SerializedProperty _mapName;
        private SerializedProperty _diceRoller;
        private SerializedProperty _playerPieces;
        private SerializedProperty _startingTile;
        private SerializedProperty _boardTiles;

        #endregion

        #region Unity Internal Methods

        private void OnEnable () {
            _mapName = serializedObject.FindProperty ("_mapName");
            _diceRoller = serializedObject.FindProperty ("_diceRoller");
            _playerPieces = serializedObject.FindProperty ("_playerPieces");
            _boardTiles = serializedObject.FindProperty ("_boardTiles");
            _startingTile = serializedObject.FindProperty ("_startingTile");
        }

        public override void OnInspectorGUI () {
            serializedObject.UpdateIfRequiredOrScript ();

            EditorGUILayout.LabelField ("Board Info", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField (_mapName, new GUIContent ("Map Name"));

            EditorGUILayout.Space (10);

            EditorGUILayout.LabelField ("Game Board Elements", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField (_diceRoller, new GUIContent ("Dice Roller"));
            EditorGUILayout.PropertyField (_playerPieces, new GUIContent ("Player Pieces"));

            EditorGUILayout.Space (10);

            EditorGUILayout.LabelField ("Map Details", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField (_startingTile, new GUIContent ("Starting Tile"));
            EditorGUILayout.PropertyField (_boardTiles, new GUIContent ("Board Tiles"));

            EditorGUILayout.Space (10);

            if (GUILayout.Button ("Generate Game Board Data")) {
                GenerateBoardData ();
            }

            serializedObject.ApplyModifiedProperties ();
        }

        #endregion

        #region Methods

        private void GenerateBoardData () {
            GameBoardController gbc = target as GameBoardController;
            gbc.GenerateBoardDataJSON ();
        }

        #endregion
    }
}