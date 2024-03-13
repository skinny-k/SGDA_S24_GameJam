using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPedestal : InteractObject
{
    public override void Interact()
    {
        GameManager.LoadScene("GameOver");
    }
}
