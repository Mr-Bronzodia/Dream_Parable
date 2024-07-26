using UnityEngine;

public class PlayArea : MonoBehaviour
{
    private readonly float PLAY_AREA_MESH_DEFAULT_SIZE = 10f;

    private Bounds _playAreaBounds;

    [SerializeField] private Vector2 _playAreaSize;

    public Bounds Bounds { get { return _playAreaBounds; } }

    private void Awake()
    {
        _playAreaBounds = new Bounds(transform.position, new Vector3(_playAreaSize.x, 1f, _playAreaSize.y));
    }

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(_playAreaSize.x / PLAY_AREA_MESH_DEFAULT_SIZE, 1f, _playAreaSize.y / PLAY_AREA_MESH_DEFAULT_SIZE);
    }

    private void OnDrawGizmos()
    {
        if (_playAreaBounds == null)
            return;

        Gizmos.DrawCube(_playAreaBounds.center, _playAreaBounds.size);
    }
}
