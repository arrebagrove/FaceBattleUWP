using FaceBattleUWP.Common;
using FaceBattleUWP.ViewModel;
using JP.Utils.Helper;
using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Navigation;

namespace FaceBattleUWP.View
{
    public sealed partial class UploadAnalysisPage : BindablePage
    {
        private UploadAnalysisViewModel UploadAnalysisVM;

        public bool ShowConfrim
        {
            get { return (bool)GetValue(ShowConfrimProperty); }
            set { SetValue(ShowConfrimProperty, value); }
        }

        public static readonly DependencyProperty ShowConfrimProperty =
            DependencyProperty.Register("ShowConfrim", typeof(bool), typeof(UploadAnalysisPage),
                new PropertyMetadata(true, OnShowConfrimPropertyChanged));

        private static void OnShowConfrimPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var page = d as UploadAnalysisPage;
            page.ToggleConfrimGridAnimation((bool)e.NewValue);
            page.ToggleImageAnimation((bool)e.NewValue);
        }

        private Compositor _compositor;
        private Visual _confirmVisual;
        private Visual _imageVisual;

        public UploadAnalysisPage()
        {
            this.InitializeComponent();
            this.DataContext = UploadAnalysisVM = new UploadAnalysisViewModel();
            NavigationCacheMode = NavigationCacheMode.Disabled;
            this.InitComposition();
            this.InitBinding();
        }

        private void InitBinding()
        {
            var b = new Binding()
            {
                Source = UploadAnalysisVM,
                Path = new PropertyPath("ShowConfirmGrid"),
                Mode = BindingMode.TwoWay,
            };
            this.SetBinding(ShowConfrimProperty, b);
        }

        private void InitComposition()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            _confirmVisual = ElementCompositionPreview.GetElementVisual(ConfirmGrid);
            _confirmVisual.Offset = new Vector3(0f, 200f, 0f);
            _imageVisual = ElementCompositionPreview.GetElementVisual(DisplayImage);
            _imageVisual.Offset = new Vector3(0f, 100f, 0f);
        }

        protected override void SetupTitleBar()
        {
            TitleBarHelper.SetUpDarkTitleBar();
        }

        protected override void SetUpStatusBar()
        {
            if (APIInfoHelper.HasStatusBar)
            {
                StatusBarHelper.SetUpWhiteStatusBar();
            }
        }

        private void ToggleConfrimGridAnimation(bool show)
        {
            var offsetAnimation = _compositor.CreateScalarKeyFrameAnimation();
            offsetAnimation.InsertKeyFrame(1f, show ? 0f : 200f);
            offsetAnimation.Duration = TimeSpan.FromMilliseconds(500);

            _confirmVisual.StartAnimation("Offset.y", offsetAnimation);
        }

        private void ToggleImageAnimation(bool stay)
        {
            var offsetAnimation = _compositor.CreateScalarKeyFrameAnimation();
            offsetAnimation.InsertKeyFrame(1f, stay ? 0f : 100f);
            offsetAnimation.Duration = TimeSpan.FromMilliseconds(500);

            _imageVisual.StartAnimation("Offset.y", offsetAnimation);
        }

        private void ConfirmUploadBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleConfrimGridAnimation(false);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ShowConfrim = true;
        }
    }
}
