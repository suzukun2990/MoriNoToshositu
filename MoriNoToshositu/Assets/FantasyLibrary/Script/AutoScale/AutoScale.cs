
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AutoScale : UdonSharpBehaviour
{
    public float cycleTime = 0.6f; // 周期
    public float maxScaleRate = 1.0f; // 最大倍率
    public float minScaleRate = 0.95f; // 最小倍率
    public bool randomOffset = false; // 初期サイズをランダムにするかどうか

    private float scaleRate = 0.02f; // 位置補正倍率
    float initialScaleY, initialPositionY, startTime, offset;

    // Start is called before the first frame update
    void Start()
    {
        initialScaleY = transform.localScale.y;
        initialPositionY = transform.localPosition.y;
        startTime = Time.time;

        // 初期サイズのオフセット
        offset = randomOffset ? UnityEngine.Random.Range(0f, 2 * UnityEngine.Mathf.PI) : 0f;
    }

    // Update is called once per frame
    void Update()
    {

        // 大きさ計算
        float newLength = initialScaleY * minScaleRate + initialScaleY
            * (1.0f - UnityEngine.Mathf.Sin(offset + (startTime - Time.time) * 2 * UnityEngine.Mathf.PI / cycleTime))
            * (maxScaleRate - minScaleRate) / 2.0f;

        // サイズ変更
        transform.localScale = new Vector3(
            transform.localScale.x,
            newLength,
            transform.localScale.z
        );

        // 位置補正
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            initialPositionY + (newLength - initialScaleY) * 0.5f * scaleRate,
            transform.localPosition.z
        );
    }

}
