using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public enum ModificationType
{
    Eyes,
    RightHand,
    LeftHand,
    Wheels,
    Shaft,
}

namespace Assets.Scripts.Player
{
    [Serializable]
    public struct Interchangable
    {
       //[SerializeField] public Vector3 position;
       //public Quaternion rotation;
       public bool isEquipped;
       public string prefabReference;
       public ModificationType type;


        public ModificationType Type { get => type; set => type = value; }

        public void changePart(GameObject reference) {
            //position = reference.transform.position;
            //rotation = reference.transform.rotation;
            prefabReference = reference.name.Replace("(Clone)", "");
            isEquipped = true;
        }

    }
}
