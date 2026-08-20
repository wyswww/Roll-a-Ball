
using UnityEngine;
using TMPro;                    // TextMeshPro 的命名空间（Unity 默认的 UI 文本类型）

public class PlayerController : MonoBehaviour
{
    // ---------- 可调参数（在 Inspector 里可直接改数值）----------
    public float speed = 10f;       // 移动力度，越大滚得越快
    public int winCount = 3;        // 收集满多少个算胜利
    public TMP_Text scoreText;      // 计分文本，Inspector 里拖入
    public TMP_Text winText;        // 胜利提示文本，Inspector 里拖入（可留空）

    private Rigidbody rb;           // 刚体组件引用
    private int count = 0;          // 已收集数量

    // Start：游戏开始时调用一次
    void Start()
    {
        rb = GetComponent<Rigidbody>();   // 获取挂在同一物体上的 Rigidbody
        count = 0;
        SetScoreText();                  // 先把分数显示出来

        if (winText != null)             // 先隐藏胜利提示
            winText.gameObject.SetActive(false);
    }

    // FixedUpdate：物理操作必须放这里，它按固定频率调用
    void FixedUpdate()
    {
        // 读取输入：A/D、W/S 或方向键，范围 -1 ~ 1
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 构造移动向量，只在 XZ 平面移动，y 保持 0
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        // 施加力让球滚动（AddForce 是"推"，球会越滚越快有惯性）
        rb.AddForce(movement * speed);
    }

    // OnTriggerEnter：两个碰撞体都勾了 Is Trigger 才会触发
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            Debug.Log("Score: " + count);   // ← 新增：打印到 Console
            SetScoreText();
        }
    }

    // 刷新分数，并判断是否胜利
    private void SetScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + count.ToString();

        if (count >= winCount && winText != null) {
            winText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);

        }
            // 显示"你赢了"
    }
}