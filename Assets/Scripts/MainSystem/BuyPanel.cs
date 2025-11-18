using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    public string headerString;
    public TMP_Text headerText;
    public TMP_Text priceText;
    public Button bedroomButton;
    public Button fitnessButton;
    public Button mailRoomButton;
    public Button KitchenButton;
    public Button LivingRoomButton;
    public Button sellButton;
    public Button upgradeButton;
    public Button buyButton;

    public Room room;
    public RoomType selectedType;
    public int price;
    public string pricePrefix = "Price: ";

    public Camera mainCamera;
    private InputAction leftClickAction;

    public Canvas canvas;
    public RectTransform panel;



    #region Singleton
    private static BuyPanel instance;
    public static BuyPanel Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    #endregion

    
    public void Initialize(Room room)
    {
        priceText.text = string.Empty;
        this.room = room;

        gameObject.SetActive(true);
        //ShowPanel();

        UpdateButton();
        UpdataHeaderText();

        //button.onClick.RemoveAllListeners();
        //button.onClick.AddListener(() => OnButtonClicked());
    }
    private void Start()
    {
        leftClickAction = InputSystem.actions.FindAction("LeftClick");
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        gameObject.SetActive(false);

    }
    private void Update()
    {
        if (leftClickAction.triggered)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                OnOuterClicked();
            }
        }
    }
    public void UpdataHeaderText()
    {
        if (room.roomType == RoomType.Empty)
        {
            // ซื้อ/สร้าง ห้องใหม่
            headerString = "Buy " + room.roomName;
        }
        else if (room.roomType == RoomType.Bedroom)
        {
            // Bedroom => แสดง tier ถัดไป
            headerString = $"Upgrade Bedroom to {(RoomTier)((int)room.roomTier + 1)}";
            priceText.text = pricePrefix + room.CalculateBuyRoomPrice(RoomType.Bedroom).ToString();
        }
        else
        {
            // ห้องซื้อแล้ว
            headerString = room.roomType.ToString();
        }
        headerText.text = headerString;
    }
    public void UpdateButton()
    {
        if (room.roomType == RoomType.Empty)
        {

            bedroomButton.gameObject.SetActive(true);
            fitnessButton.gameObject.SetActive(true);
            mailRoomButton.gameObject.SetActive(true);
            KitchenButton.gameObject.SetActive(true);
            LivingRoomButton.gameObject.SetActive(true);

            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
            upgradeButton.gameObject.SetActive(false);
        }
        else if(room.roomType == RoomType.Bedroom && room.roomTier != RoomTier.SuperExtra)
        {
            upgradeButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(true);

            buyButton.gameObject.SetActive(false);
            bedroomButton.gameObject.SetActive(false);
            fitnessButton.gameObject.SetActive(false);
            mailRoomButton.gameObject.SetActive(false);
            KitchenButton.gameObject.SetActive(false);
            LivingRoomButton.gameObject.SetActive(false);
        }
        else
        {
            sellButton.gameObject.SetActive(true);

            upgradeButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            bedroomButton.gameObject.SetActive(false);
            fitnessButton.gameObject.SetActive(false);
            mailRoomButton.gameObject.SetActive(false);
            KitchenButton.gameObject.SetActive(false);
            LivingRoomButton.gameObject.SetActive(false);
        }
    }
    public void ShowPanel()
    {
        Vector2 pos = Mouse.current.position.value; // ตำแหน่งบนจอ

        // แปลง screen → canvas local position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                pos,
                canvas.worldCamera,
                out Vector2 localPoint
            );

        // เซตตำแหน่ง panel
        panel.anchoredPosition = localPoint;

        UpdateButton();
        UpdataHeaderText();
    }
    public void OnOuterClicked()
    {
        gameObject.SetActive(false);
        //button.onClick.RemoveAllListeners();
    }
    public void OnBedroomButtonClicked()
    {
        // เปิดปุ่มซื้อ
        // แสดงราคา
        buyButton.gameObject.SetActive(true);
        priceText.text = pricePrefix + room.CalculateBuyRoomPrice(RoomType.Bedroom).ToString();
        selectedType = RoomType.Bedroom;
    }
    
    public void OnFitnessButtonClicked()
    {
        buyButton.gameObject.SetActive(true);
        priceText.text = pricePrefix + room.CalculateBuyRoomPrice(RoomType.Fitness).ToString();
        selectedType = RoomType.Fitness;
    }
    public void OnMailRoomButtonClicked()
    {
        buyButton.gameObject.SetActive(true);
        priceText.text = pricePrefix + room.CalculateBuyRoomPrice(RoomType.MailRoom).ToString();
        selectedType = RoomType.MailRoom;
    }
    public void OnLivingRoomButtonClicked()
    {
        buyButton.gameObject.SetActive(true);
        priceText.text = pricePrefix + room.CalculateBuyRoomPrice(RoomType.LivingRoom).ToString();
        selectedType = RoomType.LivingRoom;
    }
    public void OnKitchenButtonClicked()
    {
        buyButton.gameObject.SetActive(true);
        priceText.text = pricePrefix + room.CalculateBuyRoomPrice(RoomType.Kitchen).ToString();
        selectedType = RoomType.Kitchen;
    }
    public void OnRoomButtonClicked(RoomType type)
    {
        buyButton.gameObject.SetActive(true);
        priceText.text = pricePrefix + room.CalculateBuyRoomPrice(RoomType.Bedroom).ToString();
        selectedType = type;
    }
    public void OnSellButtonClicked()
    {
        room.SellRoom();
        selectedType = RoomType.Empty;
        UpdateButton();
        UpdataHeaderText();
    }
    public void OnBuyButtonClicked()
    {
        room.BuyRoom(selectedType);
        selectedType = RoomType.Empty;
        UpdateButton();
        UpdataHeaderText();
    }

    public void OnUpgradeButtonClicked()
    {
        room.UpgradeRoom();
        UpdateButton();
        UpdataHeaderText();
    }
}
