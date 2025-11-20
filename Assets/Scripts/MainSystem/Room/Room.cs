using System;
using UnityEngine;

[Serializable]
public enum RoomType
{   
    Empty,      // ระวังเรื่องการเพิ่ม type มีผลกับ SaveGame
    Bedroom,    // ถ้าอยากเพิ่มต้อง custom index เอา
    Fitness,
    MailRoom,
    LivingRoom,
    Kitchen
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
    public int[] baseRent;
    public int BaseRent
    {
        get
        {
            if(roomTier == RoomTier.Normal)
            {
                return baseRent[0];
            }
            else if(roomTier == RoomTier.Extra)
            {
                return baseRent[1];
            }
            else
            {
                return baseRent[2];
            }
        }
    }

    public Renter renter;

    private void Start()
    {
        roomName = gameObject.name;
        SaveManager.LoadRoom(this);

        UpdateSprite();
    }

    public void SetRenter(Renter _renter, bool checkIn)
    {
        if (roomType != RoomType.Bedroom) return;

        if (checkIn)
        {
            renter = _renter;

        }
        else
        {
            renter = null;
        }
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
        else if(roomType == RoomType.MailRoom)
        {
            spriteRendererLeft.sprite = roomTypeSpriteLeft[3];
            spriteRendererRight.sprite = roomTypeSpriteRight[3];
        }
        else if(roomType == RoomType.LivingRoom)
        {
            spriteRendererLeft.sprite = roomTypeSpriteLeft[4];
            spriteRendererRight.sprite = roomTypeSpriteRight[4];
        }
        else if(roomType == RoomType.Kitchen)
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
        SoundManager.Instance.PlaySFX("switch10");
    }

    public int CalculateBuyRoomPrice(RoomType _type)
    {
        if (_type == RoomType.Bedroom)
        {
            return price_Tier[(int)roomTier];
        }
        else
        {
            // type Bedroom ถูกมองข้าม แต่ต้องมีค่า เพราะยังใช้ index
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
                return CalculateBuyRoomPrice(RoomType.Bedroom)/2; //Normal
            }
            else if(roomTier == RoomTier.Extra)
            {
                return CalculateBuyRoomPrice(RoomType.Bedroom)/2; //Extra
            }
            else
            {
                return CalculateBuyRoomPrice(RoomType.Bedroom)/2; //super extra
            }
        }
        else if(roomType == RoomType.Fitness)
        {
            return CalculateBuyRoomPrice(RoomType.Fitness)/2; //Fitness
        }
        else if(roomType == RoomType.MailRoom)
        {
            return CalculateBuyRoomPrice(RoomType.MailRoom)/2; //MailRoom
        }
        else if (roomType == RoomType.LivingRoom)
        {
            return CalculateBuyRoomPrice(RoomType.LivingRoom)/2; //LivingRoom
        }
        else
        {
            return CalculateBuyRoomPrice(RoomType.Kitchen)/2; //Kitchen
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
