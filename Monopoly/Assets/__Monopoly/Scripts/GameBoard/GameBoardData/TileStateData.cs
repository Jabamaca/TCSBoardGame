using System;

namespace Monopoly.Gameplay.Data {

    [Serializable]
    public class TileStateData {

        #region Properties

        public int tileID;
        public int ownerPlayerID;
        public int propertyLevel;

        #endregion

        #region Internal Methods

        public override bool Equals (object obj) {
            TileStateData other = (TileStateData)obj;

            if (this.tileID != other.tileID ||
                this.ownerPlayerID != other.ownerPlayerID ||
                this.propertyLevel != other.propertyLevel) {

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