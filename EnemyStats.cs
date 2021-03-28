using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillGod
{
    public class EnemyStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            setRigidbodyState(true);
            setColliderState(false);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            animator.Play("Damage_01");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GetComponentInChildren<Animator>().enabled = false;
                setRigidbodyState(false);
                setColliderState(true);
            }
        }

        #region Ragdoll Functions
        public void setRigidbodyState(bool state)
        {
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = state;
            }

            GetComponent<Rigidbody>().isKinematic = !state;
        }

        public void setColliderState(bool state)
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();

            foreach (Collider collider in colliders)
            {
                collider.enabled = state;
            }

            GetComponent<Collider>().enabled = !state;
        }
        #endregion

    }
}