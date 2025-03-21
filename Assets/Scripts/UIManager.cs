using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image BatteryLifeImage;

    private Player player;
    private GameObject PlayerGB;

    private void Start()
    {
        PlayerGB = GameObject.FindGameObjectWithTag("Player");
        player = PlayerGB.GetComponent<Player>();
    }

    private void Update()
    {
        BatteryLifeImage.fillAmount = player.batteryLife / 100f;
    }
}
