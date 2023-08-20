using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake() {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    public void OnEnable()
    {
        CharacterEvents.CharacterDamaged += CharacterTookDamage;
        CharacterEvents.CharacterHealed += CharacterHealth;
    }

    public void OnDisable()
    {
        CharacterEvents.CharacterDamaged -= CharacterTookDamage;
        CharacterEvents.CharacterHealed -= CharacterHealth;
    }

    private void CharacterTookDamage(GameObject character, float damageReceive)
    {
        Vector3 spawnPoint = Camera.main.WorldToScreenPoint(character.transform.position) + new Vector3(0,1,0);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPoint, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceive.ToString();
    }

    private void CharacterHealth(GameObject character, float healthReceive)
    {
        Vector3 spawnPoint = Camera.main.WorldToScreenPoint(character.transform.position)+ new Vector3(0,1,0);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPoint, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthReceive.ToString();
    }
}
