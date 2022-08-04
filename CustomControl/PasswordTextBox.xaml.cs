using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace CustomControl
{
    public class PasswordTextBox : TextBox
    {
        static PasswordTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordTextBox), new FrameworkPropertyMetadata(typeof(PasswordTextBox)));
            FocusableProperty.OverrideMetadata(typeof(PasswordTextBox), new FrameworkPropertyMetadata(true));
        }

        bool IsUpdate = false;

        #region Property
        #region CornerRadius
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(PasswordTextBox), new PropertyMetadata(new CornerRadius(0)));
        #endregion

        #region Password
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordTextBox), new PropertyMetadata("", OnPasswordChanged));

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            PasswordTextBox control = (PasswordTextBox)d;
            control.UpdateText();
        }
        #endregion

        #region PasswordChar
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PasswordChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(PasswordTextBox), new PropertyMetadata('*'));
        #endregion

        #region IsDisplayPassword
        public bool IsDisplayPassword
        {
            get { return (bool)GetValue(IsDisplayPasswordProperty); }
            set { SetValue(IsDisplayPasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDisplayPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDisplayPasswordProperty =
            DependencyProperty.Register("IsDisplayPassword", typeof(bool), typeof(PasswordTextBox), new PropertyMetadata(false, OnIsDisplayPasswordChanged));

        private static void OnIsDisplayPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            PasswordTextBox control = (PasswordTextBox)d;
            control.UpdateText();
            control.Focus();
        }
        #endregion
        #endregion

        #region OnTextChanged
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (IsUpdate)
            {
                return;
            }

            if (string.IsNullOrEmpty(Text))
            {
                Password = "";
                return;
            }

            IsUpdate = true;
            if (IsDisplayPassword)
            {
                Password = Text;
            }
            else
            {
                foreach (var change in e.Changes)
                {
                    if (change == null)
                    {
                        continue;
                    }

                    if (change.RemovedLength > 0)
                    {
                        Password = Password.Remove(change.Offset, change.RemovedLength);
                    }

                    if (change.AddedLength > 0)
                    {
                        string s = Text.Substring(change.Offset, change.AddedLength);
                        Password = Password.Insert(change.Offset, s);
                    }
                }

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < Password.Length; i++)
                {
                    stringBuilder.Append(PasswordChar);
                }

                int index = CaretIndex;
                Text = stringBuilder.ToString();
                if (Text.Length >= index)
                {
                    Select(index, 0);
                }
            }
            IsUpdate = false;
        }
        #endregion

        #region UpdateText
        private void UpdateText()
        {
            if (IsUpdate)
            {
                return;
            }

            IsUpdate = true;
            if (string.IsNullOrEmpty(Password))
            {
                Text = "";
            }

            int index = CaretIndex;
            if (IsDisplayPassword)
            {
                Text = Password;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < Password.Length; i++)
                {
                    stringBuilder.Append(PasswordChar);
                }

                Text = stringBuilder.ToString();
            }

            if (Text.Length >= index)
            {
                Select(index, 0);
            }

            IsUpdate = false;
        }
        #endregion
    }
}
