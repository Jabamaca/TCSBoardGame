namespace Monopoly.Server.Data {

    // TODO: TO EDIT Jaba Define Bytes

    public enum GameEventType {
        NULL = 0x00,
        CONNECT,
        DISCONNECT,
        PLAYER_JOIN,
        PLAYER_LEAVE,
        PLAYER_DISCONNECT,
        PLAYER_RECONNECT,
        GAME_START,
        GAME_TURN_CHANGE,
        GAME_END,
        PLAYER_DICEROLL,
        PLAYER_GAIN_MONEY,
        PLAYER_LOSE_MONEY,
        PLAYER_MOVE,
        PLAYER_BUY_PROPERTY,
        PLAYER_UPGRADE_PROPERTY,
        PLAYER_SELL_PROPERTY,
        PLAYER_GOTO_JAIL,
    }

}