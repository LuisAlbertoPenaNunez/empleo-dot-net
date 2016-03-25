﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Transitions;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Helpers;
using Android.ViewModels;
using Android.Support.V7.Widget;

namespace Android
{
	public class JobsFragment : Fragment, IBackPressed
	{
		ListView _listView;

		JobsFragmentViewModel _viewModel;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			GetDependencies();

			_viewModel.OnCreate();
		}

		void GetDependencies ()
		{
			_viewModel = ServiceLocator.Current.GetInstance<JobsFragmentViewModel>();
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.JobsFragmentLayout, container, false);

			_listView = view.FindViewById<ListView> (Resource.Id.JobsListView);

			return view;
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);

			SetUp ();
		}

		public override void OnResume ()
		{
			base.OnResume ();

			_listView.ItemClick += OnListViewItemClick;

			_viewModel.OnJobSelectedEvent += OnJobSelected;

			_viewModel.OnResume();
		}

		public override void OnStop ()
		{
			base.OnStop ();

			_listView.ItemClick -= OnListViewItemClick;

			_viewModel.OnJobSelectedEvent -= OnJobSelected;

			_viewModel.OnStop();
		}

		void SetUp()
		{
			_listView.Adapter = _viewModel.People.GetAdapter(OnJobAdapterView);
		}

		void OnListViewItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			_viewModel.OnJobSelectedCommand.Execute(null);
		}

		void OnJobSelected (object sender, EventArgs e)
		{
			var jobDetailFragment = new JobDetail
			{
				EnterTransition = new Fade(),
				ExitTransition = new Fade()
			};

			ChildFragmentManager
				.BeginTransaction()
				.Replace(Resource.Id.container, jobDetailFragment)
				.AddToBackStack(null)
				.Commit();
		}

		View OnJobAdapterView (int position, JobItemViewModel model, View convertView)
		{
			var view = convertView ?? LayoutInflater.From(Context).Inflate (Resource.Layout.JobCardLayout, null);

			var title = view.FindViewById<TextView> (Resource.Id.JobCardTitleTextView);

			var company = view.FindViewById<TextView> (Resource.Id.JobCardCompanyTextView);

			var location = view.FindViewById<TextView> (Resource.Id.JobCardLocationTextView);

			var locationIcon = view.FindViewById<AppCompatImageView>(Resource.Id.locationIcon);

			var remote = view.FindViewById<TextView> (Resource.Id.JobCardRemoteTextView);

			var isRemoteIcon = view.FindViewById<ImageView> (Resource.Id.JobCardRemoteImageView);

			var category = view.FindViewById<TextView>(Resource.Id.JobCategory);

			title.Text = model.Title;

			company.Text = model.CompanyName;

			category.Text = model.Category;

			locationIcon.Visibility = string.IsNullOrEmpty(model.Location) ? ViewStates.Invisible : ViewStates.Visible;

			location.Text = model.Location;

			remote.Text = model.IsRemote ? GetString(Resource.String.IsRemote) : string.Empty;

			isRemoteIcon.Visibility = model.IsRemote ? ViewStates.Visible : ViewStates.Invisible;

			return view;
		}

		public bool OnBackPressed ()
		{
			return new BackPressImpl(this).OnBackPressed();
		}
	}
}

