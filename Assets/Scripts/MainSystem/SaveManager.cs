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
        if (room.renter != null)
        {
            PlayerPrefs.SetString($"{keyPrefix}_Renter_Name", room.renter.name);
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

        if (PlayerPrefs.HasKey($"{keyPrefix}_Renter_Name"))
        {
            for(int i = RenterManager.Instance.allRenters.Count - 1;
                i >= 0; i--)
            {
                if (RenterManager.Instance.allRenters[i].rentRoom == null)
                {
                    RenterManager.Instance.allRenters[i].Initialize(room);
                }
            }
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
    public static void SaveReputation()
    {
        PlayerPrefs.SetInt("Reputation", Reputation.Instance.currentReputation);
        PlayerPrefs.Save();
        Debug.Log(">> Reputation data saved via SaveManager.");
    }

    public static void LoadReputation()
    {
        if (!PlayerPrefs.HasKey("Reputation"))
        {
            Debug.Log(">> Reputation !No save data found.");
            return;
        }

        Reputation.Instance.currentReputation = PlayerPrefs.GetInt("Reputation");

        Debug.Log($"Reputation data loaded via SaveGame.");
    }

    public static void SaveRenterCount()
    {
        PlayerPrefs.SetInt("RenterCount", RenterManager.Instance.allRenters.Count);
        PlayerPrefs.Save();
        Debug.Log(">> RenterCount data saved via SaveManager.");
    }
    public static void LoadRenterCount()
    {
        if (!PlayerPrefs.HasKey("RenterCount"))
        {
            Debug.Log(">> RenterCount !No save data found.");
            return;
        }
        RenterManager.Instance.onStartRenter = PlayerPrefs.GetInt("RenterCount");
        Debug.Log($"RenterCount data loaded via SaveGame.");
    }
}
