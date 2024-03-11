using System.Collections.Generic;
using UnityEngine;

namespace Monopoly.Gameplay {

    public class DiceController : MonoBehaviour {

        #region Properties

        [Header ("Rolling Position Info")]
        [SerializeField]
        private int _predeterminedRoll;
        [SerializeField]
        private Vector3 _preRollGraphicEulerAngle;
        [SerializeField]
        private Vector3 _initLocalPosition;
        [SerializeField]
        private Vector3 _initLocalEulerAngles;
        private Quaternion _initLocalQuatAngle;
        [SerializeField]
        private float _rollAngleDeg;

        [Header ("Rolling Force Info")]
        [SerializeField]
        private Vector3 _rollForce;
        [SerializeField]
        private Vector3 _rollTorque;

        [Header ("Dice Graphic")]
        [SerializeField]
        private List<Vector3> _diceGraphicFaceEulerAngles;

        [Header ("Wired Objects")]
        [SerializeField]
        private Transform _diceFacesTransform;
        [SerializeField]
        private Transform _diceFacesGraphic;
        [SerializeField]
        private Rigidbody _diceRigidBody;

        #endregion

        #region Unity Internal Methods

        private void Start () {
            _initLocalQuatAngle = Quaternion.Euler (_initLocalEulerAngles);
        }

#if UNITY_EDITOR
        private void OnValidate () {
            _initLocalQuatAngle = Quaternion.Euler (_initLocalEulerAngles);
            ResetToInitPose ();

            // Sets Dice graphic pre-roll angle.
            _diceFacesTransform.localRotation = Quaternion.Euler (_preRollGraphicEulerAngle);

            SetFixedRoll (_predeterminedRoll);
        }
#endif

        #endregion

        #region Methods

        private void Roll () {
            ResetToInitPose ();

            // Reset velocity.
            _diceRigidBody.velocity = Vector3.zero;
            _diceRigidBody.angularVelocity = Vector3.zero;

            // Re-apply force.
            _diceRigidBody.AddForce (transform.localRotation * _rollForce);
            _diceRigidBody.AddTorque (transform.localRotation * _rollTorque);
        }

        public void RollFixedNumber (int number) {
            SetFixedRoll (number);
            Roll ();
        }

        public void SetFixedRoll (int fixedRoll) {
            int faceCount = _diceGraphicFaceEulerAngles.Count;

            if (faceCount <= 0)
                return;

            if (fixedRoll < 1) {
                _predeterminedRoll = 1;
                _diceFacesGraphic.localRotation = Quaternion.Euler (_diceGraphicFaceEulerAngles[0]);
            } else if (fixedRoll > faceCount) {
                _predeterminedRoll = faceCount;
                _diceFacesGraphic.localRotation = Quaternion.Euler (_diceGraphicFaceEulerAngles[faceCount - 1]);
            } else {
                _predeterminedRoll = fixedRoll;
                _diceFacesGraphic.localRotation = Quaternion.Euler (_diceGraphicFaceEulerAngles[fixedRoll - 1]);
            }
        }

        public void ResetToInitPose () {
            // Setup roll angle.
            transform.localRotation = Quaternion.Euler (0f, _rollAngleDeg, 0f);
            _diceRigidBody.transform.SetLocalPositionAndRotation (_initLocalPosition, _initLocalQuatAngle);
        }

        public void SetRollAngleDeg (float angle) {
            _rollAngleDeg = angle;
        }

        #endregion

    }

}