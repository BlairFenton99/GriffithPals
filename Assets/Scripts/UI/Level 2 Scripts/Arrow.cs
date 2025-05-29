using System.Collections;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class GPSGuidanceArrow : MonoBehaviour
{
    [Header("Target GPS Location (Set in Inspector)")]
    public double targetLatitude;
    public double targetLongitude;

    [Header("UI Arrow")]
    public RectTransform arrow;

    [Header("Settings")]
    public float smoothSpeed = 10f;
    public bool useCompass = true;

    private bool gpsReady = false;

    void Start()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#endif
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("Location services not enabled by user.");
            yield break;
        }

        Input.location.Start();
        if (useCompass)
            Input.compass.enabled = true;

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (Input.location.status != LocationServiceStatus.Running)
        {
            Debug.LogError("GPS failed to start. Status: " + Input.location.status);
            yield break;
        }

        gpsReady = true;
        Debug.Log("GPS started.");
    }

    void Update()
    {
        if (!gpsReady || arrow == null)
            return;

        double currentLat, currentLon;

#if UNITY_EDITOR
        Debug.LogWarning("GPS simulation in Editor. Arrow will not behave realistically.");
        return; // Disable GPS simulation — use device for actual testing
#else
        currentLat = Input.location.lastData.latitude;
        currentLon = Input.location.lastData.longitude;
#endif

        float bearingToTarget = CalculateBearing(currentLat, currentLon, targetLatitude, targetLongitude);

        if (useCompass)
        {
            float heading = Input.compass.trueHeading;
            float relativeBearing = bearingToTarget - heading;
            bearingToTarget = (relativeBearing + 360f) % 360f;
        }

        Quaternion targetRotation = Quaternion.Euler(0, 0, -bearingToTarget + 180f);
        arrow.rotation = Quaternion.Lerp(arrow.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }

    float CalculateBearing(double lat1, double lon1, double lat2, double lon2)
    {
        float degToRad = Mathf.Deg2Rad;
        float phi1 = (float)lat1 * degToRad;
        float phi2 = (float)lat2 * degToRad;
        float deltaLon = (float)(lon2 - lon1) * degToRad;

        float y = Mathf.Sin(deltaLon) * Mathf.Cos(phi2);
        float x = Mathf.Cos(phi1) * Mathf.Sin(phi2) -
                  Mathf.Sin(phi1) * Mathf.Cos(phi2) * Mathf.Cos(deltaLon);

        float bearing = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        return (bearing + 360f) % 360f;
    }
}
