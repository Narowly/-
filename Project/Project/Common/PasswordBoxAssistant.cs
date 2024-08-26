using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project.Common
{
    internal static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty BoundPassword =
        DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BoundPassword);
        }

        public static void SetBoundPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BoundPassword, value);
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;

            if (passwordBox == null)
                return;

            // 当绑定的密码改变时，更新 PasswordBox 的 Password  
            passwordBox.PasswordChanged -= PasswordChanged;
            if (!string.IsNullOrEmpty(e.NewValue as string))
                passwordBox.Password = e.NewValue as string;
            passwordBox.PasswordChanged += PasswordChanged;

            SetSelection(passwordBox, passwordBox.Password.Length, 0);
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            // 当 PasswordBox 的 Password 改变时，更新绑定的密码  
            SetBoundPassword(passwordBox, passwordBox.Password);
        }

        // <summary>
        /// 设置光标位置
        /// </summary>
        /// <param name="passwordBox"></param>
        /// <param name="start">光标开始位置</param>
        /// <param name="length">选中长度</param>
        private static void SetSelection(PasswordBox passwordBox, int start, int length)
        {
            passwordBox.GetType()
                       .GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
                       .Invoke(passwordBox, new object[] { start, length
         });
        }
    }
}
