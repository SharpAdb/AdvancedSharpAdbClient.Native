using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AdvancedSharpAdbClient.Native.Models
{
    public struct ArrayHost<T>(nint array, int count) : IEnumerable<T> where T : unmanaged
    {
        public nint Array = array;
        public int Count = count;

        public readonly unsafe T this[int index]
        {
            get => ((T*)Array)[index];
            set => ((T*)Array)[index] = value;
        }

        public readonly void CopyTo(T[] array, int arrayIndex)
        {
            Span<T> values = AsSpan();
            values.CopyTo(array.AsSpan(arrayIndex));
        }

        public readonly IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public readonly unsafe Span<T> AsSpan() => new((void*)Array, Count);

        public static implicit operator ArrayHost<T>(T[] array) =>
            new(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0), array.Length);
    }
}
