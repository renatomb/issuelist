using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace issuelist
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    /*
     * Universidade Potiguar
     * CST em Análises e Desenvolvimento de Sistemas
     * Computação para Dispositivos Móveis - Atividade Pratica Supervisionada APS
     * Renato Monteiro Batista - 201816437 <rmb@unp.edu.br>
     */

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void btLogin_Clicked(System.Object sender, System.EventArgs e)
        {
            var httpc = new HttpClient();
            usr.autenticacao = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(enLogin.Text + ":" + enPass.Text));
            httpc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", usr.autenticacao);
            httpc.BaseAddress = new Uri("https://api.github.com/");
            var httpDataIssue = await httpc.GetAsync("issues?state=open"); // Obtem a lista de issues abertos do usuario.
            if (httpDataIssue.IsSuccessStatusCode)
            {
                var strRespostaIssue = await httpDataIssue.Content.ReadAsStringAsync();
                usr.Issues = JsonConvert.DeserializeObject<ObservableCollection<Issue>>(strRespostaIssue);
                var httpDataRepos = await httpc.GetAsync("user/repos"); // Obtem a lista de repositorios do usuario para cadastrar novos issues.
                var strRespostaRepos = await httpDataRepos.Content.ReadAsStringAsync();
                usr.Repositorios = JsonConvert.DeserializeObject<ObservableCollection<Repos>>(strRespostaRepos);
                await Navigation.PushAsync(new PageIssues());
            }
            else
            {
                await DisplayAlert("Erro", "Credenciais invalidas", "OK");
            }
        }
    }
}
