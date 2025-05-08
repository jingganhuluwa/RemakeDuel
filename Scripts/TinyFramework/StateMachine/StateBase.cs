// 文件：StateBase.cs
// 作者：急冻雪柜
// 描述：状态基类
// 日期：2025/05/06 1:59

namespace TinyFramework;

public abstract class StateBase
{
    protected StateMachine StateMachine { get; private set; }

    /// <summary>
    /// 初始化状态
    /// 只在状态第一次创建时执行
    /// </summary>
    /// <param name="stateMachine">所属状态机</param>
    public virtual void Init( StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    /// <summary>
    /// 反初始化
    /// </summary>
    public virtual void UnInit()
    {
        StateMachine = null;
        PoolManager.Instance.PushObject(this);
    }

    /// <summary>
    /// 状态进入
    /// 每次进入状态时执行
    /// </summary>
    public virtual void Enter()
    {
    }
    
    public virtual void Update()
    {
    }
    


    /// <summary>
    /// 状态退出
    /// 每次退出状态时执行
    /// </summary>
    public virtual void Exit()
    {
    }
}