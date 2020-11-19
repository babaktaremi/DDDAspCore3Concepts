using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class PagingViewModel<T> where T:IList
    {
        private readonly int _totalItems;
        private readonly int _take;
        private readonly int _currentPage;

        public PagingViewModel(int totalItems, int take,T item,int currentPage)
        {
            _totalItems = totalItems;
            _take = take;
            _currentPage = currentPage;
            Items = item;
        }

        public T Items { get; }
        public int TotalItems => _totalItems;
        public bool IsLastPage => !(_totalItems> _take * _currentPage);
        public int CurrentPage => _currentPage;
        public int TotalPages => (_totalItems / _take) + 1;
    }
}
