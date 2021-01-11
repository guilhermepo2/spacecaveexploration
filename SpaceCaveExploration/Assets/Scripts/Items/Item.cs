using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    private Vector3 OriginalPosition;

    public enum EPossibleItems {
        Health,
        Chest,
        RedJar,
        GreenJar,
        BlueJar,
        Eskar
    }
    public EPossibleItems ItemType;


    private void Start() {
        OriginalPosition = transform.position;
    }

    private void Update() {
        transform.position = new Vector3(
            OriginalPosition.x,
            OriginalPosition.y + Mathf.Sin(Time.time / 10.0f) / 10.0f,
            OriginalPosition.z
            );
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            SoundManager.instance.PlayEffect(SoundBank.instance.PickUpItem);
            collision.GetComponent<PlayerController>().GiveItem(ItemType);
            Destroy(this.gameObject);
        }
    }
}
