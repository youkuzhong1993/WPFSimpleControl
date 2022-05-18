using ControlLib.RandomlyPlacedControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomControl
{
    public class RandomlyPlacedControl : Control
    {
        static RandomlyPlacedControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RandomlyPlacedControl), new FrameworkPropertyMetadata(typeof(RandomlyPlacedControl)));
            FocusableProperty.OverrideMetadata(typeof(RandomlyPlacedControl), new FrameworkPropertyMetadata(true));
        }

        #region Fields
        private Grid SelectedGrid = null;
        private bool IsMouseDownSelctedGrid = false;
        private bool IsGirdSpliterDragEnter = false;
        private Point MouseDownPoint = new Point();
        #endregion


        #region Property
        #region DisplayGrid
        public static readonly DependencyProperty DisplayGridProperty =
            DependencyProperty.Register("DisplayGrid", typeof(Grid), typeof(RandomlyPlacedControl), new PropertyMetadata(null));

        public Grid DisplayGrid
        {
            get { return (Grid)GetValue(DisplayGridProperty); }
            private set { SetValue(DisplayGridProperty, value); }
        }
        #endregion

        #region ItemsSources
        public static readonly DependencyProperty ItemsSourcesProperty =
            DependencyProperty.Register("ItemsSources", typeof(IList<RandomlyPlacedControlBase>), typeof(RandomlyPlacedControl),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public IList<RandomlyPlacedControlBase> ItemsSources
        {
            get { return (IList<RandomlyPlacedControlBase>)GetValue(ItemsSourcesProperty); }
            set { SetValue(ItemsSourcesProperty, value); }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d == null)
            {
                return;
            }

            (d as RandomlyPlacedControl).RefreshView();
        }
        #endregion
        #endregion

        public RandomlyPlacedControl()
        {
            DisplayGrid = new Grid();
            AddHandler(GridSplitter.DragEnterEvent, new RoutedEventHandler(OnGridSpliter_DragEnter));
            AddHandler(GridSplitter.DragCompletedEvent, new RoutedEventHandler(OnGridSpliter_DragCompleted));
            AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(OnPreviewMouseLeftButtonDown));
        }

        #region OnRender
        protected override void OnRender(DrawingContext drawingContext)
        {
            if(DisplayGrid != null
                && DisplayGrid.Children.Count == 0)
            {
                RefreshView();
            }

            base.OnRender(drawingContext);
        }
        #endregion

        #region RefreshView
        private void RefreshView()
        {
            if(DisplayGrid == null
                || double.IsNaN(this.ActualWidth)
                || this.ActualWidth <= 0
                || double.IsNaN(this.ActualHeight)
                || this.ActualHeight <= 0)
            {
                return;
            }

            DisplayGrid.Children.Clear();
            if(ItemsSources == null
                || ItemsSources.Count == 0)
            {
                return;
            }

            foreach(RandomlyPlacedControlBase control in ItemsSources)
            {
                if(control == null)
                {
                    continue;
                }

                DisplayGrid.Children.Add(GetGrid(control));
            }
        }
        #endregion

        #region GetGrid
        private Grid GetGrid(RandomlyPlacedControlBase controlBase)
        {
            if(controlBase == null)
            {
                return null;
            }

            Grid grid = new Grid();
            if (controlBase.FirstCol == 1
                && controlBase.SecondCol == 1
                && controlBase.ThirdCol == 1
                && controlBase.FirstRow == 1
                && controlBase.SecondRow == 1
                && controlBase.ThirdRow == 1)
            {
                double secondCol = Math.Min(controlBase.DefaultWidth, this.ActualWidth);
                double col = (this.ActualWidth - secondCol) / 2;
                double secondeRow = Math.Min(controlBase.DefaultHeight, this.ActualHeight);
                double row = (this.ActualHeight - secondeRow) / 2;

                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(col, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(secondCol, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(col, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(row, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(secondeRow, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(row, GridUnitType.Star) });
            }
            else
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(controlBase.FirstCol, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(controlBase.SecondCol, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(controlBase.ThirdCol, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(controlBase.FirstRow, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(controlBase.SecondRow, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(controlBase.ThirdRow, GridUnitType.Star) });
            }

            AddGridSpliterOnGrid(grid);
            Binding binding = new Binding("PanelZIndex");
            grid.SetBinding(Panel.ZIndexProperty, binding);
            grid.DataContext = controlBase;

            ContentControl contentControl = new ContentControl();
            contentControl.SetValue(Grid.ColumnProperty, 1);
            contentControl.SetValue(Grid.RowProperty, 1);
            contentControl.SetValue(Panel.ZIndexProperty, 0);
            contentControl.Content = controlBase;
            contentControl.SetResourceReference(ContentControl.ContentTemplateSelectorProperty, "RPCContentTmeplateSelector");
            grid.Children.Add(contentControl);
            return grid;
        }
        #endregion

        #region AddGridSpliterOnGrid
        private void AddGridSpliterOnGrid(Grid grid)
        {
            if(grid == null)
            {
                return;
            }

            GridSplitter gridSplitter1 = new GridSplitter();
            gridSplitter1.Height = 1;
            gridSplitter1.VerticalAlignment = VerticalAlignment.Top;
            gridSplitter1.HorizontalAlignment = HorizontalAlignment.Stretch;
            gridSplitter1.SetValue(Grid.ColumnProperty, 1);
            gridSplitter1.SetValue(Grid.RowProperty, 1);
            gridSplitter1.SetValue(Panel.ZIndexProperty, 1);
            grid.Children.Add(gridSplitter1);

            GridSplitter gridSplitter2 = new GridSplitter();
            gridSplitter2.Height = 1;
            gridSplitter2.VerticalAlignment = VerticalAlignment.Bottom;
            gridSplitter2.HorizontalAlignment = HorizontalAlignment.Stretch;
            gridSplitter2.SetValue(Grid.ColumnProperty, 1);
            gridSplitter2.SetValue(Grid.RowProperty, 1);
            gridSplitter2.SetValue(Panel.ZIndexProperty, 1);
            grid.Children.Add(gridSplitter2);

            GridSplitter gridSplitter3 = new GridSplitter();
            gridSplitter3.Width = 1;
            gridSplitter3.VerticalAlignment = VerticalAlignment.Stretch;
            gridSplitter3.HorizontalAlignment = HorizontalAlignment.Left;
            gridSplitter3.SetValue(Grid.ColumnProperty, 1);
            gridSplitter3.SetValue(Grid.RowProperty, 1);
            gridSplitter3.SetValue(Panel.ZIndexProperty, 1);
            grid.Children.Add(gridSplitter3);

            GridSplitter gridSplitter4 = new GridSplitter();
            gridSplitter4.Width = 1;
            gridSplitter4.VerticalAlignment = VerticalAlignment.Stretch;
            gridSplitter4.HorizontalAlignment = HorizontalAlignment.Right;
            gridSplitter4.SetValue(Grid.ColumnProperty, 1);
            gridSplitter4.SetValue(Grid.RowProperty, 1);
            gridSplitter4.SetValue(Panel.ZIndexProperty, 1);
            grid.Children.Add(gridSplitter4);
        }
        #endregion

        #region OnGridSpliter_DragEnter
        private void OnGridSpliter_DragEnter(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == null
                || !(e.OriginalSource is GridSplitter)
                || !((e.OriginalSource as GridSplitter).Parent is Grid))
            {
                return;
            }

            IsGirdSpliterDragEnter = true;
        }
        #endregion

        #region OnGridSpliter_DragCompleted
        private void OnGridSpliter_DragCompleted(object sender, RoutedEventArgs e)
        {
            if(e.OriginalSource == null
                || !(e.OriginalSource is GridSplitter)
                || !((e.OriginalSource as GridSplitter).Parent is Grid))
            {
                return;
            }

            Grid grid = (e.OriginalSource as GridSplitter).Parent as Grid;
            UpdateRandomlyPlacedControlBase(grid);
            IsGirdSpliterDragEnter = false;
        }
        #endregion

        #region UpdateRandomlyPlacedControlBase
        private void UpdateRandomlyPlacedControlBase(Grid grid)
        {
            if (grid == null
                || !(grid.DataContext is RandomlyPlacedControlBase)
                || grid.ColumnDefinitions.Count != 3
                || grid.RowDefinitions.Count != 3)
            {
                return;
            }

            RandomlyPlacedControlBase randomlyPlacedControlBase = grid.DataContext as RandomlyPlacedControlBase;
            if (!ItemsSources.Contains(randomlyPlacedControlBase))
            {
                return;
            }

            randomlyPlacedControlBase.FirstCol = (float)grid.ColumnDefinitions[0].Width.Value;
            randomlyPlacedControlBase.SecondCol = (float)grid.ColumnDefinitions[1].Width.Value;
            randomlyPlacedControlBase.ThirdCol = (float)grid.ColumnDefinitions[2].Width.Value;
            randomlyPlacedControlBase.FirstRow = (float)grid.RowDefinitions[0].Height.Value;
            randomlyPlacedControlBase.SecondRow = (float)grid.RowDefinitions[1].Height.Value;
            randomlyPlacedControlBase.ThirdRow = (float)grid.RowDefinitions[2].Height.Value;

        }
        #endregion

        #region GetGridFromDependecyObject
        /// <summary>
        /// 递归找到Grid
        /// </summary>
        private Grid GetGridFromDependecyObject(DependencyObject dependencyObject)
        {
            if(dependencyObject == null
                || dependencyObject is GridSplitter)
            {
                return null;
            }

            if (dependencyObject is Grid
                && (dependencyObject as Grid).DataContext is RandomlyPlacedControlBase) 
            {
                return dependencyObject as Grid;
            }

            return GetGridFromDependecyObject(VisualTreeHelper.GetParent(dependencyObject));
        }
        #endregion

        #region OnPreviewMouseLeftButtonDown
        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(ItemsSources == null
                || DisplayGrid.Children.Count == 0
                || IsGirdSpliterDragEnter)
            {
                return;
            }

            SelectedGrid = GetGridFromDependecyObject(e.OriginalSource as DependencyObject);
            IsMouseDownSelctedGrid = SelectedGrid != null;
            MouseDownPoint = e.GetPosition(this);
        }
        #endregion

        #region OnPreviewMouseMove
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if(e.LeftButton == MouseButtonState.Released)
            {
                IsMouseDownSelctedGrid = false;
                SelectedGrid = null;
            }

            if (IsMouseDownSelctedGrid)
            {
                Point p = e.GetPosition(this);
                double x = p.X - MouseDownPoint.X;
                double y = p.Y - MouseDownPoint.Y;
                x = x <= 0 ? Math.Max(-SelectedGrid.ColumnDefinitions[0].Width.Value, x) : Math.Min(SelectedGrid.ColumnDefinitions[2].Width.Value, x);
                y = y <= 0 ? Math.Max(-SelectedGrid.RowDefinitions[0].Height.Value, y) : Math.Min(SelectedGrid.RowDefinitions[2].Height.Value, y);
                SelectedGrid.ColumnDefinitions[0].Width = new GridLength(SelectedGrid.ColumnDefinitions[0].Width.Value + x, GridUnitType.Star);
                SelectedGrid.ColumnDefinitions[2].Width = new GridLength(SelectedGrid.ColumnDefinitions[2].Width.Value - x, GridUnitType.Star);
                SelectedGrid.RowDefinitions[0].Height = new GridLength(SelectedGrid.RowDefinitions[0].Height.Value + y, GridUnitType.Star);
                SelectedGrid.RowDefinitions[2].Height = new GridLength(SelectedGrid.RowDefinitions[2].Height.Value - y, GridUnitType.Star);
                UpdateRandomlyPlacedControlBase(SelectedGrid);

                MouseDownPoint = p;
            }
        }
        #endregion

        #region OnPreviewMouseUp
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);

            IsMouseDownSelctedGrid = false;
            SelectedGrid = null;
        }
        #endregion
    }
}
