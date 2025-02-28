using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GunConfig", menuName = "Configs/GunConfig")]
public class GunConfig : ScriptableObject
{
    public string gunName;
    public float shootingForce;
    public float useTime;
    public float damageAmt;
}
