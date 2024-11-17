using AVFoundation;
using CoreGraphics;
using CustomRenderer;
using CustomRenderer.iOS;
using Foundation;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using pdftron.PDF;
using pdftron.PDF.Tools;
using pdftron.PDF.Controls;

[assembly: ExportRenderer(typeof(ViewerPage), typeof(ViewerPageRenderer))]
namespace CustomRenderer.iOS
{
    public class ViewerPageRenderer : PageRenderer
    {
        private PDFViewCtrl mPdfViewCtrl;
        private PDFDoc mPdfDoc;
        private PTToolManager mToolManager;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                SetupUserInterface();
                //SetupEventHandlers();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            mPdfViewCtrl?.CloseDoc();
            mPdfViewCtrl = null;
            mPdfDoc?.Close();
            mPdfDoc = null;
        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
        }

      
        void SetupUserInterface()
        {
            CGRect viewRect = new CGRect(0, 0, View.Frame.Size.Width, View.Frame.Size.Height);
            mPdfViewCtrl = new pdftron.PDF.PDFViewCtrl();

            var m_toolManager = new PTToolManager(mPdfViewCtrl);
            mPdfViewCtrl.ToolManager = m_toolManager;

            var m_annotationToolbar = new PTAnnotationToolbar(m_toolManager);
            
      
 
            // Create a UIStackView to hold the annotation toolbar
            var stackView = new UIStackView(viewRect)
            {
                Spacing = 0,
                Axis = UILayoutConstraintAxis.Vertical,

            };

            stackView.AddArrangedSubview(m_annotationToolbar);


            View.AddSubview(stackView);

        }
        void SetupEventHandlers()
            {
                mPdfViewCtrl.PageNumberChangedFrom += (sender, e) =>
                {};

                mToolManager.ToolManagerToolChanged += (sender, e) =>
                {};
            }
        }
    }
