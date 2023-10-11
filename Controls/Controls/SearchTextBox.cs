using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Controls.Controls
{
    public class SearchTextBox : TextBox
    {
        public SearchTextBox()
        {
            this.KeyUp += CustomCoboBox_KeyUp;

            LostFocus += (s, e) =>
            {
                IsFocusLost = true;
                IsOpen = IsFocus = false;
                if (!String.IsNullOrEmpty(SelectedItem)) Text = SelectedItem;
            };

            GotFocus += (s, e) =>
            {
                IsFocus = true;
                if (!String.IsNullOrEmpty(SelectedItem))
                {
                    SelectAll();
                    Text = SelectedItem;
                    //ShowItems = ItemsSource;
                    //IsOpen = true;
                }
                IsFocusLost = false;
                IsOpen = false;
            };
        }
        private void CustomCoboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                if (SelectedItem == Text) Text = "";
            }
            if (IsOpen)
            {
                switch (e.Key)
                {
                    case Key.Down:
                        if (SelectedIndex < ShowItems.Count() - 1) SelectedIndex++;
                        return;
                    case Key.Up:
                        if (SelectedIndex < ShowItems.Count() && SelectedIndex > -1) SelectedIndex--;
                        return;
                    case Key.Enter:
                        if (SelectedIndex > -1)
                        {
                            Text = SelectedItem = ShowItems.ToArray()[SelectedIndex].ToString();
                            IsOpen = false;
                            return;
                        }
                        break;
                }
            }
            if (IsFocus)
            {
                ShowItems = (!String.IsNullOrEmpty(Text))
                            ? ItemsSource.Where(o => o.ToString().ToLower().Contains(Text.ToLower()) && Text != "")
                            : ItemsSource;

                //IsOpen = ShowItems.Count() > 0;
                //IsOpen = Text != "" && !IsFocusLost;
                IsOpen = !IsFocusLost;
                IsFocusLost = false;
            }
            //if(SelectedItem!=Text && IsFocus)
            //{
            //    ShowItems = ItemsSource.Where(o => o.ToString().Contains(Text) && Text != "");
            //    IsOpen = ShowItems.Count() > 0;
            //}
        }
        bool IsFocus = false;
        bool IsFocusLost = false;
        static SearchTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchTextBox),
                new FrameworkPropertyMetadata(typeof(SearchTextBox)));

            #region ItemsSourceProperty
            ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>),
                typeof(SearchTextBox), new PropertyMetadata(null));
            #endregion

            #region IsOpenProperty
            IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool),
                typeof(SearchTextBox), new PropertyMetadata(false, new PropertyChangedCallback(
                    (d, e) =>
                    {
                        SearchTextBox com = (d as SearchTextBox);
                        if (!com.IsOpen)
                        {
                            com.SelectedIndex = -1;
                            com.ShowItems = null;
                        }
                    })));
            #endregion

            #region ShowItemsProperty

            ShowItemsProperty =
            DependencyProperty.Register("ShowItems", typeof(IEnumerable<object>),
            typeof(SearchTextBox), new PropertyMetadata(null));
            #endregion

            #region SelectedIndexProperty
            SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int),
            typeof(SearchTextBox), new PropertyMetadata(-1));
            #endregion

            #region SelectedItemProperty

            SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(string),
            typeof(SearchTextBox), new PropertyMetadata(null));
            #endregion

            #region HorizontalOffsetPointProperty
            HorizontalOffsetPointProperty =
            DependencyProperty.Register("HorizontalOffsetPoint", typeof(double),
            typeof(SearchTextBox), new PropertyMetadata());
            #endregion

            #region VerticalOffsetPointProperty
            VerticalOffsetPointProperty =
            DependencyProperty.Register("VerticalOffsetPoint", typeof(double),
            typeof(SearchTextBox), new PropertyMetadata());
            #endregion

            #region RadiusProperty

            RadiusProperty =
            DependencyProperty.Register("Radius", typeof(CornerRadius)
            , typeof(SearchTextBox), new PropertyMetadata());
            #endregion
        }

        #region HorizontalOffsetPointProperty


        internal double HorizontalOffsetPoint
        {
            get { return (double)GetValue(HorizontalOffsetPointProperty); }
            set { SetValue(HorizontalOffsetPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Leftpoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalOffsetPointProperty;


        #endregion

        #region VerticalOffsetPointProperty


        internal double VerticalOffsetPoint
        {
            get { return (double)GetValue(VerticalOffsetPointProperty); }
            set { SetValue(VerticalOffsetPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Leftpoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOffsetPointProperty;


        #endregion

        #region IsSelectedProperty

        #endregion

        #region SelectedItemProperty
        public static DependencyProperty SelectedItemProperty;

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        #endregion

        #region SelectedIndexProperty

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty;

        #endregion

        #region ItemsSource


        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty;


        #endregion

        #region ShowItemsProperty


        internal IEnumerable<object> ShowItems
        {
            get { return (IEnumerable<object>)GetValue(ShowItemsProperty); }
            set { SetValue(ShowItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowItemsProperty;


        #endregion

        #region IsOpenProperty
        public static DependencyProperty IsOpenProperty;

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        #endregion

        #region RadiusProperty


        public CornerRadius Radius
        {
            get { return (CornerRadius)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty;


        #endregion
    }
}
