// 文件：BasePanel.cs
// 作者：急冻雪柜
// 描述：基础面板
// 日期：2025/05/09 16:10

using Godot;

namespace TinyFramework;

public partial class BasePanel : Panel
{
    public virtual void OnShow()
    {
        Show();
    }

    public virtual void OnHide()
    {
        Hide();
    }

    public virtual void OnClose()
    {
        UIManager.Instance.CloseUI(this);
    }
}