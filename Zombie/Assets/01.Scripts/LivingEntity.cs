using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생명체로 동작할 게임 오브젝트들을 위한 뼈대를 제공
// 체력, 대미지 받아들이기, 사망 기능, 사망 이벤트를 

public class LivingEntity : MonoBehaviour, IDamageble
{
    public float startingHealth = 100f; // 시작 체력
    public float health { get; protected set; } // 현재 체력
    public bool dead { get; protected set; } // 사망 상태
    public event Action onDeath; // 사망 시 발동할 이벤트

    // 생명체가 활성화될 떄 상태를 리셋
    protected virtual void OnEnable()
    {
        // 사망하미 않은 상태로 시작
        dead = false;
        // 체력을 시작 체력으로 초기화
        health = startingHealth;
    }

    // 대미지를 입는 기능
    public virtual void OnDamage(float damage, Vector3 hitpoint, Vector3 hitNormal)
    {
        // 대미지만큼 체력 감소
        health -= damage;

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // 체력을 회복하는 기능
    public virtual void RestorHealth(float newHealth)
    {
        if (dead)
        {
            // 이미 사망한 경우 체력을 회복할 수 없음
            return;
        }

        // 체력 추가
        health += newHealth;
    }

    // 사망 처리
    public virtual void Die()
    {
        // onDeath 이벤트에 등록된 메서드가 있다면 실행
        if (onDeath != null)
        {
            onDeath();
        }

        // 사망 상태를 참으로 변경
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
