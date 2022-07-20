using System.Collections.Generic;

public static class GameData
{
    public static int playersCount;
    public static int startMoney;
    public static int moneyForCircle;
    public static List<LobbyUser> lobbyUsers = new List<LobbyUser>();

    public static void AddUser(LobbyUser lobbyUser)
    {
        lobbyUsers.Add(lobbyUser);
    }
}
