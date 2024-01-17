using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AdvancedSharpAdbClient.Native.Models
{
    public struct BufferHost(nint buffer, int count) : IList<byte>
    {
        public nint Buffer = buffer;
        public int Count = count;

        readonly int ICollection<byte>.Count => Count;

        readonly bool ICollection<byte>.IsReadOnly => false;

        public readonly unsafe byte this[int index]
        {
            get => ((byte*)Buffer)[index];
            set => ((byte*)Buffer)[index] = value;
        }

        public readonly unsafe Span<byte> AsSpan() => new((void*)Buffer, Count);

        public readonly int IndexOf(byte item) => AsSpan().IndexOf(item);

        public readonly void Clear() => AsSpan().Clear();

        public readonly bool Contains(byte item) => AsSpan().Contains(item);

        public readonly void CopyTo(byte[] array, int arrayIndex)
        {
            Span<byte> values = AsSpan();
            values.CopyTo(array.AsSpan(arrayIndex));
        }

        public readonly IEnumerator<byte> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #region Not Implemented

        readonly void IList<byte>.Insert(int index, byte item) => throw new NotImplementedException();

        readonly void IList<byte>.RemoveAt(int index) => throw new NotImplementedException();

        readonly void ICollection<byte>.Add(byte item) => throw new NotImplementedException();

        readonly bool ICollection<byte>.Remove(byte item) => throw new NotImplementedException();

        #endregion

        public static implicit operator BufferHost(byte[] array) =>
            new(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0), array.Length);
        public static implicit operator BufferHost(ArrayHost<byte> array) =>
            new(array.Array, array.Count);
        public static implicit operator ArrayHost<byte>(BufferHost array) =>
            new(array.Buffer, array.Count);
    }
}
