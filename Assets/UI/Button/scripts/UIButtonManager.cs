using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonManager : MonoBehaviour
{
    [Header("プレイヤー本体")]
    public PlayerController player;

    [Header("左ボタン")]
    public Button leftButton;
    public Image leftImage;
    public Sprite leftNormalSprite;
    public Sprite leftPressedSprite;

    [Header("右ボタン")]
    public Button rightButton;
    public Image rightImage;
    public Sprite rightNormalSprite;
    public Sprite rightPressedSprite;

    [Header("ジャンプボタン")]
    public Button jumpButton;
    public Image jumpImage;
    public Sprite jumpNormalSprite;
    public Sprite jumpPressedSprite;

    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    void Update()
    {
        if (isLeftPressed)
            player.SetMoveDirection(-1f);
        else if (isRightPressed)
            player.SetMoveDirection(1f);
        else
            player.SetMoveDirection(0f);
    }

    // 左ボタン押下・離す
    public void OnLeftDown()
    {
        isLeftPressed = true;
        if (leftImage != null && leftPressedSprite != null)
            leftImage.sprite = leftPressedSprite;
    }

    public void OnLeftUp()
    {
        isLeftPressed = false;
        if (leftImage != null && leftNormalSprite != null)
            leftImage.sprite = leftNormalSprite;
    }

    // 右ボタン押下・離す
    public void OnRightDown()
    {
        isRightPressed = true;
        if (rightImage != null && rightPressedSprite != null)
            rightImage.sprite = rightPressedSprite;
    }

    public void OnRightUp()
    {
        isRightPressed = false;
        if (rightImage != null && rightNormalSprite != null)
            rightImage.sprite = rightNormalSprite;
    }

    // ジャンプボタン押下（押した瞬間のみ）
    public void OnJumpDown()
    {
        if (jumpImage != null && jumpPressedSprite != null)
            jumpImage.sprite = jumpPressedSprite;

        player.JumpByButton(); // プレイヤーのジャンプ実行
    }

    public void OnJumpUp()
    {
        if (jumpImage != null && jumpNormalSprite != null)
            jumpImage.sprite = jumpNormalSprite;
    }
}