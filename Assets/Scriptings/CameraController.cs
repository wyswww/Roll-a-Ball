using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;      // 要跟随的目标（在 Inspector 里拖入）
    private Vector3 offset;        // 记录摄像机与球的固定距离

    void Start()
    {
        // 记录"摄像机位置 - 球位置"的差值
        offset = transform.position - player.transform.position;
    }

    // LateUpdate：在球动完之后的每帧更新
    void LateUpdate()
    {
        // 让摄像机 = 球当前位置 + 固定差值，实现跟随
        transform.position = player.transform.position + offset;
    }
}