namespace ControlLib.RandomlyPlacedControl
{
    public class RPCTextBlock : RandomlyPlacedControlBase
    {
        #region Fiedls
        #region FontSize
        private double _fontSize;
        /// <summary>
        /// 字体大小
        /// </summary>
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Text
        private string _text;
        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #endregion

        public RPCTextBlock()
            : base()
        {
            DefaultWidth = 90;
            DefaultHeight = 20;
            FontSize = 10;
            Text = "TextBlock";
        }
    }
}
