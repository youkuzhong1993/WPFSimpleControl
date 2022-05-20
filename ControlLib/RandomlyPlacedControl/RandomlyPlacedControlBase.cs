using Prism.Mvvm;

namespace ControlLib.RandomlyPlacedControl
{
    public class RandomlyPlacedControlBase : BindableBase
    {
        #region Fields
        /// <summary>
        /// 默认宽度
        /// </summary>
        public float DefaultWidth { get; set; } = 10;
        /// <summary>
        /// 默认高度
        /// </summary>
        public float DefaultHeight { get; set; } = 10;
        /// <summary>
        /// 第一列
        /// </summary>
        public float FirstCol { get; set; } = 1;
        /// <summary>
        /// 第二列
        /// </summary>
        public float SecondCol { get; set; } = 1;
        /// <summary>
        /// 第三列
        /// </summary>
        public float ThirdCol { get; set; } = 1;
        /// <summary>
        /// 第一行
        /// </summary>
        public float FirstRow { get; set; } = 1;
        /// <summary>
        /// 第二行
        /// </summary>
        public float SecondRow { get; set; } = 1;
        /// <summary>
        /// 第三行
        /// </summary>
        public float ThirdRow { get; set; } = 1;

        #region PanelZIndex
        private int _panelZIndex = 0;
        /// <summary>
        /// Z轴位置
        /// </summary>
        public int PanelZIndex
        {
            get { return _panelZIndex; }
            set
            {
                _panelZIndex = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ControlName
        private string _controlName;
        /// <summary>
        /// 控件名称
        /// </summary>
        public string ControlName
        {
            get { return _controlName; }
            set
            {
                _controlName = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #endregion

        public RandomlyPlacedControlBase()
        {

        }
    }
}
