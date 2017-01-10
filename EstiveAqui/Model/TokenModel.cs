namespace EstiveAqui.Model
{
    using MvvmHelpers;
    using Services.Abstract;

    public class TokenModel : ObservableObject, IBaseModel<TokenModel>
    {
        #region Attributes
        private string id;
        private bool active;
        private string alias;
        private string value;
        private string barCode;
        #endregion

        #region Properties
        public string IdToken
        {
            get { return id; }
            set
            {
                SetProperty(ref id, value);
            }
        }
        public string Alias
        {
            get { return this.alias; }
            set
            {
                SetProperty(ref this.alias, value);
            }
        }
        public string Value
        {
            get { return this.value; }
            set
            {
                SetProperty(ref this.value, value);
            }
        }

        public bool Active
        {
            get { return this.active; }
            set
            {
                SetProperty(ref this.active, value);
            }
        }

        public bool ActiveToggle
        {
            get { return !this.active; }
        }

        public string BarCode
        {
            get { return barCode; }
            set
            {
                SetProperty(ref barCode, value);
            }
        }

        public TokenModel Copy
        {
            get
            {
                return new TokenModel
                {
                    Alias = alias,
                    Value = value,
                    BarCode = barCode
                };
            }
        }
        #endregion
    }
}