using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private GameObject connection;

    private SpriteRenderer sr;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start() {
        sr.sprite = onSprite;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        AudioManager.instance.Play("SwitchHit");
        // Would be good to generalize, but this version of the game doesn't need it
        connection.SetActive(false);

        sr.sprite = offSprite;
    }
}
