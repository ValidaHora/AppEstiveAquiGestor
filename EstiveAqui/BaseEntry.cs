using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EstiveAqui
{
    public class BaseEntry : Entry
    {
        // Need to overwrite default handler because we cant Invoke otherwise
        public new event EventHandler Completed;

        public static readonly BindableProperty ReturnTypeProperty =
            BindableProperty.Create<BaseEntry, ReturnType>(s => s.ReturnType, ReturnType.Done);
        

        public ReturnType ReturnType
        {
            get { return (ReturnType)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public void InvokeCompleted()
        {
            if (this.Completed != null)
                this.Completed.Invoke(this, null);
        }
    }

    public enum ReturnType
    {
        Go,
        Next,
        Done,
        Send,
        Search
    }
}
