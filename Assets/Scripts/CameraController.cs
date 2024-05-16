using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    public bool followPlayer = true;

    private void Awake()
    {
        if (!target)
        {
            target = FindObjectOfType<Character>().transform;
        }
    }

    private void Update()
    {
        if(!followPlayer) return;
        Vector3 position = target.position;
        position.z = -10.0F;

        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
