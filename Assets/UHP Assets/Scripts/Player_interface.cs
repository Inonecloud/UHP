public interface Player {

    string playerName();
    void playerRole(PlayerRole role);
    bool puckOwner();
}

public enum PlayerRole
{
    Forward,
    Defender,
    Goalkeeper
}