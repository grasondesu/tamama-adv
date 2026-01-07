using UnityEngine;
using System;

public class ContactMail : MonoBehaviour
{
    [Header("メール設定（Inspectorで変更）")]
    [SerializeField] private string mailAddress = "support@example.com";
    [SerializeField] private string subject = "お問い合わせ";
    [TextArea(5, 10)]
    [SerializeField] private string bodyTemplate =
@"【お問い合わせ内容】

アプリ名：
バージョン：
端末：
OS：

内容：
";

    public void SendMail()
    {
        if (string.IsNullOrEmpty(mailAddress))
        {
            Debug.LogWarning("メールアドレスが設定されていません");
            return;
        }

        string url =
            "mailto:" + mailAddress +
            "?subject=" + Uri.EscapeDataString(subject) +
            "&body=" + Uri.EscapeDataString(bodyTemplate);

        Application.OpenURL(url);
    }
}
