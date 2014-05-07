using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaRevCo.Presentation.Factories;
using MediaRevCo.Presentation.ViewModels;
using MediaRevCo.Business.Entities;

namespace MediaRevCo.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ReviewAdder.xaml
    /// </summary>
    public partial class ReviewAdder : UserControl
    {
        public ReviewAdder()
        {
            InitializeComponent();
        }

        public ReviewAdderViewModel Model
        {
            get { return DataContext as ReviewAdderViewModel; }
            set { DataContext = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Review lReview = new Review()
            {
                MediaItemTitle = this.MediaTitleInput.Text,
                ReviewTitle = this.ReviewTitleInput.Text,
                Comments = this.CommentInput.Text,
                UPC = this.UPCInput.Text
            };

            Model.AddReview(lReview);
        }
    }
}
