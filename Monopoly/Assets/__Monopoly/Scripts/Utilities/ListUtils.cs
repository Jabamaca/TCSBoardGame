using System.Collections.Generic;

namespace GameUtils {

    public static class ListUtils {

        #region Methods

        public static bool CheckEquals<T> (List<T> list1, List<T> list2) {
            // Check dictionary content count.
            if (list1.Count != list2.Count)
                return false;

            int itemCount = list1.Count;
            for (int i = 0; i < itemCount; i++) {
                // Check equality of same indexes.
                if (!list1[i].Equals (list2[i]))
                    return false;
            }

            return true;
        }

        #endregion

    }

}