using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShipType : MonoBehaviour
{
    public void setPlayerprefs(int type)
    {
        PlayerPrefs.SetInt("shipType", type);



    }
}
