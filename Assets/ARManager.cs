using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        PlaceIndicator();
    }

    #region 바닥에 프리팹 놓기
    public ARRaycastManager arRaycater;
    public GameObject[] spawnPrefab;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void PlacePrefab(int index)
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (arRaycater.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            Instantiate(spawnPrefab[index], hitPose.position, hitPose.rotation);
        }
    }

    #endregion

    #region 바닥 활성화

    public ARPlaneManager arPlane;

    public void ShowPlane(bool b)
    {
        foreach (var plane in arPlane.trackables)
            plane.gameObject.SetActive(b);
    }

    #endregion


    #region 바닥 표시기

    public Transform Indicator;
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();

    void PlaceIndicator()
    {
        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), indicatorHits, TrackableType.Planes);

        if (indicatorHits.Count > 0)
        {
            Indicator.position = indicatorHits[0].pose.position;
            Indicator.rotation = indicatorHits[0].pose.rotation;
        }
    }

    public void PlaceIndicatorPrefab(int index)
    {
        Pose hitPose = indicatorHits[0].pose;
        Instantiate(spawnPrefab[index], hitPose.position, hitPose.rotation);
    }

    #endregion
}
