using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IHittable
{
    bool CanBeHit { get; set; }
    public void Hit() { }
}
