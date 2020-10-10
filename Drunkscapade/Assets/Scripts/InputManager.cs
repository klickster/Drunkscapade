using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private KeyCode _moveForward;
    private KeyCode _moveBackward;
    private KeyCode _moveRight;
    private KeyCode _moveLeft;

    //[SerializeField] private KeyCode[] _movementKeys = { _moveForward, _moveBackward, _moveRight, _moveLeft };

    private KeyCode[] _keyCodes = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    private List<int> _pickedKeys = new List<int>();

    public bool IsMovingForward { get; private set; }
    public bool IsMovingBackward { get; private set; }
    public bool IsMovingRight { get; private set; }
    public bool IsMovingLeft { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        PopulatePickedKeys();
        GenerateRandomKeyCodes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateRandomKeyCodes();
        }

        IsMovingForward = Input.GetKey(_moveForward);
        IsMovingBackward = Input.GetKey(_moveBackward);
        IsMovingRight = Input.GetKey(_moveRight);
        IsMovingLeft = Input.GetKey(_moveLeft);
    }

    private void PopulatePickedKeys()
    {
        for (int n = 0; n < _keyCodes.Length; n++)
        {
            _pickedKeys.Add(n);
        }
    }

    private void GenerateRandomKeyCodes()
    {
        _moveForward = GenerateRandomKeyCode();
        _moveBackward = GenerateRandomKeyCode();
        _moveLeft = GenerateRandomKeyCode();
        _moveRight = GenerateRandomKeyCode();

        PopulatePickedKeys();
    }

    private KeyCode GenerateRandomKeyCode()
    {
        int randomKey = UnityEngine.Random.Range(0, _pickedKeys.Count - 1);
        KeyCode movementKey = _keyCodes[_pickedKeys[randomKey]];
        _pickedKeys.RemoveAt(randomKey);
        return movementKey;
    }
}
