using System;
using System.Collections.Generic;
using PCLNative_TCSafety_014.iOS.PickerModels;
using UIKit;

namespace PCLNative_TCSafety_014.iOS
{
    internal class pickerViewModel : UIPickerViewModel
    {
        private List<string> mItems;

        public int _selectedIndex { get; private set; }

        public event EventHandler SelectionChanged;

        public pickerViewModel(List<string> mItems)
        {
            this.mItems = mItems;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return mItems.Count;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return mItems[(int)row];
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            var selected = mItems[(int)row];
            _selectedIndex = (int)row;
            SelectionChanged?.Invoke(null, null);
        }

        public int GetIndex(UIPickerView pickerView, nint row, nint component)
        {
            _selectedIndex = (int)row;
            return (int)row;
        }

        public static implicit operator pickerViewModel(EmpresaPickerViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}