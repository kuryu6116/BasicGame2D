using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableAspect : MonoBehaviour
{
    private Camera cam;
    // 画像のサイズ
    private float width = 1280f;
    private float height = 720f;
    // 画像のPixel Per Unit(長さの単位)
    private float pixelPerUnit = 100f;

    void Awake()
    {
        // カメラコンポーネントを取得します
        cam = GetComponent<Camera>();
        // カメラのorthographicSizeを設定
        cam.orthographicSize = height / 2f / pixelPerUnit;

        float bgScale = height / Screen.height;
        // viewport rectの幅
        float camWidth = width / (Screen.width * bgScale);
        // viewportRectを設定
        cam.rect = new Rect((1f - camWidth) / 2f, 0f, camWidth, 1f);
    }
}