using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Image _wakeUpBar;

    public void UpdateWakeUpBar(float value)
    {
        _wakeUpBar.fillAmount = value;
    }
}
