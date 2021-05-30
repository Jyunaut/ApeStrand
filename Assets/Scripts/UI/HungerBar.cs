using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    [SerializeField] private Player.Hunger _hunger;
    private Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        _image.fillAmount = _hunger.CurrentHunger / _hunger.MaxHunger;
    }
}
