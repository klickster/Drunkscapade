using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Image _wakeUpBar;
    [Header("Keys")]
    [SerializeField] private TextMeshProUGUI _forwardText;
    [SerializeField] private TextMeshProUGUI _backwardsText;
    [SerializeField] private TextMeshProUGUI _leftText;
    [SerializeField] private TextMeshProUGUI _rightText;

    private void Start()
    {
        InputManager.Instance.OnKeyChanged.AddListener(UpdateKeys);
        UpdateKeys();
    }

    public void UpdateWakeUpBar(float value)
    {
        _wakeUpBar.fillAmount = value;
    }

    private void UpdateKeys()
    {
        _forwardText.text = InputManager.Instance.MoveForward.ToString();
        _backwardsText.text = InputManager.Instance.MoveBackward.ToString();
        _leftText.text = InputManager.Instance.MoveLeft.ToString();
        _rightText.text = InputManager.Instance.MoveRight.ToString();
    }
}
