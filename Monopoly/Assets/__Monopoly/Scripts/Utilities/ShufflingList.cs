using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtils {

    public class ShufflingList<T> {

        #region Properties

        private readonly List<T> _itemList = new ();
        private readonly List<T> _randomPool = new ();
        private readonly List<T> _shuffledList = new ();

        public IReadOnlyList<T> ShuffledList => _shuffledList;

        #endregion

        #region Constructors

        public static ShufflingList<T> ShufflingListFrom (IEnumerable<T> initList) {
            ShufflingList<T> newSL = new ();

            newSL.SetNewItemList (initList, true);

            return newSL;
        }

        #endregion

        #region Methods

        public void SetNewItemList (IEnumerable<T> newList, bool shuffle) {
            _itemList.Clear ();
            AddItemsToList (newList, shuffle);
        }

        public void AddItemToList (T item, bool shuffle) {
            _itemList.Add (item);

            if (shuffle) {
                Shuffle ();
            } else {
                _shuffledList.Add (item);
            }
        }

        public void AddItemsToList (IEnumerable<T> items, bool shuffle) {
            _itemList.AddRange (items);

            if (shuffle) {
                Shuffle ();
            } else {
                _shuffledList.AddRange (items);
            }
        }

        public void Shuffle () {
            _randomPool.Clear ();
            _randomPool.AddRange (_itemList);
            _shuffledList.Clear ();

            int itemCount = _randomPool.Count;
            for (int j = 0; j < itemCount; j++) {
                int randIndex = Random.Range (0, _randomPool.Count);
                _shuffledList.Add (_randomPool[randIndex]);
                _randomPool.RemoveAt (randIndex);
            }
        }

        #endregion

    }

}