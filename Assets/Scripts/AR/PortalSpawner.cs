using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;  // <-- Added for UI input check
using System.Collections.Generic;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portalPrefab;
    public ARPlaneManager arPlaneManager; // Assign this in the Inspector

    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool hasSpawnedPortal = false;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
#if UNITY_EDITOR
        // Use mouse click for testing in the Unity Editor
        if (Input.GetMouseButtonDown(0))
        {
            // Ignore clicks over UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePosition = Input.mousePosition;

            if (!hasSpawnedPortal && raycastManager.Raycast(mousePosition, hits, TrackableType.PlaneWithinPolygon))
            {
                SpawnPortal(hits[0].pose);
            }
        }
#else
        // Use touch input on mobile devices
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        // Ignore touches over UI
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;

        if (!hasSpawnedPortal && raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            SpawnPortal(hits[0].pose);
        }
#endif
    }

    private void SpawnPortal(Pose hitPose)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        Quaternion portalRotation = Quaternion.LookRotation(-cameraForward.normalized);

        Vector3 offsetPosition = hitPose.position + portalRotation * Vector3.forward * 0.05f;

        Instantiate(portalPrefab, offsetPosition, portalRotation);
        hasSpawnedPortal = true;

        DisablePlanes();
    }

    private void DisablePlanes()
    {
        if (arPlaneManager != null)
        {
            arPlaneManager.enabled = false;

            foreach (ARPlane plane in arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
    }
}
