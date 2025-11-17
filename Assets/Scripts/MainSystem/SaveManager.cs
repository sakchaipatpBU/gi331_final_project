using UnityEngine;

public static class SaveManager
{
    public static void SaveRoom(Room room)
    {
        string keyPrefix = room.roomName;

        PlayerPrefs.SetString($"{keyPrefix}_Name", room.roomName);
        PlayerPrefs.SetInt($"{keyPrefix}_Type", (int)room.roomType);

        if(room.roomType == RoomType.Bedroom)
        {
            PlayerPrefs.SetInt($"{keyPrefix}_Tier", (int)room.roomTier);
        }

        PlayerPrefs.Save();
        Debug.Log($"{room.roomName} data saved via SaveGame.");
    }
    public static void LoadRoom(Room room)
    {
        string keyPrefix = room.roomName;

        if (!PlayerPrefs.HasKey($"{keyPrefix}_Name"))
        {
            Debug.Log($">> {room.roomName} !No save data found.");
            return;
        }

        int type = PlayerPrefs.GetInt($"{keyPrefix}_Type");
        room.roomType = (RoomType)type;

        if (room.roomType == RoomType.Bedroom)
        {
            int tier = PlayerPrefs.GetInt($"{keyPrefix}_Type");
            room.roomTier = (RoomTier)tier;
        }

        Debug.Log($"{room.roomName} data loaded via SaveGame.");
    }

    public static void SaveFloorCount()
    {
        PlayerPrefs.SetInt("floorCount", ElevetorManager.Instance.floorCount);
        PlayerPrefs.Save();
        Debug.Log(">> floorCount data saved via SaveManager.");
    }

    public static void LoadFloorCount()
    {
        if (!PlayerPrefs.HasKey("floorCount"))
        {
            Debug.Log(">> floorCount !No save data found.");
            return;
        }

        ElevetorManager.Instance.floorCount = PlayerPrefs.GetInt("floorCount");

        Debug.Log($"floorCount data loaded via SaveGame.");

    }

    public static void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", Money.Instance.currentMoney);
        PlayerPrefs.Save();
        Debug.Log(">> Money data saved via SaveManager.");
    }

    public static void LoadMoney()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            Debug.Log(">> Money !No save data found.");
            return;
        }

        Money.Instance.currentMoney = PlayerPrefs.GetInt("Money");

        Debug.Log($"Money data loaded via SaveGame.");
    }
}
