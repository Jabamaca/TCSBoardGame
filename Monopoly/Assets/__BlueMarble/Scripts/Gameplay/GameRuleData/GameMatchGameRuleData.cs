using System;
using System.Collections.Generic;

namespace BlueMarble.Gameplay.RuleData {
    public class GameMatchGameRuleData {

        #region Properties

        public UInt32 gameID;

        public List<ItemGameRuleData> itemLibraryRuleDataList;
        public List<TileGameRuleData> tileOrderRuleDataList;

        #endregion

    }
}