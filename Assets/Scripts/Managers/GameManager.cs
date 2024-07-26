using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayArea _playArea;
    [SerializeField] private CameraRig _cameraRig;

    public static GameManager Instance { get; private set; }
    public PlayArea PlayArea { get { return _playArea; } }

    public System.Action<bool> OnPauseStateChanged;

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

    private void Start()
    {
        SelectionManager.Instance.OnAgentSelected += AgentSelected;
        SelectionManager.Instance.OnAgentDeSelected += DeselectAgent;
    }

    private void DeselectAgent()
    {
        OnPauseStateChanged?.Invoke(false);

        _cameraRig.MoveRigToDefaultPosition(); 
    }

    private void AgentSelected(Agent agent)
    {
        OnPauseStateChanged?.Invoke(true);
        
        Vector3 agentFramingPoint = agent.transform.position + (agent.transform.forward * 5f) + new Vector3(0f, 1f, 0f);
        Quaternion lookAtAgentRotation = Quaternion.LookRotation(agent.transform.TransformDirection(Vector3.back), Vector3.up);

        _cameraRig.MoveRigTo(agentFramingPoint, lookAtAgentRotation);
    }


    private void OnDisable()
    {
        SelectionManager.Instance.OnAgentSelected -= AgentSelected;
        SelectionManager.Instance.OnAgentDeSelected -= DeselectAgent;
    }
}
