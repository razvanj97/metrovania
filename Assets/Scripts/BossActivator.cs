using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject bossToActivate;
    // Start is called before the first frame update
   private void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "Player") {
            bossToActivate.SetActive(true);
            gameObject.SetActive(false);
        }
   }
}
