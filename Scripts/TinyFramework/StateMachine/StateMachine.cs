// 文件：StateMachine.cs
// 作者：急冻雪柜
// 描述：状态机控制器
// 日期：2025/05/06 2:03

using System.Collections.Generic;
using Godot;

namespace TinyFramework;

public interface IStateMachineOwner{}

[Pool]
public class StateMachine
{
    /// <summary>
    /// 当前状态枚举的值
    /// </summary>
    public int CurrentStateType { get;private set; }=-1;

    /// <summary>
    /// 当前生效状态
    /// </summary>
    public StateBase CurrentStateObj;
    
    /// <summary>
    /// 宿主
    /// </summary>
    public IStateMachineOwner Owner { get; private set; }
    
    /// <summary>
    /// 状态字典
    /// key:状态枚举的值,value:具体状态
    /// </summary>
    private readonly Dictionary<int, StateBase> _stateDict = new Dictionary<int, StateBase>();
    
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="owner">宿主</param>
    public void Init(IStateMachineOwner owner){
        Owner = owner;
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newStateType">新状态</param>
    /// <param name="isReCurrentState">如果新状态和当前状态一致,是否也要切换</param>
    /// <typeparam name="T">具体切换到的状态脚本类型</typeparam>
    /// <returns></returns>
    public bool ChangeState<T>(int newStateType,bool isReCurrentState = false) where T : StateBase,new()
    {
        //如果新状态和当前状态一致,则不切换
        if (CurrentStateType == newStateType && !isReCurrentState)
        {
            return false;
        }
        //如果当前状态存在,则先退出
        if (CurrentStateObj != null)
        {
            CurrentStateObj.Exit();
            LogicFrameManager.Instance.RemoveLogicFrameListener(CurrentStateObj.Update);
            
        }

        // 进入新状态
        CurrentStateObj = GetState<T>(newStateType);
        CurrentStateType = newStateType;
        CurrentStateObj.Enter();
        LogicFrameManager.Instance.AddLogicFrameListener(CurrentStateObj.Update);

        return true;
    }
    
    /// <summary>
    /// 从对象池获取一个状态
    /// </summary>
    private StateBase GetState<T>(int stateType) where T : StateBase, new()
    {
        if (_stateDict.TryGetValue(stateType, out StateBase state)) return state;

        state = ResManager.Load<T>();
        state.Init(this);
        _stateDict.Add(stateType, state);
        return state;
    }
    

    /// <summary>
    /// 停止工作
    /// 把所有状态都释放，但是StateMachine未来还可以工作
    /// </summary>
    public void Stop()
    {
        // 处理当前状态的额外逻辑
        CurrentStateObj.Exit();
        LogicFrameManager.Instance.RemoveLogicFrameListener(CurrentStateObj.Update);
        CurrentStateType = -1;
        CurrentStateObj = null;
        // 处理缓存中所有状态的逻辑
        var enumerator = _stateDict.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.UnInit();
        }
        _stateDict.Clear();
    }

    /// <summary>
    /// 销毁，宿主应该释放掉StateMachin的引用
    /// </summary>
    public void Destory()
    {
        // 处理所有状态
        Stop();
        // 放弃所有资源的引用
        Owner = null;

        // 放进对象池
        PoolManager.Instance.PushObject(this);
    }

}