using System;
using System.Collections.Generic;
using UnityEngine;
using Monopoly.Define;
using Monopoly.Scriptables;

namespace Monopoly.Gameplay {

    public class PropertyHouseController : MonoBehaviour {

        #region Properties

        [Header ("Property House Info")]
        [SerializeField]
        private int _upgradeLevel;
        public int UpgradeLevel => _upgradeLevel;
        [SerializeField]
        private PlayerColorEnum _ownerPlayerColor;

        [Header ("Wired Objects")]
        [SerializeField]
        private MeshRenderer _meshRenderer;
        [SerializeField]
        private MeshFilter _meshFilter;
        [SerializeField]
        private List<MeshSizeData> _upgradeTier3DMeshes = new ();
        [SerializeField]
        private ColorMatLibrary _playerColors;

        #endregion

        #region Unity Internal Methods

        private void Start () {
            UpdateOwnerColor ();
            UpdateUpgradeMesh ();
        }

#if UNITY_EDITOR

        private void OnValidate () {
            UpdateOwnerColor ();
        }

#endif

        #endregion

        #region Methods

        public void SetUpgradeLevel (int level) {
            _upgradeLevel = level;
            UpdateUpgradeMesh ();
        }

        public void UpgradeLevelUp () {
            _upgradeLevel++;
            UpdateUpgradeMesh ();
        }

        private void UpdateUpgradeMesh () {
            if (_upgradeLevel <= 0 || _upgradeTier3DMeshes.Count <= 0) {
                _meshRenderer.enabled = false;
            } else if (_upgradeLevel > _upgradeTier3DMeshes.Count) {
                _meshRenderer.enabled = true;
                MeshSizeData msd = _upgradeTier3DMeshes[^1];
                _meshFilter.sharedMesh = msd.mesh;
                _meshFilter.transform.localScale = msd.scale;
            } else {
                _meshRenderer.enabled = true;
                MeshSizeData msd = _upgradeTier3DMeshes[_upgradeLevel - 1];
                _meshFilter.sharedMesh = msd.mesh;
                _meshFilter.transform.localScale = msd.scale;
            }
        }

        public void SetPlayerColor (PlayerColorEnum color) {
            _ownerPlayerColor = color;
            UpdateOwnerColor ();
        }

        private void UpdateOwnerColor () {
            Material colorMat = _playerColors.ColorMaterialAtIndex ((int)_ownerPlayerColor);
            _meshRenderer.material = colorMat;
        }

        #endregion

        [Serializable]
        private class MeshSizeData {

            public Mesh mesh = null;
            public Vector3 scale = Vector3.zero;

        }

    }

}