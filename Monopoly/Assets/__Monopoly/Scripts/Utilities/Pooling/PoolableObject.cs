using UnityEngine;

namespace GameUtils {
    public abstract class PoolableObject : MonoBehaviour {

        internal delegate void OnObjDestroyHandler (PoolableObject obj);
        internal OnObjDestroyHandler OnObjDestroy = delegate { };
        internal delegate void OnObjPoolHandler (PoolableObject obj);
        internal OnObjPoolHandler OnObjPool = delegate { };

        #region Properties

        #endregion

        #region Unity Internal Methods

        private void OnDestroy () {
            OnObjDestroy?.Invoke (this);
        }

        #endregion

        #region Methods

        internal virtual void ResetToDefault () {
            // Override to customize.
        }

        public void Pool () {
            OnObjPool?.Invoke (this);
        }

        #endregion

    }
}