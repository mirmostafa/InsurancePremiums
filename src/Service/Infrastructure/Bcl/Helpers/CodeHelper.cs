using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Infrastructure.Bcl.Helpers;
public static class CodeHelper
{
    public static TInstance With<TInstance>(this TInstance instance, Action<TInstance> action)
    {
        action(instance);
        return instance;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TInstance With<TInstance>(this TInstance instance, in object? obj) =>
            instance;
}
