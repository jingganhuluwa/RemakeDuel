// 文件：PoolAttribute.cs
// 作者：急冻雪柜
// 描述：确定一个类是否需要对象池
// 日期：2025/05/06 2:44

using System;

namespace TinyFramework;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PoolAttribute : Attribute
{

}