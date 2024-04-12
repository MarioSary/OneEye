using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class CameraControllTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;

    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (customInspectorObjects.panCameraOnContact)
            {
                //pan the camera
                CameraManager.instance.PanCameraOnContact(customInspectorObjects.panDistance,
                    customInspectorObjects.panTime,
                    customInspectorObjects.panDirection,
                    false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - col.bounds.center).normalized;
            if (customInspectorObjects.swapCameras && customInspectorObjects.cameraOnLeft != null && customInspectorObjects.cameraOnRight != null)
            {
                //swap camera
                CameraManager.instance.SwapCamera(customInspectorObjects.cameraOnLeft, customInspectorObjects.cameraOnRight, exitDirection);
            }
            if (customInspectorObjects.panCameraOnContact)
            {
                //pan the camera
                CameraManager.instance.PanCameraOnContact(customInspectorObjects.panDistance,
                    customInspectorObjects.panTime,
                    customInspectorObjects.panDirection,
                    true);
            }
        }
    }
}

[System.Serializable]
public class CustomInspectorObjects
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    public CinemachineVirtualCamera cameraOnLeft;
    public CinemachineVirtualCamera cameraOnRight;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;

}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}


    /*#if UNITY_STANDALONE_WIN

[CustomEditor(typeof(CameraControllTrigger))]
public class MyScriptEditor: Editor
{
    private CameraControllTrigger cameraControllTrigger;

    private void OnEnable()
    {
        cameraControllTrigger = (CameraControllTrigger) target;
    }

    public void onInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (cameraControllTrigger.customInspectorObjects.swapCameras)
        {
            cameraControllTrigger.customInspectorObjects.cameraOnLeft = EditorGUILayout.ObjectField("Camera on Left",
                cameraControllTrigger.customInspectorObjects.cameraOnLeft,
                typeof(CinemachineVirtualCamera),
                true) as CinemachineVirtualCamera;
            cameraControllTrigger.customInspectorObjects.cameraOnRight = EditorGUILayout.ObjectField("Camera on Right",
                cameraControllTrigger.customInspectorObjects.cameraOnRight,
                typeof(CinemachineVirtualCamera),
                true) as CinemachineVirtualCamera;
        }

        if (cameraControllTrigger.customInspectorObjects.panCameraOnContact)
        {
            cameraControllTrigger.customInspectorObjects.panDirection =
                (PanDirection) EditorGUILayout.EnumPopup("Camera Pan Direction",
                    cameraControllTrigger.customInspectorObjects.panDirection);

            cameraControllTrigger.customInspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance",
                cameraControllTrigger.customInspectorObjects.panDistance);
            cameraControllTrigger.customInspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time",
                cameraControllTrigger.customInspectorObjects.panTime);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraControllTrigger);
        }
        
    }
}

#endif*/