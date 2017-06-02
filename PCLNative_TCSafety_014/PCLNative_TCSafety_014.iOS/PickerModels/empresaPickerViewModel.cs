using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PCLNative_TCSafety_014.iOS.PickerModels
{
    class EmpresaPickerViewModel : UIPickerViewModel
    {
        private List<string> _itemsEmpresas;

        public int _selectedIndex { get; private set; }

        public event EventHandler EmpresaSelectedChanged;

        public EmpresaPickerViewModel(List<string> list)
        {
            _itemsEmpresas = list;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _itemsEmpresas.Count;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _itemsEmpresas[(int)row];
        }

        public int GetIndex(UIPickerView pickerView, nint row, nint component)
        {
            _selectedIndex = (int)row;
            return (int)row;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            var nombre_empresa = _itemsEmpresas[(int)row];
            _selectedIndex = (int)row;
            EmpresaSelectedChanged?.Invoke(null, null);
        }
    }
}