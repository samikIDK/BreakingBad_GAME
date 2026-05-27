using UnityEngine;
using Unity.Cinemachine;

public class CameraTarget : MonoBehaviour
{
    private CinemachineCamera cam;

    void Start()
    {
        cam = GetComponent<CinemachineCamera>();
        FindPlayer();
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            cam.Target.TrackingTarget = player.transform;
        }
    }
}