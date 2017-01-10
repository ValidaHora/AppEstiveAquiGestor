namespace EstiveAqui.Pages
{
	using Services.Abstract;
	using System;
	using ViewModel;
	using Xamarin.Forms;

	public partial class ListReleasedHourPage : ContentPage
	{
		private readonly INavigationService _navigationPage;
		private ReleasedHourViewModel _current;
		private bool isReady;

		public ListReleasedHourPage()
		{
			InitializeComponent();
			_navigationPage = DependencyService.Get<INavigationService>();
		}

		protected override void OnAppearing()
		{
			this.BindingContext = new ReleasedHourViewModel();

			_current = BindingContext as ReleasedHourViewModel;

			if (!isReady)
			{
				isReady = true;
				filterUser.Items.Add("TODOS");
				foreach (var item in _current.UserItems)
					filterUser.Items.Add(item.Username.ToUpper());

				filterPassclock.Items.Add("TODOS");
				filterPassclock.Items.Add("MANUAL");
				foreach (var item in _current.PassclockItems)
					filterPassclock.Items.Add(item.Alias.ToUpper());

				if (!App.Current.Properties.ContainsKey("FilterUser"))
				{
					filterPeriodo.SelectedIndex = 1;
					labelPeriodo.Text = filterPeriodo.Items[1];
					App.Current.Properties.Add("FilterPeriodo", 1);
					App.Current.Properties.Add("FilterUser", 0);
					App.Current.Properties.Add("FilterPassclock", 0);
					App.Current.SavePropertiesAsync();
				}
				else
				{
					var FilterPeriodo = (int)App.Current.Properties["FilterPeriodo"];
					var FilterUser = (int)App.Current.Properties["FilterUser"];
					var FilterPassclock = (int)App.Current.Properties["FilterPassclock"];

					filterPeriodo.SelectedIndex = FilterPeriodo;
					labelPeriodo.Text = filterPeriodo.Items[FilterPeriodo];

					filterUser.SelectedIndex = FilterUser;
					labelUser.Text = filterUser.Items[FilterUser];

					filterPassclock.SelectedIndex = FilterPassclock;
					labelPassclock.Text = filterPassclock.Items[FilterPassclock];
				}

				Lista.ItemTapped += ItemTapped;

				filterPeriodo.SelectedIndexChanged += (object sender, EventArgs e) =>
				{
					var picker = ((Picker)sender);
					RefreshFilter(sender, e);
					labelPeriodo.Text = picker.Items[picker.SelectedIndex];
					App.Current.Properties["FilterPeriodo"] = picker.SelectedIndex;
					this.Unfocus();
				};

				filterUser.SelectedIndexChanged += (object sender, EventArgs e) =>
				{
					var picker = ((Picker)sender);
					RefreshFilter(sender, e);
					labelUser.Text = picker.Items[picker.SelectedIndex];
					App.Current.Properties["FilterUser"] = picker.SelectedIndex;
					this.Unfocus();
				};

				filterPassclock.SelectedIndexChanged += (object sender, EventArgs e) =>
				{
					var picker = ((Picker)sender);
					RefreshFilter(sender, e);
					labelPassclock.Text = picker.Items[picker.SelectedIndex];
					App.Current.Properties["FilterPassclock"] = picker.SelectedIndex;
					this.Unfocus();
				};

			}



			RefreshFilter(null, null);
			base.OnAppearing();
		}

		private async void ItemTapped(object sender, ItemTappedEventArgs e)
		{
			await _current.Read(e.Item);
		}

		private void RefreshFilter(object sender, System.EventArgs e)
		{
			var current = BindingContext as ReleasedHourViewModel;
			var selectedUsername = filterUser.SelectedIndex.Equals(-1) ? null : filterUser.Items[filterUser.SelectedIndex];
			var selectedPassclock = filterPassclock.SelectedIndex.Equals(-1) ? null : filterPassclock.Items[filterPassclock.SelectedIndex];

			current.FilterReleasedHours(selectedUsername, selectedPassclock, filterPeriodo.SelectedIndex);
		}

		private async void ShowAdd(object sender, System.EventArgs e)
		{
			await _navigationPage.PushAsync(typeof(Pages.CreateManualReleasedHourPage), true);
		}


		private void ShowPickerUsername(object sender, System.EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				filterPassclock.Unfocus();
				filterPeriodo.Unfocus();
				filterUser.Focus();
			});
		}

		private void ShowPickerPassclock(object sender, System.EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				filterUser.Unfocus();
				filterPeriodo.Unfocus();
				filterPassclock.Focus();
			});
		}

		private void ShowPickerPeriodo(object sender, System.EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				filterUser.Unfocus();
				filterPassclock.Unfocus();
				filterPeriodo.Focus();
			});
		}
	}
}