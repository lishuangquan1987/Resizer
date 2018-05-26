using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Resizer
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:Resizer"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:Resizer;assembly=Resizer"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [TemplatePart(Name ="bd",Type =typeof(Border))]
    public class ResizerControl : Control
    {
        private Rectangle rectangle;
        private FrameworkElement parent;
        
        private Point lastPoint;
        #region 依赖属性
        public static readonly DependencyProperty OrigenProperty =
          DependencyProperty.Register("Origen", typeof(EnOrigenation), typeof(ResizerControl),
          new PropertyMetadata(EnOrigenation.Horizontal, OnOrigenChanged));


        #endregion
        #region 属性
        public EnOrigenation Origenation
        {
            get { return (EnOrigenation)GetValue(OrigenProperty); }
            set { SetValue(OrigenProperty, value); }
        }
        #endregion
        private static void OnOrigenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ResizerControl resizerControl = d as ResizerControl;
            resizerControl.OnApplyTemplate();
        }
        static ResizerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizerControl), new FrameworkPropertyMetadata(typeof(ResizerControl)));
        }
        public ResizerControl()
        {
            this.Loaded += ResizerControl_Loaded;
            
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this.parent);
                if (lastPoint == new Point(0, 0))
                {
                    lastPoint = p;
                }
                else
                {
                    switch (Origenation)
                    {
                        case EnOrigenation.Horizontal:
                            if (this.HorizontalAlignment == HorizontalAlignment.Left)
                            {
                                double diffX = p.X - lastPoint.X;
                                this.parent.Width += diffX;
                                this.Margin = new Thickness(this.Margin.Left + diffX, this.Margin.Top, this.Margin.Right, this.Margin.Bottom);
                            }
                            else if (this.HorizontalAlignment == HorizontalAlignment.Right)
                            {
                                double diffX = p.X - lastPoint.X;
                                this.parent.Width += diffX;
                            }
                            break;
                        case EnOrigenation.Vertical:
                            break;
                        case EnOrigenation.TopLeft:
                            break;
                        case EnOrigenation.TopRight:
                            break;
                        case EnOrigenation.BottomLeft:
                            break;
                        case EnOrigenation.BottomRight:
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                lastPoint = new Point(0, 0);
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            lastPoint = new Point(0, 0);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            switch (Origenation)
            {
                case EnOrigenation.Horizontal:
                    this.Cursor = Cursors.ScrollWE;
                    break;
                case EnOrigenation.Vertical:
                    this.Cursor = Cursors.ScrollNS;
                    break;
                case EnOrigenation.TopLeft:
                    this.Cursor = Cursors.ScrollNW;
                    break;
                case EnOrigenation.TopRight:
                    this.Cursor = Cursors.ScrollNE;
                    break;
                case EnOrigenation.BottomLeft:
                    this.Cursor = Cursors.ScrollSW;
                    break;
                case EnOrigenation.BottomRight:
                    this.Cursor = Cursors.ScrollSE;
                    break;
                default:
                    break;
            }
        }

        private void ResizerControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnApplyTemplate();
            this.parent = this.Parent as FrameworkElement;
            if (rectangle != null)
            {
                rectangle.MouseEnter += Border_MouseEnter;
                rectangle.MouseLeave += Border_MouseLeave;
                rectangle.MouseMove += Border_MouseMove;
            }
            Panel.SetZIndex(rectangle, 1000);            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            rectangle = GetTemplateChild("bd") as Rectangle;
            double defaultWidthOrHeight = 2;
            
            if (rectangle != null)
            {
                //switch (Origenation)
                //{
                //    case EnOrigenation.Horizontal:
                //        this.Width = parent.Width;
                //        this.Height = this.Height == 0 ? defaultWidthOrHeight : this.Height;
                //        break;
                //    case EnOrigenation.Vertical:
                //        break;
                //    case EnOrigenation.TopLeft:
                //        break;
                //    case EnOrigenation.TopRight:
                //        break;
                //    case EnOrigenation.BottomLeft:
                //        break;
                //    case EnOrigenation.BottomRight:
                //        break;
                //    default:
                //        break;
                //}
            }
        }
    }
}
