﻿namespace Problem02.Stack
{
    using Problem01.List;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public Stack()
        {
            this._top = null;
            this.Count = 0;
        }

        public Stack(Node<T> top)
        {
            this._top = top;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public T Peek()
        {
            this.EnsureNotEmpty();
            return this._top.Element;
        }

        public T Pop()
        {
            this.EnsureNotEmpty();
            var pop = this._top.Element;
            this._top = this._top.Next;
            this.Count--;
            return pop;
        }

        private void EnsureNotEmpty()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }
        }

        public void Push(T item)
        {
            var newNode = new Node<T>
            {
                Element = item,
                Next = this._top
            };

            this._top = newNode;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (this._top.Element != null)
            {
                yield return this._top.Element;
                this._top = this._top.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}