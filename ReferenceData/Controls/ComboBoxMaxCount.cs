using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ReferenceData
{
    class ComboBoxMaxCount : ComboBox
    {
        public static int GetMaxDropDownItems(DependencyObject obj)
        {
            return (int)obj.GetValue(MaxDropDownItemsProperty);
        }
        public static void SetMaxDropDownItems(DependencyObject obj, int value)
        {
            obj.SetValue(MaxDropDownItemsProperty, value);
        }

        public static readonly DependencyProperty MaxDropDownItemsProperty =
            DependencyProperty.RegisterAttached("MaxDropDownItems", typeof(int), typeof(ComboBoxMaxCount), new PropertyMetadata
            {
                //обратный вызов, производимый в случае изменения свойства
                PropertyChangedCallback = (obj, e) =>
                {
                    var box = (ComboBox)obj;
                    box.DropDownOpened += UpdateHeight;
                    if (box.IsDropDownOpen)
                        UpdateHeight(box, null);
                }
            });

        static void UpdateHeight(object sender, EventArgs e)
        {
            var box = (ComboBox)sender;
            box.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            //здесь мы описываем, что будет происходить
            {
                var container = box.ItemContainerGenerator.ContainerFromIndex(0) as UIElement;
                if (container != null && container.RenderSize.Height > 0)
                    box.MaxDropDownHeight = container.RenderSize.Height * GetMaxDropDownItems(box);
            }));
        }
    }
}
