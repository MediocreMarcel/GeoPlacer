using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using UnityEngine.Events;

enum State
{
    Idle,
    StartPlaced,
    Paused
}

public abstract class Placeable : MonoBehaviour, IMixedRealityPointerHandler
{
    public UnityEvent onFinishedPlacing;
    public GameObject previewOutlinePrefab;

    protected Vector3 startPosition;
    protected GameObject previewObject;

    private GameObject previewOutline;

    private const float SPHERE_SIZE = 0.03f;

    private State state = State.Idle;
    private bool isPaused = false;

    [SerializeField] private GameObject startMarker;
    [SerializeField] private GameObject endMarker;
    [SerializeField] private GameObject startSphere;
    [SerializeField] private GameObject endSphere;

    public abstract void GeneratePreview(Vector3 centerPosition, Vector3 scale);

    private void OnEnable()
    {
        // Instruct Input System that we would like to receive all input events of type IMixedRealityGestureHandler
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);

        //create simple sphere if markers or sphere objects are null
        if (this.startMarker == null || this.startSphere == null)
        {
            this.startMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            this.startSphere = this.startMarker;
            this.startSphere.transform.localScale = new Vector3(Placeable.SPHERE_SIZE, Placeable.SPHERE_SIZE, Placeable.SPHERE_SIZE);
            this.startSphere.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
        if (this.endMarker == null || this.endSphere == null)
        {
            this.endMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            this.endSphere = this.endMarker;
            this.endSphere.transform.localScale = new Vector3(Placeable.SPHERE_SIZE, Placeable.SPHERE_SIZE, Placeable.SPHERE_SIZE);
            this.endSphere.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }

        this.startMarker.SetActive(true);
    }

    private void OnDisable()
    {
        // Instruct Input System to disregard all input events of type IMixedRealityGestureHandler
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityPointerHandler>(this);

        this.startPosition = Vector3.zero;
        this.previewObject = null;
        this.previewOutline = null;
        this.state = State.Idle;
        this.startMarker.SetActive(false);
        this.endMarker.SetActive(false);
    }

    public void SetPausePlacer(bool paused)
    {
        this.isPaused = paused;
        this.startMarker.SetActive(!paused);
        this.endMarker.SetActive(!paused);
        this.previewOutline.SetActive(!paused);
        if (paused)
        {
            Destroy(this.previewObject);
        }

    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        //Debug.Log($"Event: {eventData.ToString()}, Position: {eventData.Pointer.Position}");
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        //Debug.Log($"Event: {eventData.ToString()}, Position: {eventData.Pointer.Position}");
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        //Debug.Log($"Event: {eventData.ToString()}, Position: {eventData.Pointer.Position}");
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        if (!this.isPaused)
        {
            if (state == State.Idle)
            {
                this.startPosition = this.startMarker.transform.position;
                this.state = State.StartPlaced;
            }
            else if (state == State.StartPlaced && this.previewObject != null)
            {
                this.state = State.Idle;
                this.previewObject.GetComponent<Renderer>().material.color = new Color(0.066f, 0.122f, 0.412f);
                this.startMarker.SetActive(false);
                this.endMarker.SetActive(false);
                this.startPosition = Vector3.zero;
                this.previewObject = null;
                this.previewOutline.SetActive(false);
                this.onFinishedPlacing.Invoke();
            }
        }

    }

    void Update()
    {
        MixedRealityPose pose;
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Both, out pose) && !this.isPaused)
        {
            if (state == State.Idle)
            {
                this.UpdateStartSphere(pose);
            }
            else if (state == State.StartPlaced)
            {
                Vector3 previewCenterPosition = this.CalucaltePrewiewCenterPosition(pose);
                Vector3 previewScale = this.CalculatePreviewScale(pose);
                this.GeneratePreview(previewCenterPosition, previewScale);
                this.DrawPreviewOutline(previewCenterPosition, previewScale);
                this.UpdateEndSphere(pose);
            }
        }
        else
        {
            this.startMarker.SetActive(false);
            this.endMarker.SetActive(false);
        }
    }

    protected void SetPreviewMaterial(Material material)
    {
        MaterialUtils.SetupBlendMode(material, MaterialUtils.BlendMode.Transparent);
        material.color = new Color(0.52f, 0.52f, 0.52f, 0.7f);
    }

    private void DrawPreviewOutline(Vector3 position, Vector3 scale)
    {
        Debug.Log(this.previewOutline);
        if (this.previewOutline == null)
        {
            this.previewOutline = Instantiate(this.previewOutlinePrefab, position, Quaternion.identity).gameObject;
        }
        if (!this.previewOutline.activeSelf)
        {
            this.previewOutline.SetActive(true);
        }

        this.previewOutline.transform.position = position;
        this.previewOutline.transform.localScale = scale;

    }

    private Vector3 CalculatePreviewScale(MixedRealityPose pose)
    {
        float scaleX = Mathf.Abs(this.startPosition.x - pose.Position.x);
        float scaleY = Mathf.Abs(this.startPosition.y - pose.Position.y);
        float scaleZ = Mathf.Abs(this.startPosition.z - pose.Position.z);
        return new Vector3(scaleX, scaleY, scaleZ);
    }

    private Vector3 CalucaltePrewiewCenterPosition(MixedRealityPose pose)
    {
        return (this.startPosition + pose.Position) / 2;
    }



    private void UpdateStartSphere(MixedRealityPose pose)
    {
        if (this.startSphere == null)
        {
            this.startSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            this.startSphere.transform.localScale = new Vector3(Placeable.SPHERE_SIZE, Placeable.SPHERE_SIZE, Placeable.SPHERE_SIZE);
            this.startSphere.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }

        if (this.startMarker.activeSelf == false)
        {
            this.startMarker.SetActive(true);
        }

        this.startMarker.transform.localPosition = pose.Position;
    }

    private void UpdateEndSphere(MixedRealityPose pose)
    {
        if (this.endSphere == null)
        {
            this.endSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            this.endSphere.transform.localScale = new Vector3(Placeable.SPHERE_SIZE, Placeable.SPHERE_SIZE, Placeable.SPHERE_SIZE);
            this.endSphere.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }

        if (this.endMarker.activeSelf == false)
        {
            this.endMarker.SetActive(true);
        }

        this.endMarker.transform.localPosition = pose.Position;
    }

}
