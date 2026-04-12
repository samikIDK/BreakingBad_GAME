using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider xpBar;
    public XPSystem playerXP;

    void Awake()
    {
        xpBar.minValue = 0;
        xpBar.maxValue = 1;
        xpBar.value = 0;
    }

    void Update()
    {
        xpBar.value = playerXP.GetXPProgress();
    }
}