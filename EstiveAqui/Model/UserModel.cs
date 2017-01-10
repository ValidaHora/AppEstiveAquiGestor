namespace EstiveAqui.Model
{
    using EstiveAqui.Services.Abstract;
    using MvvmHelpers;

    public class UserModel : ObservableObject, IBaseModel<UserModel>
    {
        #region Attributes
        private string id;
        private bool active;
        private string username;
        private string activateCode;
        private int maxLancamentoDia;
        private string integrationId;
        #endregion

        #region Properties
        public string IdUser
        {
            get { return id; }
            set
            {
                SetProperty(ref id, value);
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                SetProperty(ref username, value);
            }
        }

        public string ActivateCode
        {
            get { return activateCode; }
            set
            {
                SetProperty(ref activateCode, value);
            }
        }

        public int MaxLancamentoDia
        {
            get { return maxLancamentoDia; }
            set
            {
                SetProperty(ref maxLancamentoDia, value);
            }
        }

        public string IntegrationId
        {
            get { return integrationId; }
            set
            {
                SetProperty(ref integrationId, value);
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

        public UserModel Copy
        {
            get
            {
                return new UserModel
                {
                    IdUser = id,
                    Username = username,
                    IntegrationId = integrationId,
                    MaxLancamentoDia = maxLancamentoDia
                };
            }
        }
        #endregion
    }
}
