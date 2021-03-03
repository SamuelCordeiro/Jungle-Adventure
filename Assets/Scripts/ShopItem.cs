using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string itemName;
    public int itemValue;
    public Sprite itemSprite;
    public Image objImage;
    public Text itemNameText;
    public Text itemValueText;
    public GameObject objPrefab;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        objImage.sprite = itemSprite;
        itemValueText.text = itemValue.ToString();
        itemNameText.text = itemName;
    }

    public void BuyItem()
    {
        if(GameController.current.coinScore >= itemValue)
        {
            GameController.current.coinScore -= itemValue;
            GameObject obj = Instantiate(objPrefab, player.weaponPoint.transform.position, player.weaponPoint.transform.rotation);
            obj.transform.SetParent(player.weaponPoint.transform);
            Destroy(player.weapon);
            player.weapon = obj;
        }
        else
        {
            Debug.Log("Não tem dinheiro");
        }

    }

}
