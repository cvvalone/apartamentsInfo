using Common.ConsoleUI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.ConsoleUI.Selecting
{
    public abstract class FilteringController<T>
    {
        protected FilterInfo<T>[] _filtersInfo;
        protected abstract void IniFiltersInfo(out FilterInfo<T>[] filtersInfo);
        protected MultiselectListBox<FilterInfo<T>> ListBox { get; private set; }
        public FilteringController()
        {
            IniFiltersInfo(out _filtersInfo);
            ListBox = new MultiselectListBox<FilterInfo<T>>(_filtersInfo);
        }

        protected void ShowHeader()
        {
            Console.WriteLine("\n Вибір фільтрів");
        }

        protected void SetListBoxPosition()
        {
            ListBox.SetPostition(Console.CursorLeft + 2, Console.CursorTop);
        }

        public void SelectFilter()
        {
            ListBox.SelectionMode = SelectionMode.OnePerSession;
            ShowHeader();
            SetListBoxPosition();
            ListBox.Focus();
            FilterInfo<T> filterInfo = ListBox.SelectionValue; 
            ProcessFilterInfo(filterInfo);
        }

        protected Filter<T> _selectedFilters;

        protected virtual void ProcessFilterInfo(FilterInfo<T> filterInfo)
        {
            if(filterInfo.Filter == null)
            {
                return;
            }
            filterInfo.Selected = !filterInfo.Selected;
            if (filterInfo.Selected)
            {
                _selectedFilters += filterInfo.Filter;
            }
            else
            {
                _selectedFilters -= filterInfo.Filter;
            }
            if(filterInfo.Selected && filterInfo.EnterData != null)
            {
                EnterFilterData(filterInfo);
            }
        }

        public virtual void EnterFilterData(FilterInfo<T> filterInfo)
        {
            int top = ListBox.Visible ? ListBox.Bottom + 2 : ListBox.Top + 1;
            Console.SetCursorPosition(0, top);
            filterInfo.EnterData();
        }

        public IEnumerable<T> ApplyFilters(IEnumerable<T> objects)
        {
            if (_selectedFilters != null)
                _selectedFilters(ref objects);
            return objects;
        }

    }
}
