using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField] private LayerMask _selectionLayerMask;

    public static SelectionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        _mainCamera = Camera.main;
        InputManager.Instance.InputAction.Game.PlayerSelect.performed += PlayerSelect;
    }

    private void PlayerSelect(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Ray mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, _selectionLayerMask))
        {
            if (!hit.collider.gameObject.TryGetComponent<Agent>(out Agent agent))
                return;

            Debug.Log(agent.name);
            Debug.Log(agent.Health);
        }
    }

    private void OnDisable()
    {
        InputManager.Instance.InputAction.Game.PlayerSelect.performed -= PlayerSelect;
    }
}




