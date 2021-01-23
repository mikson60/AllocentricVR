using UnityEngine;

public class WebCameraController : MonoBehaviour
{
    public enum CameraType { OBS, VivePro, e2eApple, DroidCam }

    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private CameraType cameraType;


    void Start()
    {
        string validDeviceName = "";

        foreach(var device in WebCamTexture.devices)
        {
            Debug.Log($"---={device.name}=---");
        }

        if (targetRenderer != null)
        {
            switch (cameraType)
            {
                case CameraType.DroidCam:
                    validDeviceName = "DroidCam Source 3";
                    break;
                case CameraType.e2eApple:
                    validDeviceName = "e2eSoft iVCam";
                    break;

                case CameraType.OBS:
                    validDeviceName = "OBS-Camera";
                    break;
                case CameraType.VivePro:
                    validDeviceName = "VIVE Pro Multimedia Camera";
                    break;
            }

            WebCamTexture webcamTexture = new WebCamTexture(validDeviceName);
            targetRenderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }
    }
}
