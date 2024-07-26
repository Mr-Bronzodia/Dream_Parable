using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    private readonly float PLAY_AREA_DEFAULT_SIZE = 10f;

    [SerializeField] private Vector2 _playAreaSize;

    private Bounds _playAreaBounds;

    public Bounds Bounds { get { return _playAreaBounds; } }


    private void OnEnable()
    {
        _playAreaBounds = new Bounds(gameObject.transform.position, new Vector3(_playAreaSize.x, 1f, _playAreaSize.y));
    }

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(_playAreaSize.x / PLAY_AREA_DEFAULT_SIZE, 1f, _playAreaSize.y / PLAY_AREA_DEFAULT_SIZE);
    }

    private void OnDrawGizmos()
    {
        if (_playAreaBounds == null)
            return;

        Gizmos.DrawCube(_playAreaBounds.center, _playAreaBounds.size);
    }
}
