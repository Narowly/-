using CommunityToolkit.Mvvm.Messaging;
using Project.Common;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.Views.UserControls
{
    /// <summary>
    /// ProcessTemplate.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessTemplate
    {
        public ProcessTemplate(ProcessTemplateViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            //RegistMessages();
        }
        //public void RegistMessages()
        //{
        //    WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProcessTemplate, (MessageHandler<object, string>)((obj, m) =>
        //    {
        //        var p = Window.GetWindow(this);
        //        if (p != null) p.Close();
        //    }));
        //}

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text;
            if (string.IsNullOrWhiteSpace(newText))
            {
                textBox.Text = 0.ToString();
                return;
            }


            // 构建一个StringBuilder来逐步构建有效的文本  
            StringBuilder sb = new StringBuilder();
            bool decimalPointAdded = false; // 标记是否已经添加了小数点  
            bool negativeSignAllowed = true; // 标记是否允许负号（仅在字符串开头）  

            foreach (char c in newText)
            {
                if (c == '-')
                {
                    // 负号只允许出现在字符串开头，且之前不能有内容  
                    if (sb.Length == 0)
                    {
                        sb.Append(c);
                        negativeSignAllowed = false; // 负号已添加，之后不再允许  
                    }
                    // 如果负号不是第一个字符，则忽略它  
                    continue;
                }
                else if (c == '.')
                {
                    // 小数点只允许添加一次，且前面必须有数字（除非前面是负号）  
                    if (!decimalPointAdded && (sb.Length > 0 || negativeSignAllowed))
                    {
                        sb.Append(c);
                        decimalPointAdded = true;
                    }
                    // 如果小数点已添加或位置不正确，则忽略它  
                    continue;
                }
                else if (char.IsDigit(c))
                {
                    // 数字总是允许的  
                    sb.Append(c);
                }
                // 其他字符被忽略  
            }

            // 如果构建后的文本与原始文本不同，则更新TextBox的文本  
            if (sb.ToString() != newText)
            {
                textBox.Text = sb.ToString();

                // 可能需要调整光标位置，这取决于你的具体需求  
                // 例如，将光标移到最后：textBox.CaretIndex = textBox.Text.Length;  
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var p = Window.GetWindow(this);
            if (p != null) p.Close();
        }
    }
}
