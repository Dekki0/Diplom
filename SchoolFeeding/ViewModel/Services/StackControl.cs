﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolFeeding.ViewModel.Services
{
    record StackControl
    {
        #region public varible
        
        public static event Action<object>? DataChanged;
        
        #endregion

        #region private varible

        private static readonly Stack<object> Pages = new();

        #endregion

        #region public methods
        public static object GetPage()
        {
            _ = Pages.Count > 1 ? Pages.Pop() : Pages.Peek();
            return Pages.Count > 1 ? Pages.Pop() : Pages.Peek();
        }
        public static void AddPage(object page)
        {
            if(page is null)
                throw new ArgumentNullException(nameof(page));
            Pages.Push(page);
            DataChanged?.Invoke(page);
        }
        public static void Clear()=>Pages.Clear();
        #endregion
    }
}
