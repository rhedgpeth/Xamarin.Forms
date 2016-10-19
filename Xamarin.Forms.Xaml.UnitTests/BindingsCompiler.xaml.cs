using System;
using System.Collections.Generic;

using Xamarin.Forms;
using NUnit.Framework;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xamarin.Forms.Xaml.UnitTests
{
	public partial class BindingsCompiler : ContentPage
	{
		public BindingsCompiler()
		{
			InitializeComponent();
		}

		public BindingsCompiler(bool useCompiledXaml)
		{
			//this stub will be replaced at compile time
		}

		[TestFixture]
		public class Tests
		{
			[TestCase(false)]
			[TestCase(true)]
			public void Test(bool useCompiledXaml)
			{
				MockCompiler.Compile(typeof(BindingsCompiler));
			}
		}
	}

	class MockViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public MockViewModel(string text = null)
		{
			_text = text;
		}

		string _text;
		public string Text {
			get { return _text; }
			set {
				if (_text == value)
					return;

				_text = value;
				OnPropertyChanged();
			}
		}

		MockViewModel _model;
		public MockViewModel Model {
			get { return _model; }
			set {
				if (_model == value)
					return;
				_model = value;
				OnPropertyChanged();
			}
		}

		string [] values = new string [5];
		[IndexerName("Indexer")]
		public string this [int v] {
			get { return values [v]; }
			set {
				if (values [v] == value)
					return;

				values [v] = value;
				OnPropertyChanged("Indexer[" + v + "]");
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}