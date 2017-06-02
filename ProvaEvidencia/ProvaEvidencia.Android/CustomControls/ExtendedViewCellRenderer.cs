using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using System.ComponentModel;
using ProvaEvidencia.Utils;
using Xamarin.Forms.Platform.Android;
using ProvaEvidencia.Droid.CustomControls;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace ProvaEvidencia.Droid.CustomControls
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;

        protected override Android.Views.View GetCellCore(Cell item,
                                                     Android.Views.View convertView,
                                                     ViewGroup parent,
                                                     Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);

            _selected = true;
            _unselectedBackground = _cellCore.Background;

            return _cellCore;
        }


        

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);

            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;

                if (_selected)
                {
                    var extendedViewCell = sender as ExtendedViewCell;
                    _cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                }
            }
        }

    }
}