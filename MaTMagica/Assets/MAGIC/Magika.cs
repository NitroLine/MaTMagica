using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MAGIC
{
    public class Magika
    {
        public GameObject Obj;
        public int Power;

        public Magika(GameObject obj, int power = 1)
        {
            Obj = obj;
            Power = power;
        }
    }
}
