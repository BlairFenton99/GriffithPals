using UnityEngine;
using System.Collections;
using TMPro;

public class PartRebuild : MonoBehaviour
{
    public Vector3 snapPosition;
    public Vector3 snapRotationEuler;
    public float snapPositionThreshold = 0.1f;
    public float snapRotationThreshold = 10f;

    public Material goldMaterial;
    public TextMeshProUGUI snapPromptText;

    private Material originalMaterial;
    private Renderer partRenderer;

    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 dragOffset;
    private float dragDistance;

    private bool isSnapped = false;

    public int blinkCount = 6;
    public float blinkDuration = 0.2f;

    private float rotationStep = 15f;

    void Start()
    {
        mainCamera = Camera.main;
        partRenderer = GetComponent<Renderer>();
        if (partRenderer != null)
            originalMaterial = partRenderer.material;

        if (snapPromptText != null)
            snapPromptText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isSnapped)
            return;

        HandleDrag();
        HandleRotate();
        
        if (!isDragging)
            TrySnap();
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                isDragging = true;
                dragDistance = Vector3.Distance(mainCamera.transform.position, transform.position);
                Vector3 worldPoint = ray.GetPoint(dragDistance);
                dragOffset = transform.position - worldPoint;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPos = ray.GetPoint(dragDistance) + dragOffset;
            transform.position = targetPos;
        }
    }

    void HandleRotate()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (!isDragging) return;

        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            transform.Rotate(Vector3.forward, scroll * rotationStep, Space.Self);
        }
#elif UNITY_ANDROID || UNITY_IOS
        HandleTouchRotation();
#endif
    }

    void HandleTouchRotation()
    {
        if (!isDragging || Input.touchCount != 2)
            return;

        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
        Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

        float prevAngle = Mathf.Atan2(prevTouch1.y - prevTouch0.y, prevTouch1.x - prevTouch0.x) * Mathf.Rad2Deg;
        float currentAngle = Mathf.Atan2(touch1.position.y - touch0.position.y, touch1.position.x - touch0.position.x) * Mathf.Rad2Deg;

        float angleDelta = Mathf.DeltaAngle(prevAngle, currentAngle);

        transform.Rotate(Vector3.forward, angleDelta, Space.Self);
    }

    void TrySnap()
    {
        Vector3 currentPos = transform.position;
        Vector3 currentRot = transform.rotation.eulerAngles;

        bool posClose = Mathf.Abs(currentPos.x - snapPosition.x) <= snapPositionThreshold &&
                        Mathf.Abs(currentPos.y - snapPosition.y) <= snapPositionThreshold &&
                        Mathf.Abs(currentPos.z - snapPosition.z) <= snapPositionThreshold;

        currentRot = NormalizeAngles(currentRot);
        Vector3 targetRot = NormalizeAngles(snapRotationEuler);

        bool rotClose = Mathf.Abs(Mathf.DeltaAngle(currentRot.x, targetRot.x)) <= snapRotationThreshold &&
                        Mathf.Abs(Mathf.DeltaAngle(currentRot.y, targetRot.y)) <= snapRotationThreshold &&
                        Mathf.Abs(Mathf.DeltaAngle(currentRot.z, targetRot.z)) <= snapRotationThreshold;

        if (posClose && rotClose)
        {
            transform.position = snapPosition;
            transform.rotation = Quaternion.Euler(snapRotationEuler);
            isSnapped = true;

            StartCoroutine(BlinkGold());
            ShowSnapPrompt();

            GetComponent<Collider>().enabled = false;
            this.enabled = false;

            StatueRebuildManager.Instance.PartSnapped();
        }
    }

    Vector3 NormalizeAngles(Vector3 angles)
    {
        angles.x = NormalizeAngle(angles.x);
        angles.y = NormalizeAngle(angles.y);
        angles.z = NormalizeAngle(angles.z);
        return angles;
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }

    IEnumerator BlinkGold()
    {
        if (partRenderer == null || goldMaterial == null)
            yield break;

        for (int i = 0; i < blinkCount; i++)
        {
            partRenderer.material = (i % 2 == 0) ? goldMaterial : originalMaterial;
            yield return new WaitForSeconds(blinkDuration);
        }
        partRenderer.material = goldMaterial;
    }

    void ShowSnapPrompt()
    {
        if (snapPromptText != null)
        {
            snapPromptText.text = "Correct!";
            snapPromptText.gameObject.SetActive(true);
            StartCoroutine(HideSnapPromptAfterDelay(2f));
        }
    }

    IEnumerator HideSnapPromptAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (snapPromptText != null)
            snapPromptText.gameObject.SetActive(false);
    }
}






