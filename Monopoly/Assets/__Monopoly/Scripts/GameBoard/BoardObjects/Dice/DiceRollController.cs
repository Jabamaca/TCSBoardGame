using UnityEngine;
using System.Collections.Generic;
using GameUtils;

namespace Monopoly.Gameplay {
    public class DiceRollController : MonoBehaviour {

        #region Properties

        [Header ("Dice Roll Info")]
        [SerializeField]
        private List<DiceRegion> _diceRegions;
        private ShufflingList<DiceRegion> _diceRegionShuffle;

        [Header ("Wired Objects")]
        [SerializeField]
        private List<DiceController> _dices = new ();
        private ShufflingList<DiceController> _diceShuffle;

        #endregion

        #region Unity Internal Methods

        private void Start () {
            ResetAllDice ();
            _diceRegionShuffle = ShufflingList<DiceRegion>.ShufflingListFrom (_diceRegions);
            _diceShuffle = ShufflingList<DiceController>.ShufflingListFrom (_dices);
        }

        #endregion

        #region Methods

        public void Roll (List<int> numbers) {
            ResetAllDice ();
            _diceRegionShuffle.Shuffle ();
            _diceShuffle.Shuffle ();

            float randAngle = Random.Range (0f, 360f);

            int numCount = numbers.Count;
            int diceCount = _dices.Count;
            for (int i = 0; i < numCount && i < diceCount; i++) {
                DiceController randDice = _diceShuffle.ShuffledList[i];
                randDice.gameObject.SetActive (true);
                randDice.RollFixedNumber (numbers[i]);

                // Random position.
                randDice.transform.localPosition = _diceRegionShuffle.ShuffledList[i].GetRandomizedPoint ();
                // Random angle.
                randDice.SetRollAngleDeg (randAngle);
            }
        }

        public void ResetAllDice () {
            foreach (DiceController dice in _dices) {
                dice.ResetToInitPose ();
                dice.gameObject.SetActive (false);
            }
        }

        #endregion

    }

    [System.Serializable]
    internal class DiceRegion {

        #region Properties

        public Vector3 regionCenter = new ();
        public float randXRange = 0f;
        public float randZRange = 0f;

        #endregion

        #region Methods

        public Vector3 GetRandomizedPoint () {
            float randX = Random.Range (-randXRange, randXRange);
            float randZ = Random.Range (-randZRange, randZRange);

            return regionCenter + new Vector3 (randX, 0f, randZ);
        }

        #endregion

    }
}