namespace EstiveAqui.Pages
{
    using Plugin.Share;
    using System;
    using Xamarin.Forms;

    public partial class ReportsPageTemplate : ContentView
    {
        public ReportsPageTemplate()
        {
            InitializeComponent();
        }

        private void ShareJson(object sender, EventArgs e)
        {
            var mi = ((Image)sender);
            var report = mi.BindingContext as Model.ReportModel;
            var url = $"{report.Url}/{report.NomeArquivo}.json";
            CrossShare.Current.ShareLink($"Baixe o relatório no formato JSON, acessando o link abaixo:\r\n{url}");
        }

        private void ShareCsv(object sender, EventArgs e)
        {
            var mi = ((Image)sender);
            var report = mi.BindingContext as Model.ReportModel;
            var url = $"{report.Url}/{report.NomeArquivo}.csv";
            CrossShare.Current.ShareLink($"Baixe o relatório no formato CSV, acessando o link abaixo:\r\n{url}");
        }

        private void ShareXls(object sender, EventArgs e)
        {
            var mi = ((Image)sender);
            var report = mi.BindingContext as Model.ReportModel;
            var url = $"{report.Url}/{report.NomeArquivo}.xls";
            CrossShare.Current.ShareLink($"Baixe o relatório no formato XLS, acessando o link abaixo:\r\n{url}");
        }
    }
}