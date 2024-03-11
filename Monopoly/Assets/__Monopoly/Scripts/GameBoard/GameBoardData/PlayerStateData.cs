using System;

namespace Monopoly.Gameplay.Data {

    [Serializable]
    public class PlayerStateData {

        #region Properties

        public int playerID;
        public int placementTileID;
        public int cashAmount;
        public int jailTurns;

        #endregion

        #region Internal Methods

        public override bool Equals (object obj) {
            PlayerStateData other = (PlayerStateData)obj;

            if (this.playerID != other.playerID ||
                this.placementTileID != other.placementTileID ||
                this.cashAmount != other.cashAmount ||
                this.jailTurns != other.jailTurns) {

                return false;
            }

            return true;
        }

        public override int GetHashCode () {
            return base.GetHashCode ();
        }

        #endregion

    }

}