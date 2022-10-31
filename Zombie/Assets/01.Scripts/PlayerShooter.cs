using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �־��� Gun ������Ʈ�� ��ų� ������
// �˸��� �ִϸ��̼��� ����ϰ� IK�� ����� ĳ���� ����� �ѿ� ��ġ�ϵ��� ����


public class PlayerShooter : MonoBehaviour
{
    public Gun gun; // ����� ��
    public Transform gunPivot; // �� ��ġ�� ������
    public Transform LeftHandMount; // ���� ���� ������, �޼��� ��ġ�� ����
    public Transform rightHandMount; // ���� ������ ������, �������� ��ġ�� ����

    private Playerlnput playerlnput; // �÷��̾��� �Է�
    private Animation playerAnimator; // �ִϸ����� ������Ʈ
    // Start is called before the first frame update
    void Start()
    {
        // ����� ������Ʈ ��������
        playerlnput = GetComponent<Playerlnput>();
        playerAnimator = GetComponent<Animation>();
    }

    

    private void OnEnable()
    {
        // ���Ͱ� Ȱ��ȭ�� �� �ѵ� �Բ� Ȱ��ȭ
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        // ���Ͱ� ��Ȱ��ȭ�� �� �ѵ� �Բ� ��Ȱ��ȭ
        gun.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �Է��� �����ϰ� ���� �߻��ϰų� ������
        if (playerlnput.fire)
        {
            // �߻� �Է� ���� �� �� �߻�
            gun.Fire();
        }
        else if (playerlnput.reload)
        {
            // ������ �Է� ���� �� ������
            if (gun.Reload())
            {
                // ������ ���� �ÿ��� ������ �ִϸ��̼� ���
                playerAnimator.SetTrigger("Reload");
            }
        }

        // ���� ź�� UI ����
        UpdateUI();

    }

    // ź�� UI ����
    private void UpdateUI()
    {
        if (gun != null && UIManager.instance != null)
        {
            // UI �Ŵ����� ź�� �ؽ�Ʈ�� źâ�� ź�˰� ���� ��ü ź�� ǥ��
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
    }


    // �ִϸ������� IK ����
    private void OnAnimatorIK(int layerIndex)
    {
        // ���� ������ gunPivot�� 3D ���� ������ �Ȳ�ġ ��ġ�� �̵�
        gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // IK �� ����Ͽ� �޼��� ��ġ�� ȸ���� ���� ���� �����̿� ����
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandMount.rotation);

        // IK �� ����Ͽ� �������� ����� ȸ���� ���� ������ �����̿� ����
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
}

