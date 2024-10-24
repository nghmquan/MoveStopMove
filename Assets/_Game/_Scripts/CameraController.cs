using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera properties")]
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private Vector3 cameraRotation;

    [Header("Target")]
    [SerializeField] private GameObject player;

    void LateUpdate()
    {
        if (cam != null)
        {
            FollowTarget(cam);
        }
    }

    private void FollowTarget(Camera _camera)
    {
        _camera.transform.position = new Vector3(
            player.transform.position.x + cameraPosition.x,
            player.transform.position.y + cameraPosition.y,
            player.transform.position.z + cameraPosition.z);

        _camera.transform.rotation = Quaternion.Euler(
            player.transform.rotation.x + cameraRotation.x,
            player.transform.rotation.y + cameraRotation.y,
            player.transform.rotation.z + cameraRotation.z);
    }
}
