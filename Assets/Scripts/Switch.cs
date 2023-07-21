using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private GameObject connection;

    [SerializeField] private bool on;
    [SerializeField] private bool oneWay; // TODO: Not sure if this is necessary

    private SpriteRenderer sr;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start() {
        sr.sprite = on ? onSprite : offSprite;
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // TODO: Audio sound upon switch click? Also should I check for player layer?
        if (!on) {
            connection.SetActive(false);
            on = true;
        } else if (!oneWay) {
            connection.SetActive(true);
            on = false;
        }

        sr.sprite = on ? onSprite : offSprite;
    }
}
