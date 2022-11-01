using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ü�� ������ ���� ������Ʈ���� ���� ���븦 ����
// ü��, ����� �޾Ƶ��̱�, ��� ���, ��� �̺�Ʈ�� 

public class LivingEntity : MonoBehaviour, IDamageble
{
    public float startingHealth = 100f; // ���� ü��
    public float health { get; protected set; } // ���� ü��
    public bool dead { get; protected set; } // ��� ����
    public event Action onDeath; // ��� �� �ߵ��� �̺�Ʈ

    // ����ü�� Ȱ��ȭ�� �� ���¸� ����
    protected virtual void OnEnable()
    {
        // ����Ϲ� ���� ���·� ����
        dead = false;
        // ü���� ���� ü������ �ʱ�ȭ
        health = startingHealth;
    }

    // ������� �Դ� ���
    public virtual void OnDamage(float damage, Vector3 hitpoint, Vector3 hitNormal)
    {
        // �������ŭ ü�� ����
        health -= damage;

        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // ü���� ȸ���ϴ� ���
    public virtual void RestorHealth(float newHealth)
    {
        if (dead)
        {
            // �̹� ����� ��� ü���� ȸ���� �� ����
            return;
        }

        // ü�� �߰�
        health += newHealth;
    }

    // ��� ó��
    public virtual void Die()
    {
        // onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
        if (onDeath != null)
        {
            onDeath();
        }

        // ��� ���¸� ������ ����
        dead = true;
    }

    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
