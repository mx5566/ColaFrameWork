using UnityEngine;
using System;

public class Bonus : MonoBehaviour {

    public Action<Collider2D, Bonus> OnTriggerEnter2DLua;

    //when colliding with another object, if another object is 'Player', sending command to the 'Player'
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (OnTriggerEnter2DLua != null)
        {
            OnTriggerEnter2DLua(collision, this);
        }
        if (collision.CompareTag("Player")) 
        {
            if (PlayerShooting.instance.weaponPower < PlayerShooting.instance.maxweaponPower)
            {
                PlayerShooting.instance.weaponPower++;
            }
            Destroy(gameObject);
        }
    }
}
