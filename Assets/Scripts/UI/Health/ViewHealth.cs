using System;
using UnityEngine;

namespace UI.Health
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ViewHealth : MonoBehaviour, IViewHealth
    {
        private const string MaterialFloatName = "_Fill";

        private MeshRenderer _renderer;

        private MaterialPropertyBlock _material;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _material = new MaterialPropertyBlock();
        }

        public void Set(float maxHealth, float health)
        {
            if (maxHealth < 0)
                throw new ArgumentOutOfRangeException(nameof(maxHealth));
            
            if (health > maxHealth)
                throw new ArgumentOutOfRangeException(nameof(health));

            _material.SetFloat(MaterialFloatName, ConvertHealth(maxHealth, health));

            _renderer.SetPropertyBlock(_material);
        }

        private float ConvertHealth(float maxHealth, float health) => 1 - ((maxHealth - health) / maxHealth); // вроде считает правильно, но я хз по поводу скорости
    }
}
