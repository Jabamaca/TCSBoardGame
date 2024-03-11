using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace GameUtils {
    public abstract class ObjectPool<T> : MonoBehaviour where T : PoolableObject {

        #region Properties

        [Header ("Pooling Info")]
        [SerializeField]
        private T _objectToPool;
        [SerializeField]
        private int _initialInstanceCount;

        [Header ("Pooling State")]
        [SerializeField]
        private readonly List<T> _pooledObjects = new ();
        [SerializeField]
        private readonly List<T> _unpooledObjects = new ();

        #endregion

        #region Unity Internal Methods

        private void Awake () {
            // Assign delegates to Scene Loaded pool objects.
            foreach (T pObj in _pooledObjects) {
                pObj.OnObjDestroy += OnObjDestroy;
                pObj.OnObjPool += OnObjPool;
            }
            foreach (T pObj in _unpooledObjects) {
                pObj.OnObjDestroy += OnObjDestroy;
                pObj.OnObjPool += OnObjPool;
            }

            CreateInitialObjects ();
        }

        private void OnDestroy () {
            // Remove delegates from ALL pool objects.    
            foreach (T pObj in _pooledObjects) {
                pObj.OnObjDestroy -= OnObjDestroy;
                pObj.OnObjPool -= OnObjPool;
            }
            foreach (T pObj in _unpooledObjects) {
                pObj.OnObjDestroy -= OnObjDestroy;
                pObj.OnObjPool -= OnObjPool;
            }
        }

        #endregion

        #region Methods

        public T UnpoolObject () {
            T returnObj;
            if (_pooledObjects.Count > 0) {
                returnObj = _pooledObjects[0];
                _pooledObjects.Remove (returnObj);
            } else {
                returnObj = CreateNewInstance ();
            }

            returnObj.gameObject.SetActive (true);
            _unpooledObjects.Add (returnObj);

            return returnObj;
        }

        private void PoolObject (T pObj) {
            _unpooledObjects.Remove (pObj);

            pObj.ResetToDefault ();
            pObj.gameObject.SetActive (false);
            _pooledObjects.Add (pObj);
        }

        private void CreateInitialObjects () {
            for (int i = _pooledObjects.Count + _unpooledObjects.Count; i < _initialInstanceCount; i++) {
                PoolObject (CreateNewInstance ());
            }
        }

        private T CreateNewInstance () {
            T newInstance = Instantiate (_objectToPool);
            newInstance.OnObjDestroy += OnObjDestroy;
            newInstance.OnObjPool += OnObjPool;

            return newInstance;
        }

        #endregion

        #region Delegate Methods

        private void OnObjPool (PoolableObject poolableObj) {
            PoolObject (poolableObj as T);
        }

        private void OnObjDestroy (PoolableObject poolableObj) {
            T pObj = poolableObj as T;
            _pooledObjects.Remove (pObj);
            _unpooledObjects.Remove (pObj);
        }

        #endregion

    }
}