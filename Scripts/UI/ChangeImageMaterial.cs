using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AC
{
    public class ChangeImageMaterial : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Material material;
    
        public void ChangeMaterial()
        {
            image.material = material;
        }
    }
}