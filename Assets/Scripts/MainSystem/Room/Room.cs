using System;
using UnityEngine;

[Serializable]
public enum RoomType
{   
    Empty,      // ระวังเรื่องการเพิ่ม type มีผลกับ SaveGame
    Bedroom,    // ถ้าอยากเพิ่มต้อง custom index เอา
    Fitness,
    Toilet,
    GarbageRoom,
    WashingMachineRoom
}
[Serializable]
public enum RoomTier
{  
    Normal,
    Extra,
    SuperExtra,
}
public class Room : MonoBehaviour
{
    public string roomName;
    public RoomType roomType;
    public RoomTier roomTier;
    public SpriteRenderer spriteRendererLeft;
    public SpriteRenderer spriteRendererRight;
    public Sprite[] roomTypeSpriteLeft;
    public Sprite[] roomTypeSpriteRight;
    public Sprite[] roomTierSpriteLeft;
    public Sprite[] roomTierSpriteRight;

    public Vector3 roomPosition;

    public int[] price_Type;
    public int[] price_Tier;

    private void Start()
    {
        roomName = gameObject.name;
        SaveManager.LoadRoom(this);

        UpdateSprite();
    }


    public void UpdateSprite()
    {
        if(roomType == RoomType.Empty)
        {
            spriteRendererLeft.sprite = roomTypeSpriteLeft[0];
            spriteRendererRight.sprite = roomTypeSpriteRight[0];
        }
        else if(roomType == RoomType.Bedroom)
        {
            if(roomTier == RoomTier.Normal)
            {
                spriteRendererLeft.sprite = roomTierSpriteLeft[0];
                spriteRendererRight.sprite = roomTierSpriteRight[0];
            }
            else if(roomTier == RoomTier.Extra)
            {
                spriteRendererLeft.sprite = roomTierSpriteLeft[1];
                spriteRendererRight.sprite = roomTierSpriteRight[1];
            }
            else if( roomTier == RoomTier.SuperExtra)
            {
                spriteRendererLeft.sprite = roomTierSpriteLeft[2];
                spriteRendererRight.sprite = roomTierSpriteRight[2];
            }
            else
            {
                Debug.Log("roomTier error");
            }
        }
        else if(roomType == RoomType.Fitness)
        {
            spriteRendererLeft.sprite = roomTypeSpriteLeft[2];
            spriteRendererRight.sprite = roomTypeSpriteRight[2];
        }
        else if(roomType == RoomType.Toilet)
        {
            spriteRendererLeft.sprite = roomTypeSpriteLeft[3];
            spriteRendererRight.sprite = roomTypeSpriteRight[3];
        }
        else if(roomType == RoomType.GarbageRoom)
        {
            spriteRendererLeft.sprite = roomTypeSpriteLeft[4];
            spriteRendererRight.sprite = roomTypeSpriteRight[4];
        }
        else if(roomType == RoomType.WashingMachineRoom)
        {
            spriteRendererLeft.sprite = roomTypeSpriteLeft[5];
            spriteRendererRight.sprite = roomTypeSpriteRight[5];
        }
        else
        {
            Debug.Log("roomType error");
        }
    }
    public void OnPointerClicked()
    {
        BuyPanel.Instance.Initialize(this);
        Debug.Log("room clicked");
    }

    public int CalculateBuyRoomPrice(RoomType _type)
    {
        if (_type == RoomType.Bedroom)
        {
            return price_Tier[(int)roomTier];
        }
        else
        {
            // type ถูกมองข้าม แต่ต้องมีค่า เพราะยังใช้ index
            return price_Type[(int)_type];
        }
    }
    
    public void BuyRoom(RoomType _type)
    {
        if (!Money.Instance.TrySpend(CalculateBuyRoomPrice(_type)))
        {
            Debug.Log(">> Need more money. Can NOT make purchase");
            return;
        }

        roomType = _type;

        if(roomType == RoomType.Bedroom)
        {
            roomTier = RoomTier.Normal;
        }

        UpdateSprite();
        Debug.Log("BUY");

        
    }
    public void UpgradeRoom()
    {
        // check => max / ราคา
        // set => เพิ่ม index

        if (!Money.Instance.TrySpend(CalculateBuyRoomPrice(roomType)))
        {
            Debug.Log(">> Need more money. Can NOT make purchase");
            return;
        }

        int current = (int)roomTier;
        int max = Enum.GetValues(typeof(RoomTier)).Length - 1;
        if (current < max)
        {
            roomTier = (RoomTier)(current + 1);
            Debug.Log("Room upgraded to: " + roomTier);
        }
        else
        {
            Debug.Log("RoomTier is already max Tier.");
        }

        UpdateSprite();
        Debug.Log("UPGRADE");
    }
    public int CalculateSellRoomPrice()
    {
        if(roomType == RoomType.Bedroom)
        {
            if(roomTier == RoomTier.Normal)
            {
                return 0; ///////Normal
            }
            else if(roomTier == RoomTier.Extra)
            {
                return 0; ////////Extra
            }
            else
            {
                return 0; ///////super extra
            }
        }
        else if(roomType == RoomType.Fitness)
        {
            return 0; /////////Fitness
        }
        else if(roomType == RoomType.Toilet)
        {
            return 0; //////////Toilet
        }
        else if (roomType == RoomType.GarbageRoom)
        {
            return 0; //////////GarbageRoom
        }
        else
        {
            return 0; /////////washing
        }
    }
    public void SellRoom()
    {
        // คำนวณราคาขาย
        // set เป็นห้องว่าง
        Money.Instance.AddMoney(CalculateSellRoomPrice());

        roomType = RoomType.Empty;
        roomTier = RoomTier.Normal;
        UpdateSprite();
    }
}
