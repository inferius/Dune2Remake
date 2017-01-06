//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Dune2RemakeTest.HelperClass
//{
//    public class UnrolledList<T> : IEnumerable<T>
//    {
//        // kapacita nového uzlu
//        private int _nodeCapacity = 4;
//        // uzel pro pridani dalsich prvku
//        private UnrolledListNode<T> _currentNode { get; set; }
//        // prvni uzel kolekce
//        public UnrolledListNode<T> FirstNode { get; set; }



//        public UnrolledList()
//        {
//            _currentNode = FirstNode = new UnrolledListNode<T>(_nodeCapacity);
//        }

//        public void Add(T item)
//        {
//            if (_currentNode.Add(item))
//            {
//                _nodeCapacity *= 2;
//                _currentNode.Next = new UnrolledListNode<T>(_nodeCapacity);
//                _currentNode = _currentNode.Next;
//            }
//        }

//        public IEnumerator<UnrolledList<T>> GetEnumerator()
//        {
//            return new UnrolledListEnumerator<T>();
//        }

//        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//        {
//            return new UnrolledListEnumerator<T>();
//        }
//    }

//    public class UnrolledListNode<T>
//    {
//        private int _currentIndex; // pozice pro pridani dalsiho prvku

//        public T[] Array { get; set; } // pole pro jednotlive prvky		
//        public UnrolledListNode<T> Next { get; set; } // nasledujici uzel       

//        public UnrolledListNode(int capacity)
//        {
//            Array = new T[capacity];
//        }

//        public bool Add(T item)
//        {
//            Array[_currentIndex] = item;
//            return Array.Length <= ++_currentIndex;
//        }

//        public bool IsFull(int index)
//        {
//            return Array.Length <= index + 1;
//        }
//    }

//    public class UnrolledListEnumerator<T> : IEnumerator<T>
//    {
//        private int _currentIndex;
//        private UnrolledListNode<T> _currentNode;
//        private UnrolledList<T> _list;

//        public UnrolledListEnumerator(UnrolledList<T> list)
//        {
//            _list = list;
//            _currentNode = list.FirstNode;
//        }

//        public T Current
//        {
//            get { return _currentNode.Array[_currentIndex]; }
//        }

//        public void Dispose() { return; }

//        object System.Collections.IEnumerator.Current
//        {
//            get { return Current; }
//        }

//        public bool MoveNext()
//        {
//            _currentIndex++;

//            if (_currentNode.IsFull(_currentIndex))
//            {
//                _currentIndex = 0;
//                _currentNode = _currentNode.Next;
//            }

//            return _currentNode != null;
//        }

//        public void Reset()
//        {
//            _currentIndex = 0;
//            _currentNode = _list.FirstNode;
//        }
//    }
//}
