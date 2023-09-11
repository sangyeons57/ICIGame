using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public interface IBaseLifeInfo
    {
        public int hp { get; set; }

        public void attacked(int damage);
        public void dead();
    }
}
