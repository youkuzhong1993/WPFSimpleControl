namespace ControlLib.RandomlyPlacedControl
{
    public class RPCImage : RandomlyPlacedControlBase
    {
        #region Fields
        #region ImagePath
        private string _imagePath;
        /// <summary>
        /// 图像路径
        /// </summary>
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #endregion

        public RPCImage()
            : base()
        {
            DefaultWidth = 300;
            DefaultHeight = 300;
            ImagePath = "";
        }
    }
}
