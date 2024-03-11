using System;

namespace Monopoly.Server.Data {
    [Serializable]
    public class GameEventData {

        // Might convert to bytes.
        public GameEventType eventDataType;
        public string eventJSONData;

    }
}