
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using EstiveAqui;
using Android.Views.InputMethods;
using EstiveAqui.Droid.Renderers;

[assembly: ExportRenderer(typeof(BaseEntry), typeof(BaseEntryRenderer))]
namespace EstiveAqui.Droid.Renderers
{
    public class BaseEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            BaseEntry base_entry = (BaseEntry)this.Element;

            base.OnElementChanged(e);

            if (Control != null && base_entry != null)
            {

                SetReturnType(base_entry);

                // Editor Action is called when the return button is pressed
                Control.EditorAction += (object sender, TextView.EditorActionEventArgs args) =>
                {
                    if (base_entry.ReturnType != ReturnType.Next)
                        base_entry.Unfocus();

                    // Call all the methods attached to base_entry event handler Completed
                    base_entry.InvokeCompleted();
                };
            }
        }

        private void SetReturnType(BaseEntry entry)
        {
            ReturnType type = entry.ReturnType;

            switch (type)
            {
                case ReturnType.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel("Ir", ImeAction.Go);
                    break;
                case ReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Próximo", ImeAction.Next);
                    break;
                case ReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel("Enviar", ImeAction.Send);
                    break;
                case ReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel("Procurar", ImeAction.Search);
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel("Feito", ImeAction.Done);
                    break;
            }
        }
    }
}