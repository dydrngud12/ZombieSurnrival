using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� �Է¿� ���� �÷��̾� ĳ���͸� �����̴� ��ũ��Ʈ
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �յ� �������� �ӵ�
    public float rotateSpeed = 180f; // �¿� ȸ�� �ӵ�

    private Playerlnput playerlnput; // �÷��̾� �Է��� �˷��ִ� ������Ʈ
    private Rigidbody playerRigidbody;// �÷��̾� ĳ������ ������ٵ�
    private Animator playerAnimator; // �÷��̾� ĳ������ �ִϸ�����
    // Start is called before the first frame update
    void Start()
    {
        // ����� ������Ʈ���� ���� ��������
        playerlnput = GetComponent<Playerlnput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    // FixedUpdate�� ���� ���� �ֱ⿡ ���� �����
    private void FixedUpdate()
    {
        
        // ȸ�� ����
        Rotate();
        // ������ ����
        Move();

        // �Է°��� ���� �ִϸ������� Move �Ķ���Ͱ� ����
        playerAnimator.SetFloat("Move", playerlnput.move);
    }

    // �Է°��� ���� ĳ���͸� �յڷ� ������
    private void Move() {
        // ��������� �̵��� �Ÿ����
        Vector3 moveDistance =
                playerlnput.move * transform.forward * moveSpeed * Time.deltaTime;
        // ������ٵ� �̿��� ���� ������Ʈ ��ġ ����
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    // �Է°��� ���� ĳ���͸� �¿�� ȸ��
    private void Rotate() {
        // ��������� ȸ���� ��ġ ���
        float turn = playerlnput.rotate * rotateSpeed * Time.deltaTime;
        // ������ٵ� �̿��� ���� ������Ʈ ȸ�� ����
        playerRigidbody.rotation =
            playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
     
    }
}
