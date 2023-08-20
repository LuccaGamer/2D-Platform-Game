using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public UnityEngine.Vector3 moveSpeed = new UnityEngine.Vector3(0, 75, 0);

    TextMeshProUGUI textMeshPro;
    RectTransform textTransform;

    public float timeShow = 1f;
    private float lastShown;
    private Color textColor;

    private void Awake() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textTransform = GetComponent<RectTransform>();
        textColor = textMeshPro.color;
    }

    private void Update() {
        transform.position += moveSpeed * Time.deltaTime;
        lastShown += Time.deltaTime;


        if (lastShown < timeShow)
        {
        float newAlpha = textColor.a * (1 - lastShown/timeShow);
            textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, newAlpha);
        }
        else{
            Destroy(gameObject);
        }
    }
}
