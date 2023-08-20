using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float healAmount = 10f;
    public Vector3 spinSpeed = new Vector3 (0, 180, 0);

    public AudioSource heal;
    public float volume;

    private void Awake() {
        heal = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Damageable damageable = other.GetComponent<Damageable>();
        if(damageable)
        {   
            bool isHealed = damageable.Heal(healAmount);
            AudioSource.PlayClipAtPoint(heal.clip, gameObject.transform.position, volume);
            if (isHealed)
                {
                    Destroy(gameObject);
                }
        }
    }

    private void Update() {
        transform.eulerAngles += spinSpeed * Time.deltaTime;
    }
}
