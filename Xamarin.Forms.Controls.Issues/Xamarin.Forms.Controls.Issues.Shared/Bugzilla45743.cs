﻿using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
#endif

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	public class Bugzilla45743 : TestNavigationPage
	{
		protected override void Init()
		{
			PushAsync(new ContentPage
			{
				Content = new StackLayout
				{
					AutomationId = "Page1",
					Children =
					{
						new Label { Text = "Page 1" }
					}
				}
			});

			Device.BeginInvokeOnMainThread(async () =>
			{
				await DisplayAlert("Title", "Message", "Accept", "Cancel");
			});

			Device.BeginInvokeOnMainThread(async () =>
			{
				await PushAsync(new ContentPage
				{
					AutomationId = "Page2",
					Content = new StackLayout
					{
						Children =
						{
							new Label { Text = "Page 2" }
						}
					}
				});
			});

			Device.BeginInvokeOnMainThread(async () =>
			{
				await DisplayAlert("Title2", "Message", "Accept", "Cancel");
			});
		}

#if UITEST

#if __IOS__
		[Test]
		public void Bugzilla45743_Test()
		{
			RunningApp.WaitForElement(q => q.Marked("Title 2"));
			RunningApp.Tap("Accept");
			RunningApp.WaitForElement(q => q.Marked("Title"));
			RunningApp.Tap("Accept");
			Assert.True(RunningApp.Query(q => q.Text("Page 2")).Length > 0);
		}
#endif

#endif
	}
}
