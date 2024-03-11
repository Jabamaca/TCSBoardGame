using UnityEngine;
using System.Collections.Generic;
using System;

namespace Monopoly.Scriptables {

    [CreateAssetMenu (fileName = "_COLOR_LIB", menuName = "Monopoly/Color Material Library", order = 1001)]
    public class ColorMatLibrary : ScriptableObject {

        #region Properties

        [SerializeField]
        private List<ColorReference> _colorRefList = new ();

        #endregion

        #region Methods

        public Material ColorMaterialAtIndex (int i) {
            if (ColorRefAtIndex (i, out var colorRef)) {
                return colorRef.colorMaterial;
            }

            return null;
        }

        public Color ColorBaseAtIndex (int i) {
            if (ColorRefAtIndex (i, out var colorRef)) {
                return colorRef.baseColor;
            }

            return Color.clear;
        }

        private bool ColorRefAtIndex (int i, out ColorReference colRef) {
            colRef = null;

            if (_colorRefList.Count <= 0)
                return false;

            if (i < 0)
                colRef = _colorRefList[0];
            else if (i >= _colorRefList.Count)
                colRef = _colorRefList[^1];
            else
                colRef = _colorRefList[i];

            return true;
        }

        #endregion

    }

    [Serializable]
    public class ColorReference {

        public Material colorMaterial;
        public Color baseColor;

    }

}