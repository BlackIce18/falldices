using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int playersCount;
    public static int startMoney;
    public static int moneyForCircle;
    public static List<UserInfo> users = new List<UserInfo>();

    public static void AddUser(Color32 color, Models model, Sprite icon, string nickname)
    {
        UserInfo userInfo = new UserInfo(color, model, icon, nickname);
        users.Add(userInfo);
    }
}
