namespace Monopoly.Server.Data {

    // TODO: TO EDIT Jaba Define Bytes

    public enum GameSignalType {
        NULL = 0x00,
        CONNECT,
        DISCONNECT,
        REQUEST_PLAYER_JOIN,
        REQUEST_PLAYER_LEAVE,
        GAMEINPUT_PLAYER_DICEROLL,
    }

}